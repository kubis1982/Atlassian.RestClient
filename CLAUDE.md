# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository overview

Collection of strongly-typed C# REST clients for Atlassian Cloud APIs (Jira v2, Jira v3, Bitbucket v2, Confluence v2), generated with [Microsoft Kiota](https://github.com/microsoft/kiota) from Atlassian's official OpenAPI specs. Each API surface ships as its own NuGet package, all depending on a shared core library.

## Solution structure

- `Atlassian.RestClient.slnx` — solution file (new XML-based `.slnx` format, not `.sln`).
- `src/RestClient` — **core shared library** (`Kubis1982.Atlassian.RestClient`). Contains `BasicAuthProvider` (Basic Auth for email+API token / username+app password), `HttpClientRequestAdapterFactory` (builds a Kiota `HttpClientRequestAdapter` with Atlassian-specific JSON config), and `DateTimeOffsetConverter` (handles Atlassian's non-standard `yyyy-MM-ddTHH:mm:ss.ffff+0000` date format). Every other package references this project.
- `src/Jira.V2`, `src/Jira.V3`, `src/Bitbucket.V2`, `src/Confluense.V2` — one project per API/version. Each contains:
  - A hand-written `partial class` (`JiraRestClient.cs`, `BitbucketRestClient.cs`, etc.) with a static `Create(...)` factory method that wires up the base URL + auth provider via `HttpClientRequestAdapterFactory`. **This is the only hand-written source file per project** — do not edit files under its `RestClient/` subfolder.
  - `RestClient/` — Kiota-generated code (the other half of the `partial class`, request builders, models). Regenerated wholesale, never hand-edited.
  - `GenerateClient.ps1` / `.bat` — pulls the current OpenAPI spec URL and regenerates `RestClient/` via `dotnet kiota generate`.
- `tests/*CommandLine*` — small console apps (not automated tests) used to manually exercise a client end-to-end against a real Atlassian instance. Each has hardcoded `domain`/`email`/`apiToken` placeholders that must be filled in locally before running; never commit real credentials there.
- `Directory.Build.props` — shared MSBuild settings for all projects (`net10.0`, nullable enabled, deterministic/CI build flags, NuGet packaging metadata).

Naming note: the Confluence project/namespace is spelled `Confluense` (not `Confluence`) throughout the repo — match this exactly when referencing it.

## Common commands

```powershell
# Restore, build, test the whole solution
dotnet restore Atlassian.RestClient.slnx
dotnet build Atlassian.RestClient.slnx

# Build/pack a single package (mirrors what CI does)
dotnet build src/Jira.V3/Kubis1982.Atlassian.Jira.RestClient.csproj --configuration Release
dotnet pack src/Jira.V3/Kubis1982.Atlassian.Jira.RestClient.csproj --configuration Release --output ./nupkg

# Run a manual smoke-test console app (fill in credentials in Program.cs first)
dotnet run --project tests/JiraCommandLineV3
```

There are no automated unit tests in this repo — verification is via `dotnet build` and manual runs of the `tests/*CommandLine*` console apps against a real Atlassian tenant.

### Regenerating a client from the OpenAPI spec

Run from inside the relevant `src/<Project>` directory (requires the `microsoft.openapi.kiota` local dotnet tool, restored via `dotnet tool restore` — see `.config/dotnet-tools.json`):

```powershell
cd src/Jira.V3
./GenerateClient.ps1
```

This deletes and regenerates the `RestClient/` subfolder from the OpenAPI URL hardcoded near the top of the script. When bumping to a new spec version, update the `$SwaggerUrl` version query param (and the matching `<Version>` in the project's `.csproj`) before regenerating, then verify the hand-written `partial class` file still compiles against the newly generated API surface.

## Publishing

Each package publishes independently via its own manually-triggered (`workflow_dispatch`) GitHub Actions workflow under `.github/workflows/` (`publish-jira-v2.yml`, `publish-jira-v3.yml`, `publish-bitbucket.yml`, `publish-confluense.yml`, `publish-restclient.yml`). Each builds, packs, and pushes only its own project to NuGet — there is no coordinated multi-package release step, so bumping the core `RestClient` package's version does not automatically bump or republish the dependent Jira/Bitbucket/Confluence packages.
