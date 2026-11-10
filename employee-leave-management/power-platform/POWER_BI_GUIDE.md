# Power BI reporting guide

Connect Power BI Desktop to the SharePoint Online Lists (Employees and LeaveRequests).

## Suggested KPI cards

- Total active employees
- Requests submitted this year
- Pending approvals
- Approved leave days
- Rejected requests

## Suggested visuals

- Requests by month (line chart)
- Requests by department (bar chart)
- Requests by leave type (donut chart)
- Approval status distribution (stacked bar)
- Pending requests by manager (table)

## Data model

Relate Employees[Email] (one) to LeaveRequests[EmployeeEmail] (many). Normalize email values and avoid duplicate employee records.

## Example DAX measures

```dax
Total Requests = COUNTROWS(LeaveRequests)

Pending Requests =
CALCULATE(
    [Total Requests],
    LeaveRequests[ApprovalStatus] = "Pending"
)

Approved Requests =
CALCULATE(
    [Total Requests],
    LeaveRequests[ApprovalStatus] = "Approved"
)

Approved Leave Days =
CALCULATE(
    SUMX(
        FILTER(LeaveRequests, LeaveRequests[ApprovalStatus] = "Approved"),
        DATEDIFF(LeaveRequests[StartDate], LeaveRequests[EndDate], DAY) + 1
    )
)
```

The sample measure counts calendar days. Change it if your policy counts working days or excludes holidays.
