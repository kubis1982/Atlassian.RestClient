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

#### Basic Authentication (Email + API Token) - Recommended

```csharp
using Kubis1982.Atlassian.Jira.RestClient;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using System.Text;

// Configuration
var domain = "your-company"; // without .atlassian.net
var email = "your-email@company.com";
var apiToken = "your-api-token"; // Generate from https://id.atlassian.com/manage-profile/security/api-tokens

// Create authentication provider
var basicAuthProvider = new BasicAuthProvider(email, apiToken);

// Create HttpClient with timeout
var httpClient = new HttpClient()
{
    Timeout = TimeSpan.FromSeconds(30)
};

// Create request adapter
var requestAdapter = new HttpClientRequestAdapter(basicAuthProvider, httpClient: httpClient)
{
    BaseUrl = $"https://{domain}.atlassian.net"
};

// Initialize client
var jiraClient = new JiraRestClient(requestAdapter);

// Custom BasicAuthProvider implementation
public class BasicAuthProvider : IAuthenticationProvider
{
    private readonly string _username;
    private readonly string _appPassword;

    public BasicAuthProvider(string username, string appPassword)
    {
        _username = username ?? throw new ArgumentNullException(nameof(username));
        _appPassword = appPassword ?? throw new ArgumentNullException(nameof(appPassword));
    }

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_appPassword}"));
        request.Headers.Add("Authorization", $"Basic {credentials}");
        return Task.CompletedTask;
    }
}
```

### Custom DateTime Serialization

Jira uses a specific date-time format that requires custom JSON serialization. To properly handle DateTime values, you need to configure the JSON serialization options with the custom converter:

```csharp
using Kubis1982.Atlassian.Jira.RestClient.Serialization;
using Microsoft.Kiota.Serialization.Json;
using System.Text.Json;

// Configure JSON options with custom DateTime converter
var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
jsonOptions.Converters.Add(new JiraDateTimeConverter());
jsonOptions.Converters.Add(new JiraNullableDateTimeConverter());

// Create serialization context
var context = new KiotaJsonSerializationContext(jsonOptions);
var writerFactory = new JsonSerializationWriterFactory(context);

// Create request adapter with custom serialization
var adapter = new HttpClientRequestAdapter(
    authProvider,
    serializationWriterFactory: writerFactory
);

// Initialize client with custom adapter
var jiraClient = new JiraRestClient(adapter);
```

**Why is this needed?**

Jira's API uses a specific date-time format: `2026-04-18T09:04:00.1182+0000` (ISO 8601 with timezone offset without colon). The custom `JiraDateTimeConverter` and `JiraNullableDateTimeConverter` handle both reading and writing dates in this format, ensuring compatibility with Jira's API.

### Example Operations

```csharp
// Get current user information
var myself = await jiraClient.Rest.Api.Three.Myself.GetAsync();
Console.WriteLine($"User: {myself?.DisplayName} ({myself?.EmailAddress})");

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
