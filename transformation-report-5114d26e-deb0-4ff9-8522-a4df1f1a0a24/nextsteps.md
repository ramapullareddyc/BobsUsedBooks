# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (net6.0, net7.0, or net8.0) rather than a .NET Framework version (net48, net472, etc.).

### 2. Run Unit Tests

Execute the test suite to verify functionality remains intact:

```bash
dotnet test Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj --verbosity normal
```

Review test results for any failures or warnings that may indicate compatibility issues.

### 3. Check for Runtime Dependencies

Examine dependencies for any platform-specific packages:

```bash
dotnet list package --include-transitive
```

Look for packages that may have Windows-specific dependencies or that have been replaced with cross-platform alternatives.

### 4. Review Code for Platform-Specific APIs

Search the codebase for potentially problematic APIs:

- Windows-specific file path handling (backslashes vs forward slashes)
- Registry access
- Windows-specific cryptography APIs
- COM interop
- Windows-specific configuration sources

### 5. Test the Web Application Locally

Run the Bookstore.Web project to verify it starts correctly:

```bash
dotnet run --project Bookstore.Web/Bookstore.Web.csproj
```

Test key functionality through the web interface to ensure:
- Database connectivity works correctly
- Authentication and authorization function as expected
- All major features operate without errors

### 6. Validate Data Access Layer

Test the Bookstore.Data project's database operations:

- Verify connection strings are compatible with cross-platform environments
- Confirm Entity Framework Core (if used) migrations work correctly
- Test database operations on the target platform (Linux, macOS, or Windows)

### 7. Review Configuration Files

Examine configuration files for platform-specific paths or settings:

- `appsettings.json` and environment-specific variants
- Connection strings
- File paths (ensure they use `Path.Combine` or forward slashes)
- Logging configurations

### 8. Test on Target Platform

Deploy and test the application on the intended target platform:

```bash
dotnet publish -c Release -r linux-x64 --self-contained false
```

Replace `linux-x64` with your target runtime identifier (RID) such as `osx-x64`, `win-x64`, etc.

### 9. Verify CDK Infrastructure Code

Review the Bookstore.Cdk project:

- Ensure AWS CDK constructs are compatible with the new .NET version
- Test CDK synthesis:

```bash
cd Bookstore.Cdk
cdk synth
```

### 10. Performance Testing

Conduct performance testing to identify any regressions:

- Load testing for the web application
- Database query performance
- Memory usage patterns
- Startup time

## Deployment Preparation

### 1. Update Deployment Scripts

Modify any deployment scripts to use the `dotnet` CLI instead of MSBuild or framework-specific tools.

### 2. Environment Configuration

Ensure environment variables and configuration sources work across platforms:

- Test environment variable loading
- Verify secrets management
- Confirm external service connectivity

### 3. Documentation Updates

Update project documentation to reflect:

- New target framework version
- Cross-platform compatibility
- Updated build and run instructions
- Any changed dependencies or requirements

## Final Verification Checklist

- [ ] All projects build without errors or warnings
- [ ] Unit tests pass completely
- [ ] Integration tests pass (if applicable)
- [ ] Application runs successfully on development environment
- [ ] Application runs successfully on target platform
- [ ] Database operations function correctly
- [ ] Configuration loads properly across environments
- [ ] CDK infrastructure code synthesizes without errors
- [ ] No platform-specific APIs remain in the codebase
- [ ] Performance meets expected benchmarks

## Recommended Monitoring

After deployment, monitor the following:

- Application startup time
- Memory consumption patterns
- Exception rates and types
- Database connection pool behavior
- Response times for critical endpoints