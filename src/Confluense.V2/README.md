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

## Usage

### Creating a Client Instance

To use the Confluence REST client, you need to create an instance with proper authentication:

#### Basic Authentication (Email + API Token) - Recommended

```csharp
using Kubis1982.Atlassian.Confluense.RestClient;
using Kubis1982.Atlassian.RestClient;

// Configuration
var domain = "your-company"; // without .atlassian.net
var email = "your-email@company.com";
var apiToken = "your-api-token"; // Generate from https://id.atlassian.com/manage-profile/security/api-tokens

// Create authentication provider
var basicAuthProvider = new BasicAuthProvider(email, apiToken);

// Initialize client
var confluenceClient = ConfluenseRestClient.Create(basicAuthProvider);
```

**Note:** The `BasicAuthProvider` is provided by the `Kubis1982.Atlassian.RestClient` package and is automatically included as a dependency.

### Example Operations

```csharp
// Get pages with limit
var pages = await confluenceClient.Pages.GetAsPagesGetResponseAsync(config =>
{
    config.QueryParameters.Limit = 10;
});

Console.WriteLine($"Found {pages?.Results?.Count} pages");

foreach (var page in pages?.Results ?? [])
{
    Console.WriteLine($"- {page.Title} (ID: {page.Id})");
}

// Get specific page by ID
var page = await confluenceClient.Pages["123456"].GetAsync();
Console.WriteLine($"Page: {page?.Title}");

// Search pages by title
var searchResults = await confluenceClient.Pages.GetAsPagesGetResponseAsync(config =>
{
    config.QueryParameters.Title = "Meeting Notes";
    config.QueryParameters.Limit = 5;
});

// Get spaces
var spaces = await confluenceClient.Spaces.GetAsSpacesGetResponseAsync(config =>
{
    config.QueryParameters.Limit = 10;
});

foreach (var space in spaces?.Results ?? [])
{
    Console.WriteLine($"Space: {space.Name} (Key: {space.Key})");
}
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
