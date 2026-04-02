# Calendar Hub

## Project Overview

Calendar Hub is a personal productivity platform designed to connect multiple email and calendar accounts into a single unified view. The goal is to see all your meetings, events, and schedules in one place, across providers, so you can manage time efficiently and avoid missing sessions.

The scope includes:
- Aggregating calendar events from integrated accounts
- Displaying upcoming meetings and details in a dashboard
- Supporting multi-environment configurations for local/dev/staging/prod
- API and frontend separation (C# backend + Angular frontend)

## What This Project Is For

This project is intended as a central calendar aggregator for personal or team use. You can connect all your emails and calendars (e.g., Gmail, Outlook, corporate Exchange) and get a consolidated list of:
- Today's meetings
- Weekly agenda
- Event metadata (title, time, attendees, location)
- Status and conflicts

## Technology Stack

### Backend
- .NET (ASP.NET Core)
- C# Language
- Clean architecture: Core / Application / Infrastructure / API layers
- API versioning & Swagger
- Dependency injection and middleware for request validation
- Custom filters, exception handling, and route mapping

### Frontend
- Angular
- TypeScript
- SCSS styling
- Angular Routing

### Supporting Libraries
- Entity Framework Core (presumed from infrastructure patterns)
- Serilog / logging pipeline (likely)
- Custom model binders and validation filters

## Repo Structure

- `backend/` - .NET solution and projects:
  - `Api/` - REST API project
  - `Application/` - business logic and CQRS behaviors
  - `Core/` - DTOs, interfaces, constants, exceptions
  - `Infrastructure/` - data persistence integration
  - `Tests/` - unit tests
- `frontend/` - Angular app:
  - `src/app/` - application modules, components, services
  - `angular.json`, `package.json`, `tsconfig.json`

## Quick Start

### 1) Backend Setup

1. Open terminal in `backend`.
2. Restore packages:
   - `dotnet restore calendar-hub.sln`
3. Build solution:
   - `dotnet build calendar-hub.sln`
4. Run the API (development):
   - `cd Api`
   - `dotnet run`
5. API will launch on default URL shown in terminal (usually `https://localhost:5001` and `http://localhost:5000`).

### 2) Frontend Setup

1. Open terminal in `frontend`.
2. Install npm packages:
   - `npm install`
3. Run Angular development server:
   - `ng serve` (or `npm start` if script defined)
4. Open browser at `http://localhost:4200`.

### 3) Configuration

- Backend environments are in `backend/Api/appsettings.*.json`:
  - `appsettings.json` base
  - `appsettings.dev.json`, `appsettings.local.json`, `appsettings.stg.json`, `appsettings.qa.json`, `appsettings.prod.json`
- Set `ASPNETCORE_ENVIRONMENT` to choose profile (e.g., `Development`, `Staging`, `Production`).
- Frontend API base URLs should be configured in `frontend/src/environments/` files.

## How to Use

1. Start the backend API.
2. Start the frontend app.
3. In the UI, authenticate/link your email/calendars (implementation-specific flow).
4. Grant access and refresh.
5. View consolidated calendar items and meeting timelines.

## Testing

### Backend tests
- `cd backend/Tests`
- `dotnet test`

### Frontend tests
- `cd frontend`
- `ng test`

## Contribution

- Follow clean architecture conventions in `backend`.
- Keep UI components modular in `frontend/src/app`.
- Use meaningful commit messages and small incremental PRs.

## Notes

- Ensure API and UI endpoints are aligned.
- Add any OAuth provider settings and secret storage in `appsettings` or secret manager.
- If you add new calendars/references, update API interfaces in `Core` and `Application` layers.
