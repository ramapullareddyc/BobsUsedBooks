# Next Steps

## Validation and Testing

### 1. Verify Project References and Dependencies

- **Check NuGet packages**: Ensure all NuGet packages have been restored successfully by running:
  ```bash
  dotnet restore
  ```
- **Verify project references**: Confirm that inter-project references are correctly configured and pointing to the new `.csproj` files
- **Review target framework**: Ensure all projects are targeting compatible .NET versions (e.g., `net6.0`, `net7.0`, or `net8.0`)

### 2. Build Verification

- **Clean and rebuild the solution**:
  ```bash
  dotnet clean
  dotnet build
  ```
- **Build in Release mode** to catch any configuration-specific issues:
  ```bash
  dotnet build -c Release
  ```

### 3. Run Unit Tests

- **Execute the test suite** for `Bookstore.Domain.Tests`:
  ```bash
  dotnet test app/Bookstore.Domain.Tests/Bookstore.Domain.Tests.csproj
  ```
- **Generate test coverage reports** to ensure existing functionality is preserved:
  ```bash
  dotnet test --collect:"XPlat Code Coverage"
  ```
- **Review test results** and address any failing tests that may indicate compatibility issues

### 4. Database and Data Layer Validation

- **Test database connectivity** in `Bookstore.Data` project
- **Verify Entity Framework migrations** (if applicable):
  ```bash
  dotnet ef migrations list --project app/Bookstore.Data
  ```
- **Run integration tests** against a test database to validate data access patterns
- **Check connection strings** in configuration files for any hardcoded Windows-specific paths

### 5. Web Application Testing

- **Run the web application locally**:
  ```bash
  dotnet run --project app/Bookstore.Web/Bookstore.Web.csproj
  ```
- **Test all endpoints** and verify functionality in a browser
- **Check static files and wwwroot** content are being served correctly
- **Validate configuration sources** (appsettings.json, environment variables)
- **Test on different operating systems** (Windows, Linux, macOS) to ensure true cross-platform compatibility

### 6. CDK Infrastructure Validation

- **Verify AWS CDK compatibility** with the new .NET version:
  ```bash
  dotnet build app/Bookstore.Cdk/Bookstore.Cdk.csproj
  ```
- **Synthesize the CloudFormation template** to ensure infrastructure code is valid:
  ```bash
  cd app/Bookstore.Cdk
  cdk synth
  ```
- **Review generated CloudFormation** for any unexpected changes

### 7. Configuration and Environment Review

- **Audit configuration files** for Windows-specific paths or settings
- **Verify environment variable usage** is cross-platform compatible
- **Check file path separators** - ensure code uses `Path.Combine()` instead of hardcoded separators
- **Review logging configuration** to ensure it works across platforms

### 8. Performance and Compatibility Testing

- **Run performance benchmarks** if available to compare with the legacy version
- **Test file I/O operations** on different file systems
- **Verify any P/Invoke or native interop** has cross-platform alternatives
- **Check third-party dependencies** for cross-platform support

### 9. Documentation Updates

- **Update README.md** with new build and run instructions
- **Document target framework** and minimum .NET SDK version required
- **Update deployment documentation** to reflect cross-platform capabilities
- **Create migration notes** documenting any breaking changes or behavioral differences

### 10. Deployment Preparation

- **Create deployment scripts** for the target environment
- **Test deployment package creation**:
  ```bash
  dotnet publish -c Release -o ./publish
  ```
- **Verify published output** contains all necessary files and dependencies
- **Test the published application** in an environment similar to production
- **Validate AWS CDK deployment** (if applicable):
  ```bash
  cdk deploy
  ```

## Final Checklist

- [ ] All projects build without errors
- [ ] All unit tests pass
- [ ] Web application runs successfully on target platforms
- [ ] Database connectivity verified
- [ ] Configuration files updated for cross-platform compatibility
- [ ] CDK infrastructure code synthesizes correctly
- [ ] Documentation updated
- [ ] Deployment artifacts tested