# Power Automate flow specifications

## Flow 1 — Manager leave approval

**Trigger:** SharePoint — When an item is created (LeaveRequests).

1. Get the employee record using EmployeeEmail.
2. Read ManagerEmail from the employee record.
3. Start and wait for an approval (Approve/Reject — First to respond).
4. If approved:
   - Update ApprovalStatus = Approved.
   - Save manager comments and DecisionOn.
   - Notify employee by Outlook email.
5. If rejected:
   - Update ApprovalStatus = Rejected.
   - Save comments and DecisionOn.
   - Notify employee by Outlook email.
6. Add a failure scope that logs the item ID and flow error for HR support.

## Flow 2 — HR notification

**Trigger:** When a LeaveRequests item is modified.

- Continue only when ApprovalStatus changes to Approved or Rejected.
- Notify the HR shared mailbox or Teams channel.
- Include request ID, employee, dates, leave type, and decision.
- Avoid sending duplicate messages by checking a notification-sent flag or storing an event ID.

## Flow 3 — Reminder for pending approvals

**Trigger:** Recurrence (e.g. each weekday morning).

- Get requests with ApprovalStatus = Pending.
- Compare SubmittedOn with the agreed SLA.
- Send a reminder to the assigned manager for requests older than the threshold.
- Log reminder date to prevent repeated daily spam.

## Governance and reliability

- Use solution-aware flows and environment variables for URLs/addresses.
- Use a service account or managed ownership model approved by the tenant administrator.
- Configure run-after handling and useful failure notifications.
- Avoid hard-coded personal email addresses.
- Test approval reassignment, timeouts, duplicate triggers, and connector failures.
