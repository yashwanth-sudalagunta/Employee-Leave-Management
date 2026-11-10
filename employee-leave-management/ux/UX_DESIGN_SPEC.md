# UX design specification

## Product goal

Help employees request time off quickly, help managers make informed decisions, and give HR a reliable overview of leave activity.

## Personas

- **Employee:** wants to check balance, submit a request, and track status.
- **Manager:** wants to review dates, reason, team context, and decide quickly.
- **HR administrator:** wants to maintain employee records and monitor trends.

## Primary user journey

Employee opens app → reviews available balance → selects leave type and dates → enters reason → checks validation → submits → sees confirmation → tracks request status.

Manager opens approvals → filters pending items → opens request details → reviews schedule → approves/rejects with comment → employee receives notification.

## Screen inventory

| Screen | Main elements | UX intent |
|---|---|---|
| Home | Balance cards, recent requests, CTA | At-a-glance status |
| Apply leave | Type, date range, reason, validation | Reduce submission errors |
| My requests | Search/filter, status labels, history | Transparency |
| Manager approvals | Pending queue, details, actions | Fast decisions |
| HR dashboard | KPIs, charts, employee list | Operational visibility |
| Request details | Timeline, comments, dates | Clear audit context |

## Visual system

- Primary: Indigo `#4361EE`
- Text: Slate `#182230`
- Page background: `#F7F8FB`
- Success: `#16845B`
- Warning: `#A96B13`
- Error: `#BA4A4A`
- Typography: DM Sans for interface text; Manrope for headings
- Spacing: 8px base scale
- Corner radius: 8–12px

## Figma setup

Create a Figma file with pages:
1. `01 - Research & Flows`
2. `02 - Wireframes`
3. `03 - UI Kit`
4. `04 - High Fidelity`
5. `05 - Prototype`

Frames: Desktop 1440×900 and mobile 390×844. Build components for navigation, buttons, status chips, stat cards, form fields, tables, and dialogs. Prototype the employee request journey and manager decision journey.

## Accessibility

Use clear labels, keyboard-friendly focus order, sufficient contrast, text status labels, error messages next to fields, and touch targets large enough for mobile use.
