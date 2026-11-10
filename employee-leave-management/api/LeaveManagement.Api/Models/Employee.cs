namespace LeaveManagement.Api.Models;

public sealed class Employee
{
    public int Id { get; set; }
    public required string EmployeeNumber { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Department { get; set; }
    public required string ManagerEmail { get; set; }
    public int AnnualLeaveAllowance { get; set; } = 20;
    public bool IsActive { get; set; } = true;
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
}
