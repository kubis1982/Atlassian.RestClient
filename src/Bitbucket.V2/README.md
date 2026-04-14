# Atlassian Bitbucket REST Client

Strongly-typed REST client for **Atlassian Bitbucket Cloud API v2**, automatically generated using [Microsoft Kiota](https://github.com/microsoft/kiota) from the official OpenAPI specification.

## Package Information

- **Package ID:** `Kubis1982.Atlassian.Bitbucket.RestClient.v2`
- **NuGet:** [![NuGet](https://img.shields.io/nuget/v/Kubis1982.Atlassian.Bitbucket.RestClient.v2)](https://www.nuget.org/packages/Kubis1982.Atlassian.Bitbucket.RestClient.v2)
- **Current Version:** 2.300.163

## Features

- ✅ Fully typed C# client for Bitbucket Cloud API v2
- ✅ Generated from official OpenAPI specification
- ✅ Built with Microsoft Kiota code generation tool
- ✅ Support for .NET 10.0+

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Kubis1982.Atlassian.Bitbucket.RestClient.v2
```

### Via Package Manager Console

```powershell
Install-Package Kubis1982.Atlassian.Bitbucket.RestClient.v2
```

### Via .csproj

```xml
<ItemGroup>
    <PackageReference Include="Kubis1982.Atlassian.Bitbucket.RestClient.v2" Version="2.300.163" />
</ItemGroup>
```

## Usage

### Creating a Client Instance

To use the Bitbucket REST client, you need to create an instance with proper authentication:

#### Basic Authentication (Username + App Password)

```csharp
using Kubis1982.Atlassian.Bitbucket.RestClient;
using System.Text;

// Configuration
var username = "your-username";
var appPassword = "your-app-password"; // Generate from Bitbucket Settings > App passwords

// Create HttpClient with Basic Auth
var httpClient = new HttpClient();
var credentials = Convert.ToBase64String(
    Encoding.UTF8.GetBytes($"{username}:{appPassword}"));
httpClient.DefaultRequestHeaders.Authorization = 
    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

// Create request adapter
var requestAdapter = new HttpClientRequestAdapter(httpClient)
{
    BaseUrl = "https://api.bitbucket.org/2.0"
};

// Initialize client
var bitbucketClient = new BitbucketRestClient(requestAdapter);
```

#### OAuth 2.0 Bearer Token

```csharp
using Kubis1982.Atlassian.Bitbucket.RestClient;

// Configuration
var bearerToken = "your-oauth-bearer-token";

// Create HttpClient with Bearer Token
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Authorization = 
    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);

// Create request adapter
var requestAdapter = new HttpClientRequestAdapter(httpClient)
{
    BaseUrl = "https://api.bitbucket.org/2.0" // default base URL for Bitbucket API, can be overridden if needed
};

// Initialize client
var bitbucketClient = new BitbucketRestClient(requestAdapter);
```

### Example Operations

```csharp
// Get user repositories
var repositories = await bitbucketClient.Repositories["your-username"].GetAsync();

// Get specific repository
var repo = await bitbucketClient.Repositories["your-username"]["repo-name"].GetAsync();

// Get repository commits
var commits = await bitbucketClient.Repositories["your-username"]["repo-name"].Commits.GetAsync();

// Get pull requests
var pullRequests = await bitbucketClient.Repositories["your-username"]["repo-name"].Pullrequests.GetAsync();

// Get repository branches
var branches = await bitbucketClient.Repositories["your-username"]["repo-name"].Refs.Branches.GetAsync();
```

## OpenAPI Specification

This client is generated from the official Bitbucket Cloud OpenAPI specification:

📄 **Specification URL:**
```
https://dac-static.atlassian.com/cloud/bitbucket/swagger.v3.json?_v=2.300.163
```

## Repository

For more information, visit the [Atlassian REST Client repository](https://github.com/kubis1982/Atlassian.RestClient).

## License

MIT - See [LICENSE](../../LICENSE) for details.
