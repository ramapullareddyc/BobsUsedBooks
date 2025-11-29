# Next Steps

## Overview

The transformation appears to be successful with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure the `<TargetFramework>` element specifies a cross-platform .NET version (net6.0, net7.0, or net8.0) rather than .NET Framework.

### 2. Run Unit Tests

Execute the test suite to ensure functionality remains intact:

```bash
cd app/Bookstore.Domain.Tests
dotnet test --verbosity normal
```

Review test results for any failures or warnings that may indicate runtime compatibility issues not caught during compilation.

### 3. Check Package Compatibility

List all NuGet packages and verify they are compatible with cross-platform .NET:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages that have cross-platform alternatives.

### 4. Review Configuration Files

Examine configuration files for platform-specific settings:

- Check `appsettings.json` and `appsettings.Development.json` in Bookstore.Web
- Review any connection strings for database compatibility
- Verify file path separators use `Path.Combine()` rather than hardcoded backslashes

### 5. Test Data Access Layer

Validate the Bookstore.Data project functionality:

- Ensure Entity Framework Core (if used) migrations are compatible
- Test database connectivity on the target platform
- Verify that any ORM configurations work correctly

```bash
cd app/Bookstore.Data
dotnet build --configuration Release
```

### 6. Local Runtime Testing

Run the web application locally to identify runtime issues:

```bash
cd app/Bookstore.Web
dotnet run
```

Test key application workflows:
- Navigate through main pages
- Test CRUD operations
- Verify authentication/authorization (if applicable)
- Check logging and error handling

### 7. Cross-Platform Validation

If targeting multiple operating systems, test on each platform:

- **Windows**: Test on Windows 10/11
- **Linux**: Test on Ubuntu or your target Linux distribution
- **macOS**: Test on macOS if applicable

For each platform:

```bash
dotnet build --configuration Release
dotnet run --configuration Release
```

### 8. Review CDK Infrastructure Code

Examine the Bookstore.Cdk project for AWS deployment:

- Verify CDK constructs are compatible with the new .NET version
- Test CDK synthesis locally:

```bash
cd app/Bookstore.Cdk
dotnet build
cdk synth
```

### 9. Performance Testing

Compare performance metrics between the legacy and migrated versions:

- Measure application startup time
- Test response times for key endpoints
- Monitor memory usage during typical operations

### 10. Code Analysis

Run static code analysis to identify potential issues:

```bash
dotnet format --verify-no-changes
dotnet build /p:TreatWarningsAsErrors=true
```

### 11. Dependency Audit

Review the dependency tree for security vulnerabilities:

```bash
dotnet list package --vulnerable
```

Address any reported vulnerabilities by updating packages.

## Deployment Preparation

### 1. Update Deployment Scripts

Modify any deployment scripts to use the new .NET runtime:

- Update runtime identifiers (RIDs) for self-contained deployments
- Adjust publish commands to target cross-platform .NET

```bash
dotnet publish -c Release -r linux-x64 --self-contained
```

### 2. Environment Configuration

Ensure target environments have the appropriate .NET runtime installed:

- Verify .NET runtime version availability on target servers
- Update environment variables as needed
- Configure application pools or systemd services for the new runtime

### 3. Create Deployment Package

Generate a release build and verify the output:

```bash
dotnet publish app/Bookstore.Web/Bookstore.Web.csproj -c Release -o ./publish
```

Inspect the `./publish` directory to ensure all necessary files are included.

### 4. Database Migration Strategy

Plan database updates if Entity Framework migrations were affected:

```bash
cd app/Bookstore.Data
dotnet ef migrations list
dotnet ef database update --dry-run
```

### 5. Rollback Plan

Document the rollback procedure:

- Maintain the legacy version in a separate branch
- Create database backup before deployment
- Document configuration differences between versions

## Final Checklist

- [ ] All projects build without errors or warnings
- [ ] Unit tests pass successfully
- [ ] Application runs correctly on target platform(s)
- [ ] Database connectivity verified
- [ ] CDK infrastructure code synthesizes correctly
- [ ] No vulnerable dependencies detected
- [ ] Performance metrics are acceptable
- [ ] Deployment package created and validated
- [ ] Rollback plan documented
- [ ] Team trained on any new tooling or processes

## Documentation Updates

Update project documentation to reflect the migration:

- README.md with new build and run instructions
- Development environment setup guide
- Deployment procedures
- Troubleshooting guide for common cross-platform issues