# GitHub NuGet Publishing Implementation Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Build and publish the single YFinance.NET NuGet package to GitHub Packages on release tags.

**Architecture:** Make one package (`YFinance.NET.Implementation`) with shared metadata in `Directory.Build.props`, set its `PackageId` to `YFinance.NET`, and add a GitHub Actions workflow that packs and pushes to GitHub Packages using the repo owner and the built-in `GITHUB_TOKEN` when a `v*` tag is created.

**Tech Stack:** .NET SDK 10, GitHub Actions, NuGet

### Task 1: Add shared package metadata and package ID

**Files:**
- Create: `Directory.Build.props`
- Modify: `YFinance.NET.Implementation/YFinance.NET.Implementation.csproj`
- Modify: `YFinance.NET.Tests/YFinance.NET.Tests.csproj`

**Step 1: Capture current pack output (baseline)**

Run: `dotnet pack YFinance.NET.Implementation/YFinance.NET.Implementation.csproj -c Release`
Expected: Package builds but lacks expected metadata or versioning controls.

**Step 2: Add shared metadata in `Directory.Build.props`**

```xml
<Project>
  <PropertyGroup>
    <Authors>YFinance.NET</Authors>
    <Company>YFinance.NET</Company>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
    <RepositoryUrl>https://github.com/<owner>/<repo></RepositoryUrl>
    <RepositoryType>git</RepositoryType>
    <PackageProjectUrl>https://github.com/<owner>/<repo></PackageProjectUrl>
    <PackageTags>yfinance finance stocks quotes</PackageTags>
    <GenerateDocumentationFile>true</GenerateDocumentationFile>
    <ContinuousIntegrationBuild>true</ContinuousIntegrationBuild>
    <VersionPrefix>0.1.0</VersionPrefix>
  </PropertyGroup>
</Project>
```

**Step 3: Add `PackageId` and `Description` for the single package**

```xml
<PropertyGroup>
  <PackageId>YFinance.NET</PackageId>
  <Description>Yahoo Finance data client for .NET.</Description>
</PropertyGroup>
```

**Step 4: Ensure tests are not packed**

```xml
<PropertyGroup>
  <IsPackable>false</IsPackable>
</PropertyGroup>
```

**Step 5: Re-run pack to verify metadata appears**

Run: `dotnet pack YFinance.NET.Implementation/YFinance.NET.Implementation.csproj -c Release`
Expected: `YFinance.NET.<version>.nupkg` exists and includes metadata fields.

**Step 6: Commit**

```bash
git add Directory.Build.props YFinance.NET.Implementation/YFinance.NET.Implementation.csproj YFinance.NET.Tests/YFinance.NET.Tests.csproj
git commit -m "chore: add nuget package metadata"
```

### Task 2: Add GitHub Actions workflow to publish packages

**Files:**
- Create: `.github/workflows/publish-packages.yml`

**Step 1: Add workflow for tag-based publishing**

```yaml
name: Publish NuGet Packages

on:
  push:
    tags:
      - "v*"
  workflow_dispatch:

permissions:
  contents: read
  packages: write

jobs:
  publish:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v4

      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.0.x'

      - name: Set version from tag
        id: version
        run: |
          VERSION=${GITHUB_REF#refs/tags/v}
          echo "version=$VERSION" >> $GITHUB_OUTPUT

      - name: Restore
        run: dotnet restore

      - name: Pack
        run: dotnet pack YFinance.NET.Implementation/YFinance.NET.Implementation.csproj -c Release -p:Version=${{ steps.version.outputs.version }}

      - name: Add GitHub Packages source
        run: dotnet nuget add source --username ${{ github.repository_owner }} --password ${{ secrets.GITHUB_TOKEN }} --store-password-in-clear-text --name github "https://nuget.pkg.github.com/${{ github.repository_owner }}/index.json"

      - name: Push packages
        run: dotnet nuget push "**/*.nupkg" --source github --skip-duplicate
```

**Step 2: Validate workflow YAML**

Run: `rg -n "publish-packages" .github/workflows/publish-packages.yml`
Expected: New workflow is present and matches the intended tag trigger.

**Step 3: Commit**

```bash
git add .github/workflows/publish-packages.yml
git commit -m "ci: publish nuget packages to github"
```

### Task 3: Document publishing and usage

**Files:**
- Modify: `README.md`

**Step 1: Add GitHub Packages usage notes**

```markdown
### Install from GitHub Packages

1. Authenticate to GitHub Packages.
2. Add the source: `https://nuget.pkg.github.com/<owner>/index.json`
3. Install: `dotnet add package YFinance.NET --version <version>`
```

**Step 2: Verify markdown renders**

Run: `rg -n "Install from GitHub Packages" README.md`
Expected: New section appears once.

**Step 3: Commit**

```bash
git add README.md
git commit -m "docs: add github packages install notes"
```

### Task 4: Manual verification run

**Files:**
- None

**Step 1: Run pack for all three packages**

Run: `dotnet pack YFinance.NET.Implementation/YFinance.NET.Implementation.csproj -c Release`
Expected: `YFinance.NET.<version>.nupkg` in `YFinance.NET.Implementation/bin/Release`.

**Step 2: (Optional) Dry-run push with a local source**

Run: `dotnet nuget add source ./local-packages --name local`
Expected: Local source registered without errors.

**Step 3: Commit**

No commit required.
