namespace LeaveManagement.Api.Dtos;

public sealed record CreateLeaveRequest(
    int EmployeeId,
    string LeaveType,
    DateOnly StartDate,
    DateOnly EndDate,
    string Reason);

public sealed record DecisionRequest(string? Comment);

public sealed record LeaveRequestResponse(
    int Id,
    int EmployeeId,
    string EmployeeName,
    string EmployeeEmail,
    string LeaveType,
    DateOnly StartDate,
    DateOnly EndDate,
    string Reason,
    string Status,
    string? ManagerComment,
    DateTime CreatedUtc,
    DateTime? DecisionUtc);
