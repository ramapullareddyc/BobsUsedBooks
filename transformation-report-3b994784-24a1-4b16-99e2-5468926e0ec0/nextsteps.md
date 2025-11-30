# Next Steps

## Overview

The transformation appears to be **successful** with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure consistent `<TargetFramework>` values (e.g., `net6.0`, `net7.0`, or `net8.0`).

### 2. Run Unit Tests

Execute the test suite to ensure functionality remains intact:

```bash
dotnet test Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj --verbosity normal
```

Review test results for any failures or warnings that may indicate behavioral changes.

### 3. Check Package Dependencies

Verify that all NuGet packages are compatible with the target framework:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages that have cross-platform equivalents.

### 4. Validate Runtime Behavior

Build and run the web application locally:

```bash
dotnet build
dotnet run --project Bookstore.Web/Bookstore.Web.csproj
```

Test key functionality:
- Database connectivity (Bookstore.Data)
- Web endpoints and UI rendering (Bookstore.Web)
- Business logic operations (Bookstore.Domain)

### 5. Review Platform-Specific Code

Search for any remaining platform-specific code patterns:

```bash
grep -r "System.Web" app/
grep -r "Windows" app/ --include="*.cs"
```

Check for:
- Windows-specific file path handling (`\` vs `/`)
- Registry access or Windows API calls
- Platform-specific cryptography implementations

### 6. Test on Target Platforms

Run the application on each target platform:

**Linux:**
```bash
dotnet publish -c Release -r linux-x64
./bin/Release/net*/linux-x64/publish/Bookstore.Web
```

**macOS:**
```bash
dotnet publish -c Release -r osx-x64
./bin/Release/net*/osx-x64/publish/Bookstore.Web
```

**Windows:**
```bash
dotnet publish -c Release -r win-x64
.\bin\Release\net*\win-x64\publish\Bookstore.Web.exe
```

### 7. Validate CDK Infrastructure

If the Bookstore.Cdk project contains AWS CDK infrastructure code:

```bash
cd Bookstore.Cdk
dotnet build
cdk synth
```

Review the synthesized CloudFormation template for any issues.

### 8. Performance Testing

Compare performance metrics between the legacy and migrated versions:
- Application startup time
- Memory consumption
- Request/response latency
- Database query performance

### 9. Configuration Review

Verify configuration files have been properly migrated:
- `appsettings.json` replaces `web.config` or `app.config`
- Connection strings are properly formatted
- Environment-specific settings are correctly structured

### 10. Documentation Updates

Update project documentation to reflect:
- New target framework requirements
- Cross-platform build and deployment instructions
- Any breaking changes in APIs or behavior
- Updated development environment setup

## Deployment Preparation

### Pre-Deployment Checklist

- [ ] All tests pass on target platforms
- [ ] Configuration files are environment-ready
- [ ] Database migrations are tested
- [ ] Logging and monitoring are functional
- [ ] Security settings are reviewed
- [ ] Performance benchmarks meet requirements

### Deployment Steps

1. **Publish the application:**
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. **Verify published output:**
   - Check that all required dependencies are included
   - Confirm configuration files are present
   - Validate that the output runs independently

3. **Deploy to target environment:**
   - Copy published files to the target server
   - Configure environment variables
   - Set up the application as a service (systemd on Linux, Windows Service, or launchd on macOS)

4. **Post-deployment validation:**
   - Verify application starts successfully
   - Test critical user workflows
   - Monitor logs for errors or warnings
   - Confirm database connectivity and operations

## Ongoing Maintenance

- Monitor for .NET updates and security patches
- Regularly update NuGet packages
- Review deprecation warnings in future .NET versions
- Maintain cross-platform testing in your development workflow