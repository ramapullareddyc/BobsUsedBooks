# Next Steps

## Validation and Testing

Based on the information provided, your solution appears to have **no build errors** after transformation. This is a positive indication that the migration to cross-platform .NET was successful. However, you should perform thorough validation before considering the migration complete.

### 1. Verify Build Success

```bash
# Clean and rebuild the entire solution
dotnet clean
dotnet build --configuration Release
```

Ensure all projects compile without warnings or errors.

### 2. Update Target Framework Verification

Confirm that all projects are targeting the appropriate .NET version:

```bash
# Check each project's target framework
grep -r "<TargetFramework>" **/*.csproj
```

Ensure consistency across projects (e.g., all targeting `net8.0` or `net6.0`).

### 3. Run Unit Tests

Execute the test suite to verify functionality:

```bash
# Run all tests in the solution
dotnet test

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"

# Generate code coverage report
dotnet test --collect:"XPlat Code Coverage"
```

Pay special attention to `Bookstore.Domain.Tests` to ensure domain logic remains intact.

### 4. Validate Dependencies

Review and update NuGet packages:

```bash
# List outdated packages
dotnet list package --outdated

# Update packages to latest compatible versions
dotnet add package <PackageName>
```

Check for any deprecated packages that may need replacement in cross-platform .NET.

### 5. Runtime Testing

Perform runtime validation of the web application:

```bash
# Run the web application
cd app/Bookstore.Web
dotnet run
```

Test the following:
- Application starts without runtime errors
- All endpoints respond correctly
- Database connections function properly
- Static files and assets load correctly
- Authentication and authorization work as expected

### 6. Cross-Platform Verification

If cross-platform compatibility is a requirement, test on multiple operating systems:

- **Windows**: Test on Windows 10/11
- **Linux**: Test on Ubuntu or your target Linux distribution
- **macOS**: Test on macOS if applicable

```bash
# Publish for specific runtime
dotnet publish -c Release -r linux-x64
dotnet publish -c Release -r win-x64
dotnet publish -c Release -r osx-x64
```

### 7. Database Migration Validation

If using Entity Framework Core, verify migrations:

```bash
# Check migration status
dotnet ef migrations list --project app/Bookstore.Data

# Test applying migrations to a test database
dotnet ef database update --project app/Bookstore.Data
```

### 8. CDK Infrastructure Review

Review the `Bookstore.Cdk` project for AWS CDK compatibility:

```bash
cd app/Bookstore.Cdk
dotnet build
```

Ensure CDK constructs are compatible with the new .NET version and test synthesis:

```bash
cdk synth
```

### 9. Configuration Files

Verify configuration files have been properly migrated:

- Check `appsettings.json` and environment-specific variants
- Validate connection strings
- Review any custom configuration sections
- Ensure environment variables are correctly referenced

### 10. Performance Baseline

Establish performance baselines for comparison:

- Measure application startup time
- Test response times for critical endpoints
- Monitor memory usage
- Compare with legacy application metrics if available

### 11. Integration Testing

If you have integration tests, execute them against the migrated application:

```bash
# Run integration tests if separated
dotnet test --filter Category=Integration
```

### 12. Deployment Preparation

Once validation is complete, prepare for deployment:

```bash
# Create a production-ready build
dotnet publish -c Release -o ./publish

# Verify published output
ls -la ./publish
```

Test the published application in a staging environment that mirrors production.

## Common Post-Migration Issues to Watch For

- **API compatibility**: Verify all third-party APIs still function correctly
- **Serialization changes**: JSON serialization behavior may differ slightly
- **Date/time handling**: Ensure timezone handling remains consistent
- **File path separators**: Confirm path handling works across platforms
- **Case sensitivity**: Linux file systems are case-sensitive unlike Windows

## Documentation Updates

Update project documentation to reflect:

- New target framework version
- Updated build and run instructions
- Any changed deployment procedures
- Modified system requirements