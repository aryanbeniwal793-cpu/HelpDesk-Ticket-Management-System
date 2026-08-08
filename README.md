# HelpDeskManagement

Help Desk Ticket Management System built using ASP.NET Core Web API, ASP.NET Core MVC, Entity Framework Core, SQL Server, xUnit, Moq, and GitHub[cite: 1].

---

## Business Scenario
A company receives support requests from employees regarding software, hardware, and network-related issues[cite: 1]. The Help Desk Ticket Management System allows the team to[cite: 1]:
- Raise new tickets[cite: 1]
- View all tickets and individual ticket details[cite: 1]
- Update ticket information[cite: 1]
- Delete tickets[cite: 1]
- Filter tickets by status (*Open*, *In Progress*, *Closed*)[cite: 1]

---

## Solution Structure

The Visual Studio / VS Code solution `HelpDeskManagement.sln` contains the following project hierarchy[cite: 1]:

| Project Name | Project Type | Purpose |
| :--- | :--- | :--- |
| **HelpDesk.Api** | ASP.NET Core Web API | Implements REST APIs, Entity Framework Core, SQL Server, and Repository Pattern[cite: 1]. |
| **HelpDesk.Mvc** | ASP.NET Core MVC | Consumes the Web API through a Service Layer (`HttpClient`)[cite: 1]. |
| **HelpDesk.Tests** | xUnit Test Project | Contains unit tests using xUnit and Moq[cite: 1]. |

---

## Tech Stack & Dependencies
- **Framework:** .NET 8.0 / ASP.NET Core[cite: 1]
- **ORM:** Entity Framework Core 8.0[cite: 1]
- **Database:** SQL Server / LocalDB[cite: 1]
- **Design Patterns:** Repository Pattern & Service Layer Pattern[cite: 1]
- **Unit Testing:** xUnit & Moq[cite: 1]
- **UI & Frontend:** ASP.NET Core MVC, Razor Views, Bootstrap[cite: 1]

---

##  Web API Endpoints (`HelpDesk.Api`)

The Web API provides the following endpoints through `TicketController`[cite: 1]:

| HTTP Method | API Endpoint URL | Description |
| :--- | :--- | :--- |
| `GET` | `/api/Ticket/All` | Get all tickets[cite: 1] |
| `GET` | `/api/Ticket/{id}` | Get ticket by Id[cite: 1] |
| `POST` | `/api/Ticket` | Create a new ticket[cite: 1] |
| `PUT` | `/api/Ticket/{id}` | Update an existing ticket[cite: 1] |
| `DELETE` | `/api/Ticket/{id}` | Delete a ticket[cite: 1] |
| `GET` | `/api/Ticket/Status/{status}` | Get all tickets by Status[cite: 1] |

---

## MVC UI Features (`HelpDesk.Mvc`)
- **Dashboard:** Displays total tickets, open tickets, and closed tickets[cite: 1].
- **View All Tickets:** Displays all tickets in a structured table[cite: 1].
- **View Ticket Details:** Displays complete information of one ticket[cite: 1].
- **Raise New Ticket:** Creates a new ticket with status defaulted to *Open*[cite: 1].
- **Edit Ticket:** Allows updating Title, Description, Priority, and Status[cite: 1].
- **Filter Tickets by Status:** Displays tickets as per selected status (*Open*, *In Progress*, *Closed*) using a dropdown list[cite: 1].

---

## Unit Testing (`HelpDesk.Tests`)

Unit tests are implemented for `TicketController` using **xUnit** and **Moq**[cite: 1]. The `ITicketRepository` layer is mocked so unit tests run independently without connecting to SQL Server[cite: 1].

### Mandatory Test Cases Implemented:
1. `GetAllTickets_ReturnsOkResult_WhenTicketsExist`[cite: 1]
2. `GetTicketById_ReturnsOkResult_WhenTicketExists`[cite: 1]
3. `GetTicketById_ReturnsNotFound_WhenTicketDoesNotExist`[cite: 1]
4. `CreateTicket_ReturnsOkResult_WhenTicketIsCreatedSuccessfully`[cite: 1]
5. `CreateTicket_ReturnsBadRequest_WhenTicketIsNull`[cite: 1]
6. `GetTicketsByStatus_ReturnsOkResult_WhenMatchingTicketsExist`[cite: 1]

---

## How to Run the Solution

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server / LocalDB[cite: 1]

### 1. Build & Run Web API
```bash
cd HelpDesk.Api
dotnet build
dotnet run

---