# Atlassian REST Client - Core Library

Core shared components for **Atlassian REST API Clients**, providing common authentication, serialization, and HTTP client configuration utilities for Jira, Confluence, and Bitbucket API clients.

## Package Information

- **Package ID:** `Kubis1982.Atlassian.RestClient`
- **NuGet:** [![NuGet](https://img.shields.io/nuget/v/Kubis1982.Atlassian.RestClient)](https://www.nuget.org/packages/Kubis1982.Atlassian.RestClient)

## Features

- ✅ **BasicAuthProvider** - Simple Basic Authentication implementation for Atlassian Cloud APIs
- ✅ **HttpClientRequestAdapterFactory** - Factory for creating pre-configured HTTP adapters
- ✅ **DateTimeOffsetConverter** - Custom JSON converter for Atlassian-specific date-time formats
- ✅ Built on [Microsoft Kiota](https://github.com/microsoft/kiota)
- ✅ Support for .NET 10.0+

## Installation

### Via NuGet Package Manager

```bash
dotnet add package Kubis1982.Atlassian.RestClient
```

### Via Package Manager Console

```powershell
Install-Package Kubis1982.Atlassian.RestClient
```

### Via .csproj

```xml
<ItemGroup>
    <PackageReference Include="Kubis1982.Atlassian.RestClient" Version="1.0.0" />
</ItemGroup>
```

## Components

### BasicAuthProvider

Implements `IAuthenticationProvider` for Basic Authentication using username/email and password/API token.

**Usage:**

```csharp
using Kubis1982.Atlassian.RestClient;

// Create with email and API token (recommended for Atlassian Cloud)
var authProvider = new BasicAuthProvider("your-email@company.com", "your-api-token");

// Or with username and app password (Bitbucket)
var authProvider = new BasicAuthProvider("username", "app-password");
```

**Parameters:**
- `username` - Email address (for Jira/Confluence) or username (for Bitbucket)
- `password` - API token or app password

### HttpClientRequestAdapterFactory

Factory for creating `HttpClientRequestAdapter` instances with pre-configured JSON serialization that handles Atlassian's specific date-time formats.

**Usage:**

```csharp
using Kubis1982.Atlassian.RestClient;
using Microsoft.Kiota.Abstractions.Authentication;

var authProvider = new BasicAuthProvider("email@company.com", "api-token");
var baseUrl = "https://your-domain.atlassian.net";

// Create adapter with default HttpClient
var adapter = HttpClientRequestAdapterFactory.Create(baseUrl, authProvider);

// Or with custom HttpClient
var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
var adapter = HttpClientRequestAdapterFactory.Create(baseUrl, authProvider, httpClient);
```

**Parameters:**
- `baseUrl` - Base URL for the API (e.g., `https://your-domain.atlassian.net`)
- `authenticationProvider` - Authentication provider instance
- `httpClient` - Optional custom HttpClient instance

**Features:**
- Automatic configuration of custom DateTimeOffset converters
- Pre-configured JSON serialization options
- Support for custom HttpClient instances

### DateTimeOffsetConverter

Custom JSON converter that handles Atlassian's specific date-time format: `2026-04-18T09:04:00.1182+0000`

**Features:**
- Reads multiple date-time format variations
- Writes dates in Atlassian-compatible format (ISO 8601 with timezone offset without colon)
- Handles up to 4 millisecond digits
- Automatic UTC conversion

**Format Specification:**
- Format: `yyyy-MM-ddTHH:mm:ss.ffff+0000`
- Timezone: Always +0000 (UTC)
- Milliseconds: Up to 4 digits, trailing zeros removed

## API Token Generation

### Jira & Confluence
Generate API tokens from: https://id.atlassian.com/manage-profile/security/api-tokens

### Bitbucket
Generate app passwords from: Bitbucket Settings > App passwords

## Repository

For more information, visit the [Atlassian REST Client repository](https://github.com/kubis1982/Atlassian.RestClient).

## License

MIT - See [LICENSE](../../LICENSE) for details.
