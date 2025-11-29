# Next Steps

## Validation and Testing

Based on the information provided, your solution appears to have **no build errors** after the transformation to cross-platform .NET. This is a positive indication that the migration was successful. However, you should perform thorough validation before considering the transformation complete.

### 1. Verify Build Success

```bash
# Clean and rebuild the entire solution
dotnet clean
dotnet build --configuration Release
```

Ensure all projects compile without warnings or errors in both Debug and Release configurations.

### 2. Run Unit Tests

Execute the test suite to verify functionality has been preserved:

```bash
# Run all tests in the solution
dotnet test

# Run tests with detailed output
dotnet test --verbosity normal

# Generate code coverage report (if configured)
dotnet test --collect:"XPath Code Coverage"
```

Pay special attention to `Bookstore.Domain.Tests` to ensure domain logic remains intact.

### 3. Review Project Dependencies

Verify that all NuGet packages are compatible with the target framework:

```bash
# List outdated packages
dotnet list package --outdated

# Check for deprecated packages
dotnet list package --deprecated

# Check for packages with known vulnerabilities
dotnet list package --vulnerable
```

Update any packages that have newer cross-platform compatible versions.

### 4. Validate Framework-Specific Code

Review your codebase for potential runtime issues:

- **File Path Handling**: Ensure all file paths use `Path.Combine()` or `Path.DirectorySeparatorChar` instead of hardcoded backslashes
- **Case Sensitivity**: File and directory names are case-sensitive on Linux/macOS
- **Line Endings**: Verify that line ending differences don't affect file parsing
- **Platform-Specific APIs**: Search for any P/Invoke calls or Windows-specific APIs that may need alternatives

### 5. Test on Target Platforms

Run the application on each target platform:

```bash
# Test on Windows
dotnet run --project Bookstore.Web

# Test on Linux (if available)
dotnet run --project Bookstore.Web

# Test on macOS (if available)
dotnet run --project Bookstore.Web
```

### 6. Validate the CDK Project

The `Bookstore.Cdk` project likely contains AWS CDK infrastructure code:

```bash
# Navigate to the CDK project
cd Bookstore.Cdk

# Synthesize CloudFormation template
cdk synth

# Compare with existing infrastructure (if applicable)
cdk diff
```

Ensure the CDK constructs are compatible with the updated .NET version.

### 7. Review Configuration Files

Examine configuration files for any framework-specific settings:

- `appsettings.json` and environment-specific variants
- `web.config` (should be removed or replaced with appropriate middleware)
- `launchSettings.json` for development settings
- Any connection strings or environment-specific configurations

### 8. Database Connectivity Testing

If `Bookstore.Data` uses Entity Framework or another ORM:

```bash
# Verify migrations are intact
dotnet ef migrations list --project Bookstore.Data

# Test database connection
dotnet ef database update --project Bookstore.Data --dry-run
```

### 9. Integration Testing

Perform end-to-end testing of the web application:

- Test all major user workflows
- Verify API endpoints (if applicable)
- Check authentication and authorization
- Validate data access layer operations
- Test file uploads/downloads if present

### 10. Performance Baseline

Establish performance metrics for comparison:

- Measure application startup time
- Monitor memory usage during typical operations
- Test response times for key endpoints
- Compare with pre-migration metrics if available

## Deployment Preparation

### 1. Update Deployment Scripts

Modify any existing deployment scripts to use `dotnet publish`:

```bash
# Publish for production
dotnet publish Bookstore.Web --configuration Release --output ./publish
```

### 2. Verify Runtime Requirements

Ensure target environments have the appropriate .NET runtime installed, or publish as self-contained:

```bash
# Self-contained deployment for Linux
dotnet publish Bookstore.Web -c Release -r linux-x64 --self-contained true

# Self-contained deployment for Windows
dotnet publish Bookstore.Web -c Release -r win-x64 --self-contained true
```

### 3. Update Documentation

Document the changes made during migration:

- Updated framework version and target platforms
- Any breaking changes or behavioral differences
- New deployment procedures
- Updated system requirements

### 4. Staged Rollout

Deploy to environments in sequence:

1. Development environment - verify basic functionality
2. Testing/QA environment - run full test suite
3. Staging environment - perform user acceptance testing
4. Production environment - deploy with monitoring

### 5. Monitoring and Rollback Plan

- Enable application logging and monitoring
- Prepare rollback procedures in case issues arise
- Monitor error rates and performance metrics post-deployment
- Keep the previous version available for quick rollback if needed

## Final Checklist

- [ ] Solution builds without errors or warnings
- [ ] All unit tests pass
- [ ] Integration tests pass on all target platforms
- [ ] Dependencies are up-to-date and compatible
- [ ] Configuration files are reviewed and updated
- [ ] Database migrations work correctly
- [ ] Performance meets expectations
- [ ] Deployment scripts are updated
- [ ] Documentation is current
- [ ] Rollback plan is in place