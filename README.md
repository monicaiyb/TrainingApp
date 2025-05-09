# Employee Workflow Approval System

## Introduction

This is an ASP.NET application that demonstrates a **workflow approval process** tailored for employee-related operations such as leave requests, procurement approvals, or onboarding. It showcases how to define, configure, and execute multi-step approval workflows using a clean and extensible architecture.

## Requirements 

- ASP.NET Core == 8
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- Hangfire
- MSTest



## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/monicaiyb/TrainingApp.git
```

### 2. Set Up Configuration
Update appsettings.Development.json or appsettings.json with your database and authentication configuration:
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=yourDatabaseName; Database=TrainingDb;User Id=yourUserId; Password=yourVerycomplexPassword;  Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
  },
}
```

### 3. Apply Migrations & Seed Database
Ensure EF Core tools are installed

### 4. Run the Application
Running the application will seed the database with initial user data
```
dotnet run
```
### 5. Explore the API (Swagger)


##  Workflow Overview
- Define workflow configuration and add steps and assign approvers

- Create a workflow request by saving an employee(e.g., leave request)

- Approvers receive notification based on the workflow sequence

- Status updates at each step (Pending, Approved, Rejected)



