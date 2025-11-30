# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects compiled without issues:

- Bookstore.Data
- Bookstore.Domain.Tests
- Bookstore.Cdk
- Bookstore.Web
- Bookstore.Domain

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (e.g., `net6.0`, `net7.0`, or `net8.0`).

### 2. Run Unit Tests

Execute the test suite to verify functionality:

```bash
cd app/Bookstore.Domain.Tests
dotnet test --configuration Release
```

Review test results for any failures or warnings that may indicate runtime incompatibilities.

### 3. Check for Platform-Specific Dependencies

Audit NuGet packages for Windows-specific dependencies:

```bash
dotnet list package --include-transitive
```

Look for packages that may have platform-specific implementations and verify they support cross-platform execution.

### 4. Validate Data Access Layer

Test the Bookstore.Data project's database connectivity:

- Verify connection strings are configured correctly for the target environment
- Test database operations on the target platform (Linux/macOS if applicable)
- Confirm Entity Framework Core or other ORM functionality works as expected

### 5. Test the Web Application

Run the Bookstore.Web project locally:

```bash
cd app/Bookstore.Web
dotnet run
```

Verify:
- Application starts without errors
- All endpoints respond correctly
- Static files are served properly
- Authentication/authorization functions as expected

### 6. Review Configuration Files

Check configuration files for hardcoded paths or Windows-specific settings:

- `appsettings.json` and environment-specific variants
- `web.config` (should be removed or replaced with appropriate .NET configuration)
- Connection strings
- File path separators (use `Path.Combine()` instead of hardcoded backslashes)

### 7. Validate CDK Infrastructure

Test the Bookstore.Cdk project:

```bash
cd app/Bookstore.Cdk
dotnet build
cdk synth
```

Ensure the CDK stack synthesizes correctly and review the generated CloudFormation template.

### 8. Cross-Platform Runtime Testing

If targeting multiple platforms, test the application on each:

- Windows
- Linux (Ubuntu/Debian recommended)
- macOS (if applicable)

Run the application and execute tests on each platform to identify platform-specific issues.

### 9. Performance Baseline

Establish performance metrics for the migrated application:

```bash
dotnet test --configuration Release --logger "console;verbosity=detailed"
```

Compare response times and resource usage against the legacy application baseline.

### 10. Review Deprecated API Usage

Check for warnings about deprecated APIs:

```bash
dotnet build /warnaserror
```

Address any warnings related to obsolete methods or types that may be removed in future .NET versions.

## Deployment Preparation

### 1. Create Publish Profiles

Generate optimized release builds:

```bash
dotnet publish app/Bookstore.Web/Bookstore.Web.csproj -c Release -o ./publish
```

### 2. Validate Published Output

Test the published application:

```bash
cd publish
dotnet Bookstore.Web.dll
```

Ensure all dependencies are included and the application runs from the published directory.

### 3. Update Deployment Scripts

Modify existing deployment scripts to use `dotnet` commands instead of MSBuild or framework-specific tools.

### 4. Environment Configuration

Prepare environment-specific configuration:

- Set up environment variables for sensitive data
- Configure logging providers appropriate for the deployment environment
- Verify HTTPS certificate configuration

### 5. Database Migration

If using Entity Framework Core, prepare migration scripts:

```bash
dotnet ef migrations script --idempotent -o migration.sql
```

Review and test the migration script in a staging environment before production deployment.

## Final Checklist

- [ ] All projects build without errors or warnings
- [ ] Unit tests pass on target platform(s)
- [ ] Integration tests complete successfully
- [ ] Application runs correctly on target platform(s)
- [ ] Configuration files updated for cross-platform compatibility
- [ ] No Windows-specific API calls remain
- [ ] Published output tested and verified
- [ ] Database migrations tested
- [ ] CDK infrastructure validated
- [ ] Performance meets or exceeds legacy application baseline

## Additional Considerations

### Code Quality

Run static analysis tools to identify potential issues:

```bash
dotnet format --verify-no-changes
```

### Security Review

Verify that security-related functionality remains intact:

- Authentication mechanisms
- Authorization policies
- Data encryption
- Secure communication (HTTPS/TLS)

### Documentation

Update project documentation to reflect:

- New target framework version
- Updated build and deployment procedures
- Platform-specific considerations
- Dependency changes

The transformation appears complete based on the absence of build errors. Focus on thorough testing across all target platforms and environments to ensure full compatibility and functionality.