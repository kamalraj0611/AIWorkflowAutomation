# AIWorkflowAutomation

AIWorkflowAutomation is a web application designed to handle customer queries using an integrated AI service, manage support tickets, and automate workflows. This application leverages OpenAI's API to process and respond to customer inquiries and integrates various custom services to streamline query handling.

## Table of Contents

1. [Getting Started](#getting-started)
2. [Prerequisites](#prerequisites)
3. [Installation](#installation)
4. [Configuration](#configuration)
   - [Setting up User Secrets](#setting-up-user-secrets)
5. [Running the Application](#running-the-application)
6. [Endpoints](#endpoints)
7. [Project Structure](#project-structure)
8. [Troubleshooting](#troubleshooting)

---

## Getting Started

To get started with AIWorkflowAutomation, follow the steps below to set up and run the application.

## Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio or Visual Studio Code](https://visualstudio.microsoft.com/)
- [Git](https://git-scm.com/) (for cloning the repository)
- OpenAI API Key (for connecting with OpenAI's language model)

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/AIWorkflowAutomation.git
   cd AIWorkflowAutomation
   ```

2. Restore NuGet packages:

   ```bash
   dotnet restore
   ```

3. Build the project:

   ```bash
   dotnet build
   ```

## Configuration

AIWorkflowAutomation requires an API key from OpenAI. Instead of storing this sensitive information in `appsettings.json`, we will use .NET's **User Secrets** feature.

### Setting up User Secrets

1. **Initialize User Secrets** (if not already initialized for the project):

   ```bash
   dotnet user-secrets init
   ```

2. **Add the OpenAI API Key** to User Secrets:

   ```bash
   dotnet user-secrets set "OpenAI:ApiKey" "your-openai-api-key"
   ```

3. Update your `Program.cs` or `Startup.cs` to retrieve the API key from the User Secrets:

   ```csharp
   var builder = WebApplication.CreateBuilder(args);

   // Add services to the container
   builder.Services.AddControllers();

   // Configure OpenAI API key from User Secrets
   string apiKey = builder.Configuration["OpenAI:ApiKey"];
   builder.Services.AddSingleton<ChatGPTService>(new ChatGPTService(apiKey));
   builder.Services.AddSingleton<TicketService>();

   var app = builder.Build();

   // Configure the HTTP request pipeline
   if (!app.Environment.IsDevelopment())
   {
       app.UseExceptionHandler("/Error");
       app.UseHsts();
   }

   app.UseHttpsRedirection();
   app.UseStaticFiles();
   app.UseRouting();
   app.UseAuthorization();
   app.MapControllers();
   app.Run();
   ```

**Note**: If you need to add additional configuration, update your `Program.cs` to retrieve other User Secrets as needed.

## Running the Application

1. **Run the application**:

   ```bash
   dotnet run
   ```

2. By default, the application will start on `https://localhost:7116`. You can access it directly or use tools like Postman to interact with the API endpoints.

## Endpoints

### POST /support/query

- **Description**: Processes a customer query. If the question is a frequently asked question (FAQ), it responds directly; otherwise, it queries OpenAI.
- **URL**: `https://localhost:7116/support/query`
- **Method**: `POST`
- **Request Body**:

   ```json
   {
       "Query": "What are your hours?",
       "Category": "General"
   }
   ```

- **Response**: JSON object containing the AI response or FAQ answer.

### GET /support/report

- **Description**: Retrieves a report of all tickets created during the current session.
- **URL**: `https://localhost:7116/support/report`
- **Method**: `GET`

## Project Structure

- **Controllers**: Contains API controllers, such as `SupportController`, which define the application's endpoints.
- **Models**: Defines data models like `CustomerQuery` and `Ticket`.
- **Services**: Contains business logic for handling queries and managing tickets.
  - `ChatGPTService`: Manages interactions with the OpenAI API.
  - `TicketService`: Handles ticket generation and reporting.

## Troubleshooting

- **API Key Not Found**: Make sure you have set up User Secrets correctly and that your API key is in place. You can verify your secrets by running:

  ```bash
  dotnet user-secrets list
  ```

- **Too Many Requests Error**: OpenAI API has rate limits. If you encounter a `429 Too Many Requests` error, consider implementing a retry mechanism in `ChatGPTService` to handle rate limits with exponential backoff.

