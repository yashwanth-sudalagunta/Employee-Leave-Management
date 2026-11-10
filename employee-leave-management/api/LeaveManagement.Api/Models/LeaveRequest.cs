namespace LeaveManagement.Api.Models;

public sealed class LeaveRequest
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public required string LeaveType { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public required string Reason { get; set; }
    public string Status { get; set; } = "Pending";
    public string? ManagerComment { get; set; }
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DecisionUtc { get; set; }
}
