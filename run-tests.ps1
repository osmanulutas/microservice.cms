# Test Runner Script for Microservice.CMS
# This script runs all tests for Account and Content projects

Write-Host "Running tests for Microservice.CMS..." -ForegroundColor Green
Write-Host "=====================================" -ForegroundColor Green

# Function to run tests for a specific project
function Run-Tests {
    param(
        [string]$ProjectPath,
        [string]$ProjectName
    )
    
    Write-Host "`nRunning tests for $ProjectName..." -ForegroundColor Yellow
    Write-Host "Project path: $ProjectPath" -ForegroundColor Gray
    
    if (Test-Path $ProjectPath) {
        Set-Location $ProjectPath
        Write-Host "Running: dotnet test --verbosity normal" -ForegroundColor Cyan
        
        try {
            dotnet test --verbosity normal
            if ($LASTEXITCODE -eq 0) {
                Write-Host "✅ $ProjectName tests completed successfully!" -ForegroundColor Green
            } else {
                Write-Host "❌ $ProjectName tests failed!" -ForegroundColor Red
            }
        }
        catch {
            Write-Host "❌ Error running tests for $ProjectName: $_" -ForegroundColor Red
        }
    } else {
        Write-Host "❌ Project path not found: $ProjectPath" -ForegroundColor Red
    }
}

# Get the script directory
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $ScriptDir

# Run Account tests
$AccountTestPath = "src\Account\Microservice.Account.Test"
Run-Tests -ProjectPath $AccountTestPath -ProjectName "Account"

# Run Content tests
$ContentTestPath = "src\Content\Microservice.Content.Tests"
Run-Tests -ProjectPath $ContentTestPath -ProjectName "Content"

# Return to original directory
Set-Location $ScriptDir

Write-Host "`n=====================================" -ForegroundColor Green
Write-Host "Test execution completed!" -ForegroundColor Green
Write-Host "Check the output above for results." -ForegroundColor Gray

# Wait for user input before closing
Read-Host "`nPress Enter to exit"
