namespace LeaveManagement.Api.Dtos;

public sealed record CreateEmployeeRequest(
    string EmployeeNumber,
    string FullName,
    string Email,
    string Department,
    string ManagerEmail,
    int AnnualLeaveAllowance = 20);

public sealed record EmployeeResponse(
    int Id,
    string EmployeeNumber,
    string FullName,
    string Email,
    string Department,
    string ManagerEmail,
    int AnnualLeaveAllowance,
    bool IsActive);
