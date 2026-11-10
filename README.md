[README.md](https://github.com/user-attachments/files/34649442/README.md)
# Employee Leave Management System

A portfolio project for managing employee leave requests. This repository contains a C#/.NET API, an interactive browser-based UI prototype, sample data, and implementation specifications for a Microsoft Power Platform version.

> **Project status and scope:** The C# ASP.NET Core API source and browser UI prototype are included in this repository. The Power Apps Canvas App, SharePoint lists, and Power Automate approval flow are specified in the `power-platform/` guides but have **not** been deployed to a live Microsoft 365 tenant as part of this repository. No `.msapp` or exported Power Platform solution is included. Do not treat the guides as evidence of a live deployment.

## What is included

### Implemented in this repository

- **C# API:** ASP.NET Core 8 Minimal API source for employee and leave-request operations.
- **Data access:** Entity Framework Core with SQL Server configuration.
- **Interactive UI prototype:** HTML, CSS, and JavaScript screens for a leave-management dashboard and request workflow.
- **Sample data:** Fictional employee and leave-request CSV files for local demonstration.
- **API examples:** Example requests and endpoint documentation.

### Designed / documented, not tenant-deployed

- **Power Apps:** Canvas App screen plan, user roles, and Power Fx implementation guidance.
- **SharePoint:** Proposed Employees, LeaveRequests, and LeaveTypes list schemas.
- **Power Automate:** Approval and notification flow design.
- **Power BI:** Reporting/dashboard implementation guide.
- **UX:** Screen specifications, user journeys, and a usability test plan.

## Repository structure

```text
employee-leave-management/
├── api/                 # ASP.NET Core API source
├── ui-prototype/        # Interactive HTML/CSS/JavaScript prototype
├── power-platform/      # Power Apps, SharePoint, Flow, and Power BI guides
├── ux/                  # UX design specification and test plan
├── sample-data/         # Fictional demo data
├── docs/                # API examples
└── evidence/            # Instructions for adding genuine implementation evidence
```

## Run the browser UI prototype

Open `ui-prototype/index.html` in a modern browser. The prototype uses local sample/demo behavior; it is not connected to a live Microsoft tenant or the .NET API by default.

## Run the API

Requirements: .NET 8 SDK and SQL Server (LocalDB, SQL Server Express, or a SQL Server instance).

1. Update `api/LeaveManagement.Api/appsettings.json` with your SQL Server connection string.
2. Open a terminal in `api/LeaveManagement.Api`.
3. Restore and run:

```bash
dotnet restore
dotnet run
```

The starter API creates its database schema on startup using `EnsureCreated()`. Swagger is available at the URL printed by the app, typically `/swagger` in Development.

> This is a learning starter, not production HR software. Before production use, add authentication and authorization, EF Core migrations, audit logging, privacy controls, validation hardening, and appropriate security review.

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

## Power Platform implementation guides

The `power-platform/` folder describes how to build the Microsoft 365 version:

- Create SharePoint lists for Employees, LeaveRequests, and LeaveTypes.
- Build a Power Apps Canvas App with employee and manager experiences.
- Create a Power Automate approval flow and employee notifications.
- Optionally build Power BI reporting.

These are implementation instructions, not exported or deployed components. Add real screenshots and an exported `.msapp` or solution only after you create and test them in your own tenant. See [`evidence/README.md`](evidence/README.md).

## Sample data and metrics

The included CSVs contain fictional demo records. They are not real users and do not establish business impact. If you test the project, record the test setup and measured results (for example, API success rate, validation cases passed, request submission time, and approval cycle time). Only publish metrics you personally measured, and label simulated results as simulated.

## Resume wording

Use wording that matches the work you have completed. Based on the contents currently in this repository, a defensible entry is:

**Employee Leave Management System | C#, ASP.NET Core 8, Entity Framework Core, SQL Server, HTML, CSS, JavaScript, Power Platform design**

- Developed an ASP.NET Core REST API starter for employee and leave-request operations using C# and Entity Framework Core.
- Built an interactive browser-based leave-management UI prototype using HTML, CSS, and JavaScript.
- Designed the proposed SharePoint data model and documented Power Apps Canvas App screens and Power Automate approval workflows.
- Documented API usage and prepared fictional sample data for demonstration and testing.

After you actually build and test the Canvas App, SharePoint lists, and approval flow, update this README and your resume to state exactly what you implemented. Include measured results only when you have test evidence.

## Security and privacy

Do not upload real employee records, personal information, credentials, connection strings, or tenant-specific secrets. Use fictional data in a public portfolio repository.
