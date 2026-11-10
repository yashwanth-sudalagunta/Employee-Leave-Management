# SharePoint list schema

Create three SharePoint Lists. Use these internal names consistently in Power Apps formulas.

## Employees

| Column | Type | Notes |
|---|---|---|
| Title | Single line text | Employee full name |
| EmployeeNumber | Single line text | Enforce unique values |
| Email | Single line text | Enforce unique values |
| Department | Choice | Engineering, HR, Finance, Sales, Operations |
| ManagerEmail | Single line text | Manager's work email |
| AnnualLeaveAllowance | Number | Default 20 |
| IsActive | Yes/No | Default Yes |

## LeaveRequests

| Column | Type | Notes |
|---|---|---|
| Title | Single line text | Optional request label |
| EmployeeEmail | Single line text | Requester's email |
| EmployeeName | Single line text | Display snapshot |
| LeaveType | Choice | Annual, Personal, Sick |
| StartDate | Date only | Required |
| EndDate | Date only | Required |
| Reason | Multiple lines | Plain text |
| ApprovalStatus | Choice | Pending, Approved, Rejected |
| ManagerComments | Multiple lines | Plain text |
| SubmittedOn | Date/time | Default Created |
| DecisionOn | Date/time | Optional |

## LeaveTypes

| Column | Type | Notes |
|---|---|---|
| Title | Single line text | Leave type name |
| AnnualAllowance | Number | Configure per type if needed |
| Description | Multiple lines | Plain text |

### Data design notes

- For a real deployment, prefer a Person column for employee/manager identity where appropriate.
- Index `EmployeeEmail`, `ApprovalStatus`, and `StartDate` if lists will grow.
- Define whether balances are calendar days or working days, and how holidays/half-days are treated.
- Use a separate LeaveBalance ledger for auditable accrual and adjustments instead of directly overwriting a single number.
- Apply SharePoint permissions and Microsoft Entra ID security; hiding controls in the app is not authorization.
