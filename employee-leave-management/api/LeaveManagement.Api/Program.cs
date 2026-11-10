using LeaveManagement.Api.Data;
using LeaveManagement.Api.Dtos;
using LeaveManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LeaveDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LeaveDb")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Starter database initialization. Use EF Core migrations for a production deployment.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LeaveDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/", () => Results.Ok(new
{
    name = "Employee Leave Management API",
    version = "1.0",
    documentation = "/swagger"
}));

app.MapGet("/api/employees", async (LeaveDbContext db) =>
{
    var employees = await db.Employees.AsNoTracking()
        .OrderBy(e => e.FullName)
        .Select(e => ToEmployeeResponse(e))
        .ToListAsync();
    return Results.Ok(employees);
});

app.MapGet("/api/employees/{id:int}", async (int id, LeaveDbContext db) =>
{
    var employee = await db.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    return employee is null
        ? Results.NotFound(new { message = "Employee not found." })
        : Results.Ok(ToEmployeeResponse(employee));
});

app.MapPost("/api/employees", async (CreateEmployeeRequest request, LeaveDbContext db) =>
{
    if (string.IsNullOrWhiteSpace(request.EmployeeNumber) ||
        string.IsNullOrWhiteSpace(request.FullName) ||
        !IsValidEmail(request.Email) ||
        !IsValidEmail(request.ManagerEmail) ||
        string.IsNullOrWhiteSpace(request.Department) ||
        request.AnnualLeaveAllowance < 0)
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["request"] = ["Provide valid employee details and a non-negative leave allowance."]
        });

    var email = request.Email.Trim().ToLowerInvariant();
    var employeeNumber = request.EmployeeNumber.Trim();

    if (await db.Employees.AnyAsync(e => e.Email == email || e.EmployeeNumber == employeeNumber))
        return Results.Conflict(new { message = "Employee email or employee number already exists." });

    var employee = new Employee
    {
        EmployeeNumber = employeeNumber,
        FullName = request.FullName.Trim(),
        Email = email,
        Department = request.Department.Trim(),
        ManagerEmail = request.ManagerEmail.Trim().ToLowerInvariant(),
        AnnualLeaveAllowance = request.AnnualLeaveAllowance
    };

    db.Employees.Add(employee);
    await db.SaveChangesAsync();
    return Results.Created($"/api/employees/{employee.Id}", ToEmployeeResponse(employee));
});

app.MapGet("/api/leaverequests", async (string? employeeEmail, LeaveDbContext db) =>
{
    var query = db.LeaveRequests.AsNoTracking().Include(r => r.Employee).AsQueryable();

    if (!string.IsNullOrWhiteSpace(employeeEmail))
        query = query.Where(r => r.Employee!.Email == employeeEmail.Trim().ToLowerInvariant());

    var requests = await query.OrderByDescending(r => r.CreatedUtc).ToListAsync();
    return Results.Ok(requests.Select(ToLeaveResponse));
});

app.MapGet("/api/leaverequests/{id:int}", async (int id, LeaveDbContext db) =>
{
    var request = await db.LeaveRequests.AsNoTracking()
        .Include(r => r.Employee)
        .FirstOrDefaultAsync(r => r.Id == id);

    return request is null
        ? Results.NotFound(new { message = "Leave request not found." })
        : Results.Ok(ToLeaveResponse(request));
});

app.MapPost("/api/leaverequests", async (CreateLeaveRequest request, LeaveDbContext db) =>
{
    var errors = new Dictionary<string, string[]>();

    if (request.EmployeeId <= 0)
        errors["employeeId"] = ["A valid employee ID is required."];
    if (string.IsNullOrWhiteSpace(request.LeaveType))
        errors["leaveType"] = ["Leave type is required."];
    if (request.StartDate == default || request.EndDate == default || request.EndDate < request.StartDate)
        errors["dates"] = ["Provide valid dates. End date must be on or after start date."];
    if (string.IsNullOrWhiteSpace(request.Reason) || request.Reason.Trim().Length < 5)
        errors["reason"] = ["Reason must contain at least five characters."];

    if (errors.Count > 0)
        return Results.ValidationProblem(errors);

    var employee = await db.Employees.FirstOrDefaultAsync(e => e.Id == request.EmployeeId && e.IsActive);
    if (employee is null)
        return Results.NotFound(new { message = "Active employee not found." });

    var leave = new LeaveRequest
    {
        EmployeeId = employee.Id,
        LeaveType = request.LeaveType.Trim(),
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        Reason = request.Reason.Trim(),
        Status = "Pending"
    };

    db.LeaveRequests.Add(leave);
    await db.SaveChangesAsync();
    await db.Entry(leave).Reference(r => r.Employee).LoadAsync();

    return Results.Created($"/api/leaverequests/{leave.Id}", ToLeaveResponse(leave));
});

app.MapPut("/api/leaverequests/{id:int}/approve", async (int id, DecisionRequest decision, LeaveDbContext db) =>
{
    var request = await db.LeaveRequests.Include(r => r.Employee)
        .FirstOrDefaultAsync(r => r.Id == id);

    if (request is null)
        return Results.NotFound(new { message = "Leave request not found." });
    if (request.Status != "Pending")
        return Results.Conflict(new { message = "Only pending requests can be approved." });

    request.Status = "Approved";
    request.ManagerComment = decision.Comment?.Trim();
    request.DecisionUtc = DateTime.UtcNow;
    await db.SaveChangesAsync();

    return Results.Ok(ToLeaveResponse(request));
});

app.MapPut("/api/leaverequests/{id:int}/reject", async (int id, DecisionRequest decision, LeaveDbContext db) =>
{
    var request = await db.LeaveRequests.Include(r => r.Employee)
        .FirstOrDefaultAsync(r => r.Id == id);

    if (request is null)
        return Results.NotFound(new { message = "Leave request not found." });
    if (request.Status != "Pending")
        return Results.Conflict(new { message = "Only pending requests can be rejected." });
    if (string.IsNullOrWhiteSpace(decision.Comment))
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["comment"] = ["A rejection comment is required."]
        });

    request.Status = "Rejected";
    request.ManagerComment = decision.Comment.Trim();
    request.DecisionUtc = DateTime.UtcNow;
    await db.SaveChangesAsync();

    return Results.Ok(ToLeaveResponse(request));
});

app.Run();

static bool IsValidEmail(string value) =>
    !string.IsNullOrWhiteSpace(value) &&
    System.Text.RegularExpressions.Regex.IsMatch(
        value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

static EmployeeResponse ToEmployeeResponse(Employee e) =>
    new(e.Id, e.EmployeeNumber, e.FullName, e.Email, e.Department,
        e.ManagerEmail, e.AnnualLeaveAllowance, e.IsActive);

static LeaveRequestResponse ToLeaveResponse(LeaveRequest r) =>
    new(r.Id, r.EmployeeId, r.Employee?.FullName ?? "Unknown",
        r.Employee?.Email ?? "", r.LeaveType, r.StartDate, r.EndDate,
        r.Reason, r.Status, r.ManagerComment, r.CreatedUtc, r.DecisionUtc);
