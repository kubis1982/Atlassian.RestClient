# Atlassian Jira REST Client

Strongly-typed REST client for **Atlassian Jira Cloud API v3**, automatically generated using [Microsoft Kiota](https://github.com/microsoft/kiota) from the official OpenAPI specification.

## Package Information

- **Package ID:** `Kubis1982.Atlassian.Jira.RestClient.v3`
- **NuGet:** [![NuGet](https://img.shields.io/nuget/v/Kubis1982.Atlassian.Jira.RestClient.v3)](https://www.nuget.org/packages/Kubis1982.Atlassian.Jira.RestClient.v3)
- **Current Version:** 1.8453.0

## Features

- ✅ Fully typed C# client for Jira Cloud API v3
- ✅ Generated from official OpenAPI specification
- ✅ Built with Microsoft Kiota code generation tool
- ✅ Support for .NET 10.0+

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Kubis1982.Atlassian.Jira.RestClient.v3
```

### Via Package Manager Console

```powershell
Install-Package Kubis1982.Atlassian.Jira.RestClient.v3
```

### Via .csproj

```xml
<ItemGroup>
    <PackageReference Include="Kubis1982.Atlassian.Jira.RestClient.v3" Version="1.8458.0" />
</ItemGroup>
```

## Usage

### Creating a Client Instance

To use the Jira REST client, you need to create an instance with proper authentication:

#### Basic Authentication (Email + API Token)

```csharp
using Kubis1982.Atlassian.Jira.RestClient;
using System.Text;

// Configuration
var domain = "your-company"; // without .atlassian.net
var email = "your-email@company.com";
var apiToken = "your-api-token"; // Generate from Atlassian Account Settings

// Create HttpClient with Basic Auth
var httpClient = new HttpClient();
var credentials = Convert.ToBase64String(
    Encoding.UTF8.GetBytes($"{email}:{apiToken}"));
httpClient.DefaultRequestHeaders.Authorization = 
    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

// Create request adapter
var requestAdapter = new HttpClientRequestAdapter(httpClient)
{
    BaseUrl = $"https://{domain}.atlassian.net"
};

// Initialize client
var jiraClient = new JiraRestClient(requestAdapter);
```

#### OAuth 2.0 Bearer Token

```csharp
using Kubis1982.Atlassian.Jira.RestClient;

// Configuration
var domain = "your-company"; // without .atlassian.net
var bearerToken = "your-oauth-bearer-token";

// Create HttpClient with Bearer Token
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization = 
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

// Create request adapter
var requestAdapter = new HttpClientRequestAdapter(httpClient)
{
    BaseUrl = $"https://{domain}.atlassian.net"
};

// Initialize client
var jiraClient = new JiraRestClient(requestAdapter);
```

### Example Operations

```csharp
// Get all projects
var projects = await jiraClient.Rest.Api.Three.Project.GetAsync();

// Search for issues
var searchResults = await jiraClient.Rest.Api.Three.Search.GetAsync(requestConfiguration =>
{
    requestConfiguration.QueryParameters.Jql = "project = 'YOUR_PROJECT'";
});

// Get specific issue
var issue = await jiraClient.Rest.Api.Three.Issue["ISSUE-123"].GetAsync();

// Create an issue
var createIssueRequest = new IssueUpdateDetails
{
    Fields = new Dictionary<string, object>
    {
        ["project"] = new { key = "YOUR_PROJECT" },
        ["summary"] = "New issue summary",
        ["description"] = "Issue description",
        ["issuetype"] = new { name = "Task" }
    }
};
var newIssue = await jiraClient.Rest.Api.Three.Issue.PostAsync(createIssueRequest);
```

## OpenAPI Specification

This client is generated from the official Jira Cloud OpenAPI specification:

📄 **Specification URL:**
```
https://dac-static.atlassian.com/cloud/jira/platform/swagger-v3.v3.json?_v=1.8458.0
```

## Repository

For more information, visit the [Atlassian REST Client repository](https://github.com/kubis1982/Atlassian.RestClient).

## License

MIT - See [LICENSE](../../LICENSE) for details.
