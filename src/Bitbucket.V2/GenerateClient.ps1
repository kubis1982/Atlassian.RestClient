#!/usr/bin/env pwsh
<#
.SYNOPSIS
    Generates Bitbucket REST client using Microsoft Kiota from OpenAPI specification.

.DESCRIPTION
    This script automates the generation of a strongly-typed Bitbucket REST client.
    It downloads the latest OpenAPI spec from Bitbucket, validates it, and generates
    C# client code using Kiota.

.EXAMPLE
    .\generate-client.ps1
    Generates the client with default settings.

.NOTES
    Requires: Microsoft.OpenApi.Kiota dotnet tool (installed locally)
#>

[CmdletBinding()]
param()

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

# Configuration
$SwaggerUrl = "https://dac-static.atlassian.com/cloud/bitbucket/swagger.v3.json?_v=2.300.163"
$OutputDir = "RestClient"
$Namespace = "Kubis1982.Atlassian.Bitbucket.RestClient"

Write-Host "================================================"   -ForegroundColor Cyan
Write-Host "  Bitbucket REST client Generator (Kiota)"          -ForegroundColor Cyan
Write-Host "================================================"   -ForegroundColor Cyan
Write-Host ""

# Step 1: Restore dotnet tools
Write-Host "[1/3] Restoring dotnet tools..." -ForegroundColor Yellow
try {
    dotnet tool restore 2>&1 | Out-Null
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to restore dotnet tools"
    }
    Write-Host "✓ Tools restored successfully" -ForegroundColor Green
} catch {
    Write-Error "Failed to restore dotnet tools: $_"
    exit 1
}

# Step 2: Clean output directory
Write-Host ""
Write-Host "[2/3] Preparing output directory..." -ForegroundColor Yellow

if (Test-Path $OutputDir) {
    Write-Host "  Cleaning existing generated code..." -ForegroundColor Gray
    Remove-Item -Path $OutputDir -Recurse -Force
}

Write-Host "✓ Output directory ready: $OutputDir" -ForegroundColor Green

# Step 3: Generate client with Kiota
Write-Host ""
Write-Host "[3/3] Generating Bitbucket Rest client..." -ForegroundColor Yellow
Write-Host "  Language: C#" -ForegroundColor Gray
Write-Host "  Namespace: $Namespace" -ForegroundColor Gray

try {
    $kiotaArgs = @(
        "generate"
        "--language", "CSharp"
        "--openapi", $SwaggerUrl
        "--output", $OutputDir
        "--namespace-name", $Namespace
        "--class-name", "BitbucketRestClient"
        "--clean-output"
        "--clear-cache"
    )

    Write-Host ""
    Write-Host "  Executing: dotnet kiota $($kiotaArgs -join ' ')" -ForegroundColor Gray
    Write-Host ""

    & dotnet kiota @kiotaArgs

    if ($LASTEXITCODE -ne 0) {
        throw "Kiota generation failed with exit code $LASTEXITCODE"
    }

    # Count generated files
    $GeneratedFiles = Get-ChildItem -Path $OutputDir -Recurse -File | Measure-Object
    Write-Host ""
    Write-Host "✓ Client generated successfully ($($GeneratedFiles.Count) files)" -ForegroundColor Green

} catch {
    Write-Error "Failed to generate client: $_"
    exit 1
}

# Summary
Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Generation Complete!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "Generated files location: $OutputDir" -ForegroundColor White
Write-Host "Next steps:" -ForegroundColor White
Write-Host "  1. Review generated code in $OutputDir" -ForegroundColor Gray
Write-Host "  2. Add required NuGet packages to the project" -ForegroundColor Gray
Write-Host "  3. Update BitbucketRestClient to use generated client" -ForegroundColor Gray
Write-Host ""
