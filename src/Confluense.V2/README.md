# Atlassian Confluence REST Client

Strongly-typed REST client for **Atlassian Confluence Cloud API v2**, automatically generated using [Microsoft Kiota](https://github.com/microsoft/kiota) from the official OpenAPI specification.

## Package Information

- **Package ID:** `Kubis1982.Atlassian.Confluense.RestClient.v2`
- **NuGet:** [![NuGet](https://img.shields.io/nuget/v/Kubis1982.Atlassian.Confluense.RestClient.v2)](https://www.nuget.org/packages/Kubis1982.Atlassian.Confluense.RestClient.v2)
- **Current Version:** 1.8453.0

## Features

- ✅ Fully typed C# client for Confluence Cloud API v2
- ✅ Generated from official OpenAPI specification
- ✅ Built with Microsoft Kiota code generation tool
- ✅ Support for .NET 10.0+

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Kubis1982.Atlassian.Confluense.RestClient.v2
```

### Via Package Manager Console

```powershell
Install-Package Kubis1982.Atlassian.Confluense.RestClient.v2
```

### Via .csproj

```xml
<ItemGroup>
    <PackageReference Include="Kubis1982.Atlassian.Confluense.RestClient.v2" Version="1.8458.0" />
</ItemGroup>
```

## Usage

### Creating a Client Instance

To use the Confluence REST client, you need to create an instance with proper authentication:

#### Basic Authentication (Email + API Token) - Recommended

```csharp
using Kubis1982.Atlassian.Confluense.RestClient;
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
    BaseUrl = $"https://{domain}.atlassian.net/wiki/api/v2"
};

// Initialize client
var confluenceClient = new ConfluenseRestClient(requestAdapter);

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

### Example Operations

```csharp
// Get pages with limit
var pages = await confluenceClient.Pages.GetAsPagesGetResponseAsync(requestConfiguration =>
{
    requestConfiguration.QueryParameters.Limit = 5;
});
Console.WriteLine($"Found {pages?.Results?.Count} pages");

// Get all spaces
var spaces = await confluenceClient.Spaces.GetAsync();

// Get pages in a specific space
var spacePages = await confluenceClient.Pages.GetAsync(requestConfiguration =>
{
    requestConfiguration.QueryParameters.SpaceId = "your-space-id";
});

// Get specific page
var page = await confluenceClient.Pages["page-id"].GetAsync();

// Get page content with specific format
var content = await confluenceClient.Pages["page-id"].GetAsync(requestConfiguration =>
{
    requestConfiguration.QueryParameters.BodyFormat = "atlas_doc_format";
});
```

## OpenAPI Specification

This client is generated from the official Confluence Cloud OpenAPI specification:

📄 **Specification URL:**
```
https://dac-static.atlassian.com/cloud/confluence/openapi-v2.v3.json?_v=1.8458.0
```

## Repository

For more information, visit the [Atlassian REST Client repository](https://github.com/kubis1982/Atlassian.RestClient).

## License

MIT - See [LICENSE](../../LICENSE) for details.
