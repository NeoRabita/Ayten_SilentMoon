
# 🏗️ Onion Architecture Project Template

A professional, production-ready .NET API template based on **Onion Architecture** principles. This template comes pre-configured with **Entity Framework Core**, **Dapper**, **Unit of Work**, and **Custom Messaging Pipeline Behaviors**.

---

## 🚀 How to Package the Template

To export this project as a reusable `.nupkg` (NuGet package), follow these steps:

### 1. Configuration File
Ensure you have a file named `OnionArchitecture.csproj` in the root of your project folder (next to the `.sln` file) with the following content:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <PackageId>My.Onion.Architecture.Template</PackageId>
    <Version>1.0.0</Version>
    <Authors>Nijat Shahverdiyev</Authors>
    <Description>EF Core and Dapper supported Onion Architecture API Template.</Description>
    <PackageType>Template</PackageType>
    
    <TargetFramework>net8.0</TargetFramework>
    <IncludeContentInPack>true</IncludeContentInPack>
    <IncludeBuildOutput>false</IncludeBuildOutput>
    <GeneratePackageOnBuild>false</GeneratePackageOnBuild>
    <IsPackable>true</IsPackable>
    
    <NoBuild>true</NoBuild>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="**\*" Exclude="**\bin\**;**\obj\**;**\.git\**;**\.vs\**;**\.vscode\**;*.nupkg;*.csproj;**\dist\**" />
    <None Include=".gitignore" Pack="true" PackagePath="content/.gitignore" />
    <Compile Remove="**\*.cs" />
    <None Include="**\*.cs" />
  </ItemGroup>
</Project>
```
### 2. Build the Package

Open your terminal in the root folder and run:


```
# Clean previous build artifacts
dotnet clean

# Pack the template into the /dist folder
dotnet pack OnionArchitecture.csproj -o ./dist
```

## 🛠️ Installation & Setup

### Install the Template

Once the `.nupkg` file is created in the `/dist` folder, install it globally on your machine:


```
dotnet new install ./dist/My.Onion.Architecture.Template.1.0.0.nupkg
```

### Uninstall the Template

If you need to remove or update the template:


```
dotnet new uninstall My.Onion.Architecture.Template
```

## 🏗️ Creating a New Project

To generate a new solution based on this architecture, use the following command:


```
dotnet new myoniontemplate -n [YourProjectName]
```

**Example:**


```
dotnet new myoniontemplate -n SuperMarketApp
```

## 📋 Architecture Overview

-   **Core.Domain:** Contains Enterprise logic, Entities, Enums, and Constants.
    
-   **Core.Application:** Contains Business logic, DTOs, Interfaces, Handlers, and Pipeline Behaviors (Validation, Transactions).
    
-   **Infrastructure.Persistence:** Implementation of Repositories and Unit of Work using **EF Core** for commands and **Dapper** for high-performance queries.
    
-   **Infrastructure.Shared:** External services like Email, Logging, or File Storage.
    
-   **Presentation.WebApi:** REST API Controllers, Middlewares, and Dependency Injection configurations.
    

----------

**Author:** Nijat Shahverdiyev
