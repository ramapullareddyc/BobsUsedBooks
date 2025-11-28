# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (e.g., `net6.0`, `net7.0`, or `net8.0`).

### 2. Run Unit Tests

Execute the test suite to ensure functionality remains intact:

```bash
dotnet test Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj --verbosity normal
```

Review test results for any failures or warnings that may indicate runtime compatibility issues.

### 3. Check Package Dependencies

List all NuGet packages and verify compatibility with cross-platform .NET:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages that may have cross-platform alternatives.

### 4. Validate Data Layer Functionality

Since Bookstore.Data likely contains database access code:

- Verify database connection strings are correctly configured
- Test database migrations if using Entity Framework Core
- Run integration tests against a test database instance
- Confirm that any platform-specific database drivers have been replaced with cross-platform equivalents

### 5. Test Web Application Locally

For the Bookstore.Web project:

```bash
cd Bookstore.Web
dotnet run
```

- Access the application through the browser at the specified localhost address
- Test critical user workflows and features
- Check browser console and application logs for any runtime warnings or errors
- Verify static file serving, routing, and middleware functionality

### 6. Review CDK Infrastructure Code

For the Bookstore.Cdk project:

- Ensure AWS CDK constructs are compatible with the new .NET version
- Synthesize the CloudFormation template to verify infrastructure definitions:

```bash
cd Bookstore.Cdk
cdk synth
```

- Review the generated template for any unexpected changes

### 7. Platform-Specific Code Review

Search for potential platform-specific code patterns:

- Windows-specific path separators (use `Path.Combine` instead of hardcoded backslashes)
- Registry access or Windows-specific APIs
- Case-sensitive file system assumptions
- Line ending differences (CRLF vs LF)

### 8. Runtime Configuration Validation

- Review `appsettings.json` and environment-specific configuration files
- Verify that configuration providers work correctly across platforms
- Test environment variable substitution and configuration binding

### 9. Cross-Platform Testing

If possible, test the application on multiple operating systems:

- Windows
- Linux
- macOS

This ensures true cross-platform compatibility beyond just compilation success.

### 10. Performance Baseline

Establish performance benchmarks:

- Measure application startup time
- Profile memory usage
- Test response times for critical operations
- Compare metrics with the legacy version to identify any regressions

## Deployment Preparation

### 1. Create Publish Profiles

Generate platform-specific publish profiles:

```bash
dotnet publish Bookstore.Web/Bookstore.Web.csproj -c Release -o ./publish/web
```

Test the published output to ensure all dependencies are included.

### 2. Update Deployment Scripts

Modify existing deployment scripts to use `dotnet` CLI commands instead of legacy framework-specific tools.

### 3. Environment Configuration

- Ensure target deployment environments have the appropriate .NET runtime installed
- Verify that any system dependencies (e.g., native libraries) are available on target platforms
- Update deployment documentation to reflect new runtime requirements

### 4. Database Migration Strategy

If using Entity Framework Core:

```bash
dotnet ef migrations script --idempotent --output migration.sql
```

Review the generated migration script before applying to production databases.

### 5. Monitoring and Logging

- Verify that logging providers are compatible with cross-platform .NET
- Test application insights or other monitoring tools in the new environment
- Ensure structured logging works correctly

## Documentation Updates

- Update README files with new build and run instructions
- Document any breaking changes or behavioral differences
- Update developer onboarding guides with new prerequisites
- Revise deployment runbooks to reflect the modernized stack

## Final Verification Checklist

- [ ] All projects build successfully with `dotnet build`
- [ ] All unit tests pass with `dotnet test`
- [ ] Web application runs locally without errors
- [ ] Database connectivity and operations function correctly
- [ ] CDK infrastructure synthesizes without errors
- [ ] Application configuration loads properly
- [ ] No runtime exceptions in critical paths
- [ ] Performance meets acceptable thresholds
- [ ] Deployment artifacts generate correctly