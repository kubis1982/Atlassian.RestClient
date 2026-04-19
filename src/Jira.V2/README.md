# Atlassian Jira REST Client

Strongly-typed REST client for **Atlassian Jira Cloud API v3**, automatically generated using [Microsoft Kiota](https://github.com/microsoft/kiota) from the official OpenAPI specification.

## Package Information

- **Package ID:** `Kubis1982.Atlassian.Jira.RestClient.v2`
  - **NuGet:** [![NuGet](https://img.shields.io/nuget/v/Kubis1982.Atlassian.Jira.RestClient.v2)](https://www.nuget.org/packages/Kubis1982.Atlassian.Jira.RestClient.v2)
- **Current Version:** 1.8464.0

## Features

- ✅ Fully typed C# client for Jira Cloud API v2
- ✅ Generated from official OpenAPI specification
- ✅ Built with Microsoft Kiota code generation tool
- ✅ Support for .NET 10.0+

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Kubis1982.Atlassian.Jira.RestClient.v2
```

### Via Package Manager Console

```powershell
Install-Package Kubis1982.Atlassian.Jira.RestClient.v2
```

### Via .csproj

```xml
<ItemGroup>
    <PackageReference Include="Kubis1982.Atlassian.Jira.RestClient.v2" Version="1.8464.0" />
</ItemGroup>
```

## Usage

### Creating a Client Instance

To use the Jira REST client, you need to create an instance with proper authentication:

#### Basic Authentication (Email + API Token) - Recommended

```csharp
using Kubis1982.Atlassian.Jira.RestClient.V2;
using Kubis1982.Atlassian.Jira.RestClient.V2.Models;
using Kubis1982.Atlassian.RestClient;

// Configuration
var domain = "your-company"; // without .atlassian.net
var email = "your-email@company.com";
var apiToken = "your-api-token"; // Generate from https://id.atlassian.com/manage-profile/security/api-tokens

// Create authentication provider
var basicAuthProvider = new BasicAuthProvider(email, apiToken);

// Initialize client
var jiraClient = JiraRestClient.Create(domain, basicAuthProvider);
```

**Note:** The `BasicAuthProvider` is provided by the `Kubis1982.Atlassian.RestClient` package and is automatically included as a dependency.

### Example Operations

```csharp
// Get current user information
var myself = await jiraClient.Rest.Api.Two.Myself.GetAsync();
Console.WriteLine($"User: {myself?.DisplayName} ({myself?.EmailAddress})");

// Add worklog to an issue
try
{
    var worklog = await jiraClient.Rest.Api.Two.Issue["PROJECT-123"].Worklog.PostAsync(new Worklog
    {
        TimeSpentSeconds = 120,
        Started = DateTimeOffset.UtcNow,
        Comment = "Completed task"
    });

    Console.WriteLine($"Worklog created with ID: {worklog?.Id}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error creating worklog: {ex.Message}");
}

// Get issue details
var issue = await jiraClient.Rest.Api.Two.Issue["PROJECT-123"].GetAsync();
Console.WriteLine($"Issue: {issue?.Key} - {issue?.Fields?.Summary}");

// Search for issues
var searchResults = await jiraClient.Rest.Api.Two.Search.GetAsync(config =>
{
    config.QueryParameters.Jql = "project = 'YOUR_PROJECT' AND status = 'In Progress'";
    config.QueryParameters.MaxResults = 50;
});

foreach (var issue in searchResults?.Issues ?? [])
{
    Console.WriteLine($"- {issue.Key}: {issue.Fields?.Summary}");
}
```

### Custom DateTime Serialization

The client automatically handles Jira's specific date-time format (`2026-04-18T09:04:00.1182+0000`) through the `Kubis1982.Atlassian.RestClient` package. No additional configuration is needed when using `JiraRestClient.Create()`.

## OpenAPI Specification

This client is generated from the official Jira Cloud OpenAPI specification:

📄 **Specification URL:**
```
https://dac-static.atlassian.com/cloud/jira/platform/swagger.v3.json?_v=1.8464.0
```

## Repository

For more information, visit the [Atlassian REST Client repository](https://github.com/kubis1982/Atlassian.RestClient).

## License

MIT - See [LICENSE](../../LICENSE) for details.
