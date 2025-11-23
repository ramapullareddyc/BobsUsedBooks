# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (e.g., `net6.0`, `net7.0`, or `net8.0`) rather than .NET Framework (e.g., `net48`).

### 2. Run Unit Tests

Execute the test suite to ensure existing functionality remains intact:

```bash
dotnet test app/Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj --logger "console;verbosity=detailed"
```

Review test results for any failures or warnings that may indicate compatibility issues.

### 3. Check Package Dependencies

List all NuGet package references and verify they are compatible with cross-platform .NET:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages to their latest stable versions that support cross-platform .NET.

### 4. Validate Runtime Compatibility

Build the solution in Release configuration:

```bash
dotnet build --configuration Release
```

Check for any warnings related to platform-specific APIs or deprecated functionality that may have been suppressed in Debug builds.

### 5. Test the Web Application

Run the web application locally:

```bash
dotnet run --project app/Bookstore.Web/Bookstore.Web.csproj
```

Perform the following checks:
- Verify the application starts without errors
- Test critical user workflows and features
- Check database connectivity through Bookstore.Data
- Validate API endpoints if applicable
- Review application logs for warnings or errors

### 6. Review Configuration Files

Examine configuration files for platform-specific settings:
- `appsettings.json` and `appsettings.Development.json`
- Connection strings for database access
- Any file paths that may use Windows-specific separators

Replace hardcoded Windows paths with cross-platform alternatives using `Path.Combine()` or equivalent methods.

### 7. Test on Target Platforms

If the goal is true cross-platform support, test the application on different operating systems:

**Linux:**
```bash
dotnet publish -c Release -r linux-x64 --self-contained false
```

**macOS:**
```bash
dotnet publish -c Release -r osx-x64 --self-contained false
```

**Windows:**
```bash
dotnet publish -c Release -r win-x64 --self-contained false
```

Run the published output on each platform to identify any runtime issues.

### 8. Validate CDK Infrastructure

Since the solution includes a CDK project, verify the infrastructure code:

```bash
cd app/Bookstore.Cdk
dotnet build
cdk synth
```

Review the synthesized CloudFormation template for any issues and ensure all constructs are compatible with the current CDK version.

### 9. Database Migration Verification

If Entity Framework or another ORM is used in Bookstore.Data:

```bash
dotnet ef migrations list --project app/Bookstore.Data/Bookstore.Data.csproj
```

Verify that existing migrations are intact and can be applied to a test database:

```bash
dotnet ef database update --project app/Bookstore.Data/Bookstore.Data.csproj
```

### 10. Performance Testing

Conduct basic performance testing to ensure the migrated application performs comparably to the legacy version:
- Measure application startup time
- Test response times for key operations
- Monitor memory usage during typical workloads

## Final Deployment Preparation

### 1. Create a Deployment Package

Generate a production-ready build:

```bash
dotnet publish app/Bookstore.Web/Bookstore.Web.csproj -c Release -o ./publish
```

### 2. Update Documentation

Document the following:
- New target framework version
- Any breaking changes from the migration
- Updated deployment procedures
- New runtime requirements

### 3. Environment-Specific Configuration

Ensure environment-specific settings are properly configured:
- Production connection strings
- API keys and secrets
- Logging levels
- Feature flags

### 4. Backup and Rollback Plan

Before deploying to production:
- Create a backup of the current production environment
- Document the rollback procedure
- Test the rollback process in a staging environment

### 5. Staged Deployment

Deploy to environments in sequence:
- Development environment (validate basic functionality)
- Staging environment (perform full regression testing)
- Production environment (monitor closely after deployment)

## Post-Deployment Monitoring

After deployment, monitor the following:
- Application logs for unexpected errors
- Performance metrics compared to baseline
- Database query performance
- User-reported issues

Address any issues promptly and document resolutions for future reference.