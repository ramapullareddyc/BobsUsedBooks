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

Execute the test suite to ensure functionality remains intact:

```bash
cd app/Bookstore.Domain.Tests
dotnet test --verbosity normal
```

Review test results for any failures or warnings that may indicate runtime compatibility issues.

### 3. Check Package Compatibility

Verify that all NuGet packages are compatible with the target framework:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any packages that have newer versions supporting cross-platform .NET.

### 4. Validate Data Access Layer

If Bookstore.Data uses Entity Framework or other data access technologies:

- Test database connectivity on the target platform (Linux/macOS if applicable)
- Verify connection strings use cross-platform compatible formats
- Run any existing database migrations:

```bash
cd app/Bookstore.Data
dotnet ef database update
```

### 5. Test Web Application Locally

Run the web application to verify it functions correctly:

```bash
cd app/Bookstore.Web
dotnet run
```

Test key functionality:
- Application startup and configuration loading
- Routing and endpoint accessibility
- Static file serving
- Authentication/authorization (if applicable)

### 6. Review Platform-Specific Code

Search for potential platform-specific issues:

- File path separators (use `Path.Combine` instead of hardcoded slashes)
- Case-sensitive file system references
- Windows-specific APIs (Registry, WMI, etc.)
- Line ending differences

### 7. Test on Target Platforms

If the goal is cross-platform support, test the application on:

- Linux (Ubuntu or your target distribution)
- macOS (if applicable)
- Windows (to ensure backward compatibility)

Build and run on each platform:

```bash
dotnet build --configuration Release
dotnet run --configuration Release
```

### 8. Validate CDK Infrastructure

For the Bookstore.Cdk project:

```bash
cd app/Bookstore.Cdk
dotnet build
```

If using AWS CDK, synthesize the CloudFormation template to verify:

```bash
cdk synth
```

Review the generated template for any issues.

### 9. Performance Testing

Compare application performance between the legacy and migrated versions:

- Startup time
- Memory consumption
- Request throughput (for web applications)
- Database query performance

### 10. Review Configuration Files

Verify configuration files are compatible:

- `appsettings.json` and environment-specific variants
- `web.config` (if present, consider migrating to `appsettings.json`)
- Ensure configuration providers are cross-platform compatible

## Post-Validation Steps

### Update Documentation

Document the following:

- New target framework version
- Updated build and run commands
- Any configuration changes
- Platform-specific considerations

### Code Cleanup

Remove legacy artifacts:

- Unused `packages.config` files (if NuGet packages were migrated to PackageReference)
- Obsolete project files or build scripts
- Legacy framework-specific conditional compilation symbols

### Establish Testing Strategy

Create a testing checklist for future deployments:

- Automated test execution in the build process
- Cross-platform validation procedures
- Performance benchmarking baselines

## Deployment Preparation

### 1. Publish the Application

Create a release build:

```bash
dotnet publish app/Bookstore.Web/Bookstore.Web.csproj \
  --configuration Release \
  --output ./publish \
  --runtime linux-x64 \
  --self-contained false
```

Adjust the `--runtime` parameter based on your target deployment environment.

### 2. Verify Published Output

Inspect the publish directory:

- Confirm all necessary assemblies are present
- Check that `appsettings.json` and other configuration files are included
- Verify static assets (wwwroot contents) are copied

### 3. Test Published Application

Run the published application locally:

```bash
cd publish
dotnet Bookstore.Web.dll
```

Validate functionality matches the development environment.

### 4. Prepare Deployment Environment

Ensure the target environment has:

- Appropriate .NET runtime installed
- Required environment variables configured
- Database connectivity established
- Necessary permissions for file system access

### 5. Deploy and Monitor

After deployment:

- Monitor application logs for runtime errors
- Verify all endpoints are accessible
- Check database connections and operations
- Monitor resource utilization (CPU, memory, disk I/O)

## Additional Considerations

### Security Review

- Ensure secrets are not hardcoded in configuration files
- Verify secure connection strings are used
- Review authentication and authorization mechanisms for cross-platform compatibility

### Dependency Audit

Run a security audit on dependencies:

```bash
dotnet list package --vulnerable
```

Address any vulnerabilities by updating packages.

### Rollback Plan

Maintain the legacy version until the migrated version is fully validated in production. Document a rollback procedure in case issues arise.