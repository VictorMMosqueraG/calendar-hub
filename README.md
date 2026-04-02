# CalendarHub

CalendarHub is a personal project designed to aggregate all your emails and display your meetings in a single, unified view. It helps you manage your schedule and communications more efficiently by bringing everything together in one place.

## Technologies Used

- **Backend**: .NET (C#) - Handles data processing, API endpoints, and integration with email and calendar services.
- **Frontend**: Angular - Provides a modern, responsive user interface for viewing and managing emails and meetings.
- **Monorepo Management**: Nx - Manages the workspace, build processes, and dependencies for both backend and frontend.
- **Build Tools**: .NET CLI for backend, Angular CLI for frontend.
- **Other**: TypeScript, ESLint for code quality.

## Prerequisites

Before running the project, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (version 10.0 or later)
- [Node.js](https://nodejs.org/) (version 18 or later)
- [Nx CLI](https://nx.dev/getting-started/installation) (install globally with `npm install -g nx`)

## Getting Started

1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd calendar-hub
   ```

2. Install dependencies:
   ```sh
   npm install
   ```

## Running the Application

### Backend

To run the backend server:

```sh
npx nx serve backend
```

This will start the .NET application, typically on `http://localhost:5000` or as configured in `apps/backend/Properties/launchSettings.json`.

### Frontend

To run the frontend development server:

```sh
npx nx serve frontend
```

This will start the Angular application, typically on `http://localhost:4200`.

### Running Both Simultaneously

You can run both backend and frontend in separate terminals, or use Nx to manage them.

## Building for Production

### Backend

```sh
npx nx build backend
```

### Frontend

```sh
npx nx build frontend
```

## Project Structure

- `apps/backend/`: .NET backend application
- `apps/frontend/`: Angular frontend application
- `libs/shared-models/`: Shared TypeScript models and utilities
- `nx.json`: Nx workspace configuration

## Contributing

This is a personal project, but feel free to fork and modify as needed.

## License

MIT

You can use `npx nx list` to get a list of installed plugins. Then, run `npx nx list <plugin-name>` to learn about more specific capabilities of a particular plugin. Alternatively, [install Nx Console](https://nx.dev/getting-started/editor-setup?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects) to browse plugins and generators in your IDE.

[Learn more about Nx plugins &raquo;](https://nx.dev/concepts/nx-plugins?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects) | [Browse the plugin registry &raquo;](https://nx.dev/plugin-registry?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)

## Set up CI!

### Step 1

To connect to Nx Cloud, run the following command:

```sh
npx nx connect
```

Connecting to Nx Cloud ensures a [fast and scalable CI](https://nx.dev/ci/intro/why-nx-cloud?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects) pipeline. It includes features such as:

- [Remote caching](https://nx.dev/ci/features/remote-cache?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)
- [Task distribution across multiple machines](https://nx.dev/ci/features/distribute-task-execution?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)
- [Automated e2e test splitting](https://nx.dev/ci/features/split-e2e-tasks?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)
- [Task flakiness detection and rerunning](https://nx.dev/ci/features/flaky-tasks?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)

### Step 2

Use the following command to configure a CI workflow for your workspace:

```sh
npx nx g ci-workflow
```

[Learn more about Nx on CI](https://nx.dev/ci/intro/ci-with-nx#ready-get-started-with-your-provider?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)

## Install Nx Console

Nx Console is an editor extension that enriches your developer experience. It lets you run tasks, generate code, and improves code autocompletion in your IDE. It is available for VSCode and IntelliJ.

[Install Nx Console &raquo;](https://nx.dev/getting-started/editor-setup?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)

## Useful links

Learn more:

- [Learn more about this workspace setup](https://nx.dev/getting-started/tutorials/angular-monorepo-tutorial?utm_source=nx_project&amp;utm_medium=readme&amp;utm_campaign=nx_projects)
- [Learn about Nx on CI](https://nx.dev/ci/intro/ci-with-nx?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)
- [Releasing Packages with Nx release](https://nx.dev/features/manage-releases?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)
- [What are Nx plugins?](https://nx.dev/concepts/nx-plugins?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)

And join the Nx community:
- [Discord](https://go.nx.dev/community)
- [Follow us on X](https://twitter.com/nxdevtools) or [LinkedIn](https://www.linkedin.com/company/nrwl)
- [Our Youtube channel](https://www.youtube.com/@nxdevtools)
- [Our blog](https://nx.dev/blog?utm_source=nx_project&utm_medium=readme&utm_campaign=nx_projects)
