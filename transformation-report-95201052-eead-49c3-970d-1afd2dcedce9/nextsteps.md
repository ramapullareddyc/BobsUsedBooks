# Next Steps

## Overview

The transformation appears to be **successful** with no build errors reported across any of the projects in the solution. All five projects (Bookstore.Data, Bookstore.Domain.Tests, Bookstore.Cdk, Bookstore.Web, and Bookstore.Domain) have compiled without issues.

## Validation Steps

### 1. Verify Target Framework

Confirm that all projects are targeting the appropriate .NET version:

```bash
dotnet list package --framework
```

Review each `.csproj` file to ensure consistent framework targeting (e.g., `net6.0`, `net7.0`, or `net8.0`).

### 2. Run Unit Tests

Execute the test suite to verify functionality has been preserved:

```bash
dotnet test Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj --verbosity normal
```

Review test results for any failures or warnings that may indicate behavioral changes.

### 3. Check NuGet Package Compatibility

List all package references and verify they are compatible with cross-platform .NET:

```bash
dotnet list package --outdated
dotnet list package --deprecated
```

Update any outdated or deprecated packages as needed.

### 4. Validate Data Access Layer

Since `Bookstore.Data` is present, verify database connectivity and operations:

- Test database connections on the target platform (Linux/macOS if migrating from Windows)
- Verify connection strings are configured correctly for cross-platform environments
- Confirm Entity Framework Core (if used) migrations work as expected:

```bash
dotnet ef migrations list --project Bookstore.Data
```

### 5. Test the Web Application Locally

Run the web application to ensure it functions correctly:

```bash
dotnet run --project Bookstore.Web/Bookstore.Web.csproj
```

Verify:
- Application starts without errors
- All endpoints respond correctly
- Static files are served properly
- Authentication/authorization works as expected

### 6. Review CDK Infrastructure Code

Examine `Bookstore.Cdk` to ensure AWS CDK constructs are compatible:

```bash
dotnet build Bookstore.Cdk/Bookstore.Cdk.csproj
```

Verify that the CDK stack can be synthesized:

```bash
cd Bookstore.Cdk
cdk synth
```

### 7. Cross-Platform Testing

If the original project was Windows-only, test on target platforms:

- **Linux**: Test on a Linux distribution (Ubuntu, Debian, etc.)
- **macOS**: Test on macOS if applicable
- Verify file path handling (forward slashes vs backslashes)
- Check case-sensitive file system compatibility

### 8. Configuration Review

Examine configuration files for platform-specific settings:

- Review `appsettings.json` and environment-specific variants
- Verify environment variables are set correctly
- Check for hardcoded Windows paths (e.g., `C:\`, `\\server\share`)

### 9. Dependency Analysis

Verify all dependencies are restored correctly:

```bash
dotnet restore
dotnet build --no-restore
```

### 10. Performance Baseline

Establish performance benchmarks on the new platform:

- Measure application startup time
- Test response times for critical endpoints
- Monitor memory usage patterns

## Deployment Preparation

### 1. Publish the Application

Create a release build:

```bash
dotnet publish Bookstore.Web/Bookstore.Web.csproj -c Release -o ./publish
```

### 2. Validate Published Output

Test the published application:

```bash
cd publish
dotnet Bookstore.Web.dll
```

### 3. Review Runtime Dependencies

Ensure the target environment has the necessary runtime installed:

```bash
dotnet --list-runtimes
```

### 4. Environment-Specific Configuration

Prepare configuration for target environments:

- Create environment-specific `appsettings.{Environment}.json` files
- Document required environment variables
- Update connection strings for production databases

### 5. Deploy CDK Stack (if applicable)

Deploy the infrastructure using AWS CDK:

```bash
cd Bookstore.Cdk
cdk deploy
```

Review the CloudFormation stack output for any errors or warnings.

## Documentation Updates

- Update README.md with new build and run instructions for cross-platform .NET
- Document any breaking changes or behavioral differences
- Update deployment documentation to reflect new platform requirements
- Create troubleshooting guide for common cross-platform issues

## Final Verification Checklist

- [ ] All projects build successfully
- [ ] All unit tests pass
- [ ] Application runs locally without errors
- [ ] Database connectivity verified
- [ ] Configuration files reviewed and updated
- [ ] Cross-platform compatibility tested
- [ ] Published output validated
- [ ] CDK stack synthesizes and deploys successfully
- [ ] Documentation updated