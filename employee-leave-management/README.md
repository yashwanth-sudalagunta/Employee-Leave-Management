# Employee Leave Management System

A portfolio project demonstrating a leave-management workflow across a .NET REST API and a Microsoft Power Platform implementation blueprint.

> **Project status:** The ASP.NET Core API source and browser-based UI prototype are included. The Power Apps, SharePoint, Power Automate, Power BI, and Figma sections are implementation guides and assets to configure in your own Microsoft/Figma accounts. No live Microsoft tenant or production deployment is included.

## Solution overview

- **Frontend prototype:** HTML, CSS, vanilla JavaScript (interactive dashboard and leave-request form)
- **Backend:** C#, ASP.NET Core 8 Minimal API
- **Data access:** Entity Framework Core 8 + SQL Server
- **API documentation:** Swagger / OpenAPI
- **Power Platform blueprint:** Canvas App screens, SharePoint lists, Power Fx examples, and Power Automate flow specifications
- **UX deliverables:** user journeys, screen inventory, design system, and usability test plan

## Repository structure

```text
employee-leave-management/
├── api/
│   └── LeaveManagement.Api/
│       ├── Data/
│       ├── Dtos/
│       ├── Models/
│       ├── Properties/
│       ├── appsettings.json
│       ├── LeaveManagement.Api.csproj
│       └── Program.cs
├── ui-prototype/
│   ├── index.html
│   ├── styles.css
│   └── app.js
├── power-platform/
│   ├── SHAREPOINT_SCHEMA.md
│   ├── POWER_APPS_BUILD_GUIDE.md
│   ├── POWER_AUTOMATE_FLOWS.md
│   └── POWER_BI_GUIDE.md
├── ux/
│   ├── UX_DESIGN_SPEC.md
│   └── USABILITY_TEST_PLAN.md
├── sample-data/
│   ├── employees.csv
│   └── leave-requests.csv
└── docs/
    └── API_EXAMPLES.md
```

## Run the API

Requirements: .NET 8 SDK and SQL Server (LocalDB, SQL Server Express, or a SQL Server instance).

1. Update `api/LeaveManagement.Api/appsettings.json` with your SQL Server connection string.
2. Open a terminal in `api/LeaveManagement.Api`.
3. Restore and run:

```bash
dotnet restore
dotnet run
```

The API creates the database schema on startup using `EnsureCreated()` for this starter project. Swagger is available at the URL printed by the app, typically `/swagger` in Development.

> For a production project, replace `EnsureCreated()` with EF Core migrations, add authentication/authorization, structured logging, secrets management, and role-based policies.

## API endpoints

| Method | Route | Purpose |
|---|---|---|
| GET | `/api/employees` | List employees |
| GET | `/api/employees/{id}` | Get employee |
| POST | `/api/employees` | Create employee |
| GET | `/api/leaverequests` | List leave requests; optional `?employeeEmail=` |
| GET | `/api/leaverequests/{id}` | Get request |
| POST | `/api/leaverequests` | Submit request |
| PUT | `/api/leaverequests/{id}/approve` | Approve pending request |
| PUT | `/api/leaverequests/{id}/reject` | Reject pending request |

## Example API request

```http
POST /api/leaverequests
Content-Type: application/json

{
  "employeeId": 1,
  "leaveType": "Annual",
  "startDate": "2026-12-14",
  "endDate": "2026-12-16",
  "reason": "Family commitment"
}
```

## Power Platform build

Follow the documents in `power-platform/` to create:
- SharePoint lists for Employees, LeaveRequests, and LeaveTypes
- A Power Apps Canvas App with employee, manager, and HR views
- A Power Automate approval flow with manager approval and employee notification
- Optional Power BI reporting

The API and Power Platform blueprint are two integration paths for the same business process. For a connected end-to-end deployment, choose one system of record and implement a secured custom connector or a Power Automate HTTP action. Do not maintain two competing employee/leave databases.

## Sample test scope

The included sample CSVs contain **10 fictional employees and 20 fictional leave requests**. These are seed/demo records, not evidence of actual users or business impact.

For a portfolio test, expand to 50 synthetic employees and 100 synthetic requests, then record:
- API success/error counts
- Request submission duration
- Approval cycle duration
- Validation cases passed
- Responsive UI checks

Only add measured results to your resume after you run and document the tests.

## Suggested resume entry

**Employee Leave Management System | ASP.NET Core, C#, SQL Server, Power Apps, Power Automate, Figma**
- Built a C# ASP.NET Core REST API for employee and leave-request management using Entity Framework Core and SQL Server.
- Designed an employee/manager leave-management experience with a responsive UI prototype and documented Power Apps Canvas App implementation.
- Defined SharePoint data structures and Power Automate approval/notification workflows for a two-stage manager/HR process.
- Documented REST endpoints with Swagger and prepared API test scenarios for Postman.

Edit this entry to reflect only the components you personally implement and test.

## Important security note

This is a learning starter, not production HR software. It intentionally omits identity integration, authorization policies, audit logging, privacy controls, concurrency handling, holiday calendars, and legally compliant retention. Do not use real employee or health data.
