using LeaveManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Api.Data;

public sealed class LeaveDbContext(DbContextOptions<LeaveDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email).IsUnique();

        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.EmployeeNumber).IsUnique();

        modelBuilder.Entity<LeaveRequest>()
            .HasOne(r => r.Employee)
            .WithMany(e => e.LeaveRequests)
            .HasForeignKey(r => r.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<LeaveRequest>()
            .Property(r => r.Status).HasMaxLength(20);

        modelBuilder.Entity<LeaveRequest>()
            .Property(r => r.LeaveType).HasMaxLength(40);
    }
}
