# API examples for Postman

Base URL: use the HTTPS URL printed by `dotnet run`.

## Create employee

POST `/api/employees`

```json
{
  "employeeNumber": "EMP-001",
  "fullName": "Jordan Davis",
  "email": "jordan.davis@example.test",
  "department": "Product Design",
  "managerEmail": "manager@example.test",
  "annualLeaveAllowance": 20
}
```

## Submit leave request

POST `/api/leaverequests`

```json
{
  "employeeId": 1,
  "leaveType": "Annual",
  "startDate": "2026-12-14",
  "endDate": "2026-12-16",
  "reason": "Family commitment"
}
```

## Approve request

PUT `/api/leaverequests/1/approve`

```json
{
  "comment": "Approved. Enjoy your time off."
}
```

## Reject request

PUT `/api/leaverequests/1/reject`

```json
{
  "comment": "Please coordinate coverage before resubmitting."
}
```

## Expected status codes

- 200 — successful read or decision
- 201 — employee/request created
- 404 — record not found
- 409 — duplicate employee or request already decided
- 400 — validation problem

This starter API does not include authentication. Do not expose it publicly or connect it to real employee records until authentication, authorization, and security controls are implemented.
