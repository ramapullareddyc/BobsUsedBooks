# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (net6.0, net7.0, or net8.0) rather than .NET Framework versions.

### 2. Run Unit Tests

Execute the test suite to ensure functionality remains intact:

```bash
cd app/Bookstore.Domain.Tests
dotnet test --logger "console;verbosity=detailed"
```

Review test results for any failures or warnings that may indicate compatibility issues.

### 3. Check Dependencies

List all package references to identify any legacy or deprecated packages:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated packages to their latest stable versions compatible with your target framework.

### 4. Validate Data Access Layer

Since Bookstore.Data is present, verify database connectivity and operations:

- Test database connection strings for compatibility with cross-platform environments
- Verify that any Entity Framework or ADO.NET code functions correctly
- Check for hardcoded Windows-specific paths or connection string formats

### 5. Review Platform-Specific Code

Search for potential platform-specific implementations:

```bash
grep -r "System.Windows" app/
grep -r "Microsoft.Win32" app/
grep -r "PlatformID.Win32" app/
```

Replace any Windows-specific code with cross-platform alternatives using `System.Runtime.InteropServices.RuntimeInformation`.

### 6. Test Web Application Locally

Run the web application to verify runtime behavior:

```bash
cd app/Bookstore.Web
dotnet run
```

- Navigate to the application in a browser
- Test critical user workflows
- Check for any runtime exceptions in the console output
- Verify static file serving and routing functionality

### 7. Validate CDK Infrastructure

Review the Bookstore.Cdk project for deployment configurations:

```bash
cd app/Bookstore.Cdk
dotnet build
```

- Ensure AWS CDK constructs are compatible with the new .NET version
- Verify that infrastructure definitions reference correct runtime environments
- Test CDK synthesis: `cdk synth` (if AWS CDK CLI is installed)

### 8. Configuration File Review

Examine configuration files for platform-specific settings:

- Review `appsettings.json` and `appsettings.Development.json`
- Check `web.config` files (these should be removed or replaced with appropriate .NET configuration)
- Verify environment variable usage is cross-platform compatible

### 9. File Path Validation

Search for hardcoded Windows paths:

```bash
grep -r "C:\\\\" app/
grep -r "\\\\" app/ | grep -v node_modules | grep -v bin | grep -v obj
```

Replace backslashes with `Path.Combine()` or forward slashes where appropriate.

### 10. Runtime Testing on Target Platform

If the target deployment platform is Linux or macOS:

- Test the application on the actual target operating system
- Verify file system case sensitivity does not cause issues
- Confirm that all file paths resolve correctly
- Test any external process invocations or shell commands

## Performance and Compatibility Checks

### 11. Analyze Runtime Performance

Profile the application to establish baseline performance metrics:

```bash
dotnet run --configuration Release
```

Compare performance characteristics with the legacy version to identify any regressions.

### 12. Review Logging and Diagnostics

- Verify that logging providers are compatible with cross-platform .NET
- Test that diagnostic tools and monitoring integrations function correctly
- Ensure exception handling captures platform-agnostic information

## Final Validation

### 13. Clean Build Verification

Perform a clean build of the entire solution:

```bash
dotnet clean
dotnet build --configuration Release
```

Verify that no warnings or errors appear during the release build.

### 14. Publish Test

Test the publishing process:

```bash
cd app/Bookstore.Web
dotnet publish -c Release -o ./publish
```

Inspect the publish output directory to ensure all necessary files are included and no legacy framework dependencies remain.

### 15. Integration Testing

If integration tests exist or can be created:

- Test database migrations and seeding
- Verify external service integrations
- Test authentication and authorization flows
- Validate API endpoints if applicable

## Deployment Preparation

### 16. Update Deployment Documentation

- Document the new target framework version
- Update deployment scripts to use `dotnet` CLI commands
- Revise server requirements to reflect cross-platform hosting capabilities

### 17. Environment-Specific Configuration

Prepare configuration for target environments:

- Set up environment variables for production
- Configure connection strings for target infrastructure
- Verify SSL/TLS certificate handling is platform-agnostic

### 18. Staged Deployment

Deploy to a staging environment first:

- Monitor application startup and initialization
- Execute smoke tests against the staging deployment
- Review logs for any unexpected warnings or errors
- Validate that all application features function as expected

Once staging validation is complete, proceed with production deployment following your standard release process.

## Post-Deployment Monitoring

After deployment, monitor the application for:

- Unexpected exceptions or error patterns
- Performance degradation
- Memory leaks or resource consumption issues
- Compatibility problems with external dependencies