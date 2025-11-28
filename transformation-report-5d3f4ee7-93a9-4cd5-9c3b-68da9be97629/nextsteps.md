# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (net6.0, net7.0, or net8.0).

### 2. Run Unit Tests

Execute the test suite to validate functionality:

```bash
cd app/Bookstore.Domain.Tests
dotnet test --logger "console;verbosity=detailed"
```

Review test results for any failures or warnings that may indicate compatibility issues.

### 3. Check Package Dependencies

Verify that all NuGet packages are compatible with cross-platform .NET:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages that may have cross-platform alternatives.

### 4. Validate Data Access Layer

Test the Bookstore.Data project functionality:

- Verify database connection strings are correctly configured
- Test database migrations if Entity Framework Core is used
- Confirm that data access operations work on the target platform

### 5. Test the Web Application Locally

Run the Bookstore.Web project:

```bash
cd app/Bookstore.Web
dotnet run
```

Perform the following checks:

- Verify the application starts without errors
- Test critical user workflows through the UI
- Check browser console for JavaScript errors
- Validate API endpoints if applicable
- Review application logs for warnings or exceptions

### 6. Platform-Specific Testing

Test the application on target operating systems:

- **Linux**: Run on a Linux distribution to verify compatibility
- **macOS**: Test on macOS if this is a target platform
- **Windows**: Validate on Windows to ensure no regressions

Use the following command to run the application:

```bash
dotnet run --project app/Bookstore.Web/Bookstore.Web.csproj
```

### 7. Review Configuration Files

Examine configuration files for platform-specific paths or settings:

- Check `appsettings.json` and environment-specific variants
- Verify file paths use forward slashes or `Path.Combine()`
- Confirm connection strings are environment-agnostic
- Review any hardcoded paths that may be Windows-specific

### 8. Validate AWS CDK Infrastructure

Test the Bookstore.Cdk project:

```bash
cd app/Bookstore.Cdk
dotnet build
cdk synth
```

Verify that the CDK stack synthesizes correctly and review the generated CloudFormation template.

### 9. Performance Testing

Conduct basic performance testing to identify any regressions:

- Measure application startup time
- Test response times for key operations
- Monitor memory usage during typical workflows

### 10. Code Review for Platform-Specific APIs

Search the codebase for potentially problematic patterns:

```bash
grep -r "System.Windows" app/
grep -r "Microsoft.Win32" app/
grep -r "P/Invoke" app/
```

Replace any Windows-specific APIs with cross-platform alternatives.

## Deployment Preparation

### 1. Create Publish Profiles

Generate platform-specific publish outputs:

```bash
# Self-contained deployment for Linux
dotnet publish app/Bookstore.Web/Bookstore.Web.csproj -c Release -r linux-x64 --self-contained

# Framework-dependent deployment
dotnet publish app/Bookstore.Web/Bookstore.Web.csproj -c Release
```

### 2. Validate Published Output

Test the published application:

```bash
cd app/Bookstore.Web/bin/Release/net*/publish
dotnet Bookstore.Web.dll
```

Ensure all dependencies are included and the application runs correctly.

### 3. Environment Configuration

Prepare environment-specific configurations:

- Set up environment variables for different deployment targets
- Configure secrets management for sensitive data
- Verify logging configuration for production environments

### 4. Database Migration Strategy

If using Entity Framework Core, prepare migration scripts:

```bash
dotnet ef migrations script --project app/Bookstore.Data --startup-project app/Bookstore.Web --idempotent --output migration.sql
```

Review the generated SQL script before applying to production databases.

### 5. Deploy CDK Stack

Deploy the infrastructure using AWS CDK:

```bash
cd app/Bookstore.Cdk
cdk deploy
```

Verify that all AWS resources are created successfully and note any output values needed for application configuration.

## Post-Deployment Validation

### 1. Smoke Testing

Execute smoke tests against the deployed application:

- Verify the application is accessible
- Test authentication and authorization flows
- Validate database connectivity
- Confirm external service integrations

### 2. Monitor Application Health

Set up monitoring for the deployed application:

- Review application logs for errors
- Monitor performance metrics
- Set up alerts for critical failures

### 3. Rollback Plan

Document the rollback procedure in case issues are discovered:

- Identify the previous stable version
- Document the rollback steps
- Test the rollback procedure in a non-production environment

## Documentation Updates

Update project documentation to reflect the migration:

- Document the new target framework version
- Update build and deployment instructions
- Note any configuration changes required
- Update developer setup guides for cross-platform development