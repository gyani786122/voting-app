# 📦 Dependencies & Versions

## Backend Dependencies

### Voting.Api (ASP.NET Core Web API)

```xml
<ItemGroup>
  <!-- EF Core -->
  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.0" />
  
  <!-- SignalR -->
  <PackageReference Include="Microsoft.AspNetCore.SignalR" Version="1.1.0" />
  
  <!-- Swagger -->
  <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.6" />
  
  <!-- Validation -->
  <PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
</ItemGroup>
```

### Voting.Application

```xml
<ItemGroup>
  <PackageReference Include="FluentValidation" Version="11.8.0" />
</ItemGroup>
```

### Voting.Infrastructure

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />
</ItemGroup>
```

### Voting.Application.Tests

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.2" />
  <PackageReference Include="xunit" Version="2.6.4" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.5.4" />
  <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.0" />
</ItemGroup>
```

---

## Frontend Dependencies

### Angular & Core

```json
{
  "@angular/animations": "^17.0.0",
  "@angular/common": "^17.0.0",
  "@angular/compiler": "^17.0.0",
  "@angular/core": "^17.0.0",
  "@angular/forms": "^17.0.0",
  "@angular/material": "^17.0.0",
  "@angular/platform-browser": "^17.0.0",
  "@angular/platform-browser-dynamic": "^17.0.0",
  "@angular/router": "^17.0.0"
}
```

### Real-time Communication

```json
{
  "@microsoft/signalr": "^8.0.0"
}
```

### Reactive Programming

```json
{
  "rxjs": "^7.8.0"
}
```

### Utilities

```json
{
  "tslib": "^2.6.0",
  "zone.js": "^0.14.0"
}
```

### Dev Dependencies

```json
{
  "@angular-devkit/build-angular": "^17.0.0",
  "@angular/cli": "^17.0.0",
  "@angular/compiler-cli": "^17.0.0",
  "@types/jasmine": "~5.1.0",
  "jasmine-core": "~5.1.0",
  "karma": "~6.4.0",
  "karma-chrome-launcher": "~3.2.0",
  "karma-coverage": "~2.2.0",
  "karma-jasmine": "~5.1.0",
  "karma-jasmine-html-reporter": "~2.1.0",
  "typescript": "~5.2.2"
}
```

---

## System Requirements

### For Backend

- **.NET 8 SDK** (or later)
  - Download: https://dotnet.microsoft.com/download/dotnet/8.0
  - Check version: `dotnet --version`

### For Frontend

- **Node.js 18+** (or later)
  - Download: https://nodejs.org/
  - Check version: `node --version`
  - npm comes with Node.js

### Operating System

- ✅ Windows 10/11 (Primary Development Target)
- ✅ macOS
- ✅ Linux

### Browser Support (Frontend)

- ✅ Chrome (Latest)
- ✅ Edge (Latest)
- ✅ Firefox (Latest)
- ✅ Safari (Latest)

---

## Installation Instructions

### Backend Setup

1. **Install .NET 8 SDK**
   ```bash
   # Verify installation
   dotnet --version  # Should be 8.0.x
   ```

2. **Restore NuGet Packages**
   ```bash
   cd "d:\MY PROJECTS\MY\Voting App"
   dotnet restore
   ```

3. **Verify Build**
   ```bash
   dotnet build
   ```

### Frontend Setup

1. **Install Node.js**
   ```bash
   # Verify installation
   node --version    # Should be v18.x or higher
   npm --version     # Should be 8.x or higher
   ```

2. **Install npm Dependencies**
   ```bash
   cd frontend
   npm install
   ```

3. **Verify Angular CLI** (optional but recommended)
   ```bash
   npm install -g @angular/cli@17
   ng version
   ```

---

## Dependency Purposes

### Backend

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.EntityFrameworkCore | 8.0.0 | ORM for database access |
| Microsoft.EntityFrameworkCore.Sqlite | 8.0.0 | SQLite provider for EF |
| Microsoft.AspNetCore.SignalR | 1.1.0 | Real-time communication |
| Swashbuckle.AspNetCore | 6.4.6 | Swagger/OpenAPI documentation |
| FluentValidation | 11.8.0 | Request validation |
| xunit | 2.6.4 | Unit testing framework |
| Microsoft.EntityFrameworkCore.InMemory | 8.0.0 | In-memory DB for tests |

### Frontend

| Package | Version | Purpose |
|---------|---------|---------|
| @angular/core | 17.0.0 | Core Angular framework |
| @angular/forms | 17.0.0 | Reactive forms |
| @angular/material | 17.0.0 | Material Design UI |
| @microsoft/signalr | 8.0.0 | SignalR client |
| rxjs | 7.8.0 | Reactive programming |
| typescript | 5.2.2 | TypeScript compiler |

---

## Version Compatibility

### Verified Combinations

- **ASP.NET Core 8.0** ✅ with **Entity Framework Core 8.0**
- **Angular 17.0** ✅ with **Angular Material 17.0**
- **TypeScript 5.2** ✅ with **Angular 17.0**
- **Node.js 18+** ✅ with **npm 8+**
- **.NET 8 SDK** ✅ with **SQLite 3**
- **@microsoft/signalr 8.0** ✅ with **ASP.NET Core SignalR 1.1**

---

## License Information

### Backend Packages

- **EntityFrameworkCore** - Apache 2.0
- **FluentValidation** - Apache 2.0
- **Swashbuckle.AspNetCore** - MIT
- **xunit** - Apache 2.0

### Frontend Packages

- **Angular** - MIT
- **Angular Material** - MIT
- **RxJS** - Apache 2.0
- **@microsoft/signalr** - MIT
- **TypeScript** - Apache 2.0

---

## Optional Tools (Recommended)

### Backend Development

- **Visual Studio 2022** (Community, Professional, or Enterprise)
  - Includes .NET development tools
  - Download: https://visualstudio.microsoft.com/downloads/

- **SQL Server Management Studio** (Optional)
  - For database administration
  - Download: https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms

### Frontend Development

- **Visual Studio Code**
  - Lightweight, free code editor
  - Download: https://code.visualstudio.com/

- **Recommended VS Code Extensions**:
  - Angular Language Service
  - Prettier - Code formatter
  - ESLint
  - Material Icon Theme

---

## Troubleshooting Dependency Issues

### Backend

**Issue**: "The type initializer for 'System.Text.Json.JsonSerializerOptions' threw an exception"
```bash
# Solution: Update to latest stable .NET
dotnet tool update --global dotnet-tools
```

**Issue**: "Package not found"
```bash
# Solution: Clear NuGet cache and restore
dotnet nuget locals all --clear
dotnet restore
```

### Frontend

**Issue**: "npm ERR! code ERESOLVE"
```bash
# Solution: Use legacy peer deps flag
npm install --legacy-peer-deps
```

**Issue**: "Angular CLI not found"
```bash
# Solution: Install globally
npm install -g @angular/cli@17
```

---

## Updating Dependencies

### Backend (Safe Update)

```bash
# Check available updates
dotnet package search [package-name]

# Update specific package
dotnet add package [package-name] --version [version]

# Update all packages
dotnet upgrade
```

### Frontend (Safe Update)

```bash
# Check for outdated packages
npm outdated

# Update specific package
npm install [package-name]@[version]

# Update all to latest minor/patch
npm update
```

---

## Deployment Considerations

### Backend - Production Dependencies

You may want to add:

```xml
<!-- Logging -->
<PackageReference Include="Serilog" Version="3.x" />
<PackageReference Include="Serilog.AspNetCore" Version="7.x" />

<!-- Authentication (if needed) -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0" />

<!-- CORS advanced -->
<PackageReference Include="CorsOptions" Version="..." />

<!-- For SQL Server (production) -->
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
```

### Frontend - Production Dependencies

You may want to add:

```json
{
  "@sentry/angular": "^7.x",         // Error tracking
  "ngx-translate/core": "^14.x",     // i18n support
  "@ngrx/store": "^17.x",            // Advanced state management
  "chart.js": "^4.x",                // Charts (for voting analytics)
  "ngx-pagination": "^16.x"          // Pagination
}
```

---

**Current Setup**: All dependencies are up-to-date as of December 2025 ✅
