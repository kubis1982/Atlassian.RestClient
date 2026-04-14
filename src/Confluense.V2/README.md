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

#### Basic Authentication (Email + API Token)

```csharp
using Kubis1982.Atlassian.Confluense.RestClient;
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
    BaseUrl = $"https://{domain}.atlassian.net/wiki/api/v2"
};

// Initialize client
var confluenceClient = new ConfluenseRestClient(requestAdapter);
```

#### OAuth 2.0 Bearer Token

```csharp
using Kubis1982.Atlassian.Confluense.RestClient;

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
    BaseUrl = $"https://{domain}.atlassian.net/wiki/api/v2"
};

// Initialize client
var confluenceClient = new ConfluenseRestClient(requestAdapter);
```

### Example Operations

```csharp
// Get all spaces
var spaces = await confluenceClient.Spaces.GetAsync();

// Get pages in a space
var pages = await confluenceClient.Pages.GetAsync(requestConfiguration =>
{
    requestConfiguration.QueryParameters.SpaceId = "your-space-id";
});

// Get specific page
var page = await confluenceClient.Pages["page-id"].GetAsync();

// Get page content
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
