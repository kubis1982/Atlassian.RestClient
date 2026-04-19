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
using Kubis1982.Atlassian.RestClient;

// Configuration
var domain = "your-company"; // without .atlassian.net
var email = "your-email@company.com";
var apiToken = "your-api-token"; // Generate from https://id.atlassian.com/manage-profile/security/api-tokens

// Create authentication provider
var basicAuthProvider = new BasicAuthProvider(email, apiToken);

// Initialize client
var bitbucketClient = BitbucketRestClient.Create(basicAuthProvider);
```

**Note:** The `BasicAuthProvider` is provided by the `Kubis1982.Atlassian.RestClient` package and is automatically included as a dependency.

### Example Operations

```csharp
// Get user repositories
var repositories = await bitbucketClient.Repositories[username].GetAsync();

foreach (var repo in repositories?.Values ?? [])
{
    Console.WriteLine($"Repository: {repo.Name} ({repo.FullName})");
}

// Get specific repository
var repo = await bitbucketClient.Repositories[username]["repo-name"].GetAsync();
Console.WriteLine($"Repository: {repo?.Name}");
Console.WriteLine($"Description: {repo?.Description}");
Console.WriteLine($"Language: {repo?.Language}");

// Get repository commits
var commits = await bitbucketClient.Repositories[username]["repo-name"].Commits.GetAsync();

foreach (var commit in commits?.Values ?? [])
{
    Console.WriteLine($"Commit: {commit.Hash} - {commit.Message}");
}

// Get pull requests
var pullRequests = await bitbucketClient.Repositories[username]["repo-name"].Pullrequests.GetAsync();

foreach (var pr in pullRequests?.Values ?? [])
{
    Console.WriteLine($"PR #{pr.Id}: {pr.Title} ({pr.State})");
}

// Get repository branches
var branches = await bitbucketClient.Repositories[username]["repo-name"].Refs.Branches.GetAsync();

foreach (var branch in branches?.Values ?? [])
{
    Console.WriteLine($"Branch: {branch.Name}");
}
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
