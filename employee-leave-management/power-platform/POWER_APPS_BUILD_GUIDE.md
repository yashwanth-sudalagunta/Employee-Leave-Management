# Power Apps Canvas App build guide

## App layout

Create a responsive Canvas App with these screens:
1. `scrHome` — employee dashboard and balance cards
2. `scrApplyLeave` — leave request form
3. `scrMyRequests` — employee's request gallery
4. `scrManagerApprovals` — manager's pending requests
5. `scrHRDashboard` — HR summary
6. `scrRequestDetails` — request details and decision comments

Connect the `Employees`, `LeaveRequests`, and `LeaveTypes` SharePoint lists as data sources.

## App.OnStart

```powerfx
Set(varCurrentEmail, Lower(User().Email));
Set(
    varCurrentEmployee,
    LookUp(Employees, Lower(Email) = varCurrentEmail)
);
Set(
    varIsManager,
    !IsBlank(LookUp(Employees, Lower(ManagerEmail) = varCurrentEmail))
);
Set(varIsHR, varCurrentEmployee.Department = "HR");
```

This is for navigation/display only. Enforce access with tenant permissions and server-side authorization in a real app.

## Employee request gallery

Set `galMyRequests.Items`:

```powerfx
SortByColumns(
    Filter(LeaveRequests, Lower(EmployeeEmail) = varCurrentEmail),
    "SubmittedOn",
    SortOrder.Descending
)
```

## Submit request button

Set `btnSubmit.DisplayMode` to require the fields:

```powerfx
If(
    IsBlank(dpStart.SelectedDate) ||
    IsBlank(dpEnd.SelectedDate) ||
    dpEnd.SelectedDate < dpStart.SelectedDate ||
    Len(Trim(txtReason.Text)) < 5,
    DisplayMode.Disabled,
    DisplayMode.Edit
)
```

Set `btnSubmit.OnSelect`:

```powerfx
If(
    CountRows(
        Filter(
            LeaveRequests,
            Lower(EmployeeEmail) = varCurrentEmail &&
            ApprovalStatus.Value = "Approved" &&
            StartDate <= dpEnd.SelectedDate &&
            EndDate >= dpStart.SelectedDate
        )
    ) > 0,
    Notify("You already have approved leave during these dates.", NotificationType.Error),
    IfError(
        Patch(
            LeaveRequests,
            Defaults(LeaveRequests),
            {
                Title: "Leave - " & Text(Now(), "yyyymmdd-hhmm"),
                EmployeeEmail: varCurrentEmail,
                EmployeeName: varCurrentEmployee.Title,
                LeaveType: {Value: ddLeaveType.Selected.Value},
                StartDate: dpStart.SelectedDate,
                EndDate: dpEnd.SelectedDate,
                Reason: Trim(txtReason.Text),
                ApprovalStatus: {Value: "Pending"},
                SubmittedOn: Now()
            }
        ),
        Notify("Could not submit the request. Try again.", NotificationType.Error),
        Notify("Request submitted.", NotificationType.Success);
        Reset(dpStart);
        Reset(dpEnd);
        Reset(txtReason);
        Navigate(scrMyRequests)
    )
)
```

Adapt Choice/Person column references to the actual SharePoint schema generated in your tenant.

## Manager approval gallery

Set `galPending.Items`:

```powerfx
Filter(
    LeaveRequests,
    ApprovalStatus.Value = "Pending" &&
    Lower(
        LookUp(Employees, Lower(Email) = Lower(EmployeeEmail), ManagerEmail)
    ) = varCurrentEmail
)
```

Approve button `OnSelect` (starter pattern):

```powerfx
Patch(
    LeaveRequests,
    ThisItem,
    {
        ApprovalStatus: {Value: "Approved"},
        ManagerComments: Trim(txtManagerComment.Text),
        DecisionOn: Now()
    }
);
Notify("Request approved.", NotificationType.Success);
Refresh(LeaveRequests)
```

Reject button should require a comment and patch `ApprovalStatus` to `Rejected`.

## UI/UX checklist

- Use a consistent 8px spacing scale and reusable components.
- Provide loading, empty, error, and success states.
- Use labels and accessible names for controls.
- Do not communicate status using color alone; include text labels.
- Confirm before destructive actions.
- Test phone, tablet, and desktop layouts.
- Test with employee, manager, and HR test accounts.
