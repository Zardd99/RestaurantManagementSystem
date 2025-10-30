# Restaurant Management System - Windows Forms

A comprehensive Windows Forms application for restaurant management, featuring user management, order processing, menu management, and more.

## Prerequisites

- .NET 6.0 or later
- Windows 10 or later
- Visual Studio 2022 or VS Code with C# extensions

## Installation

### 1. Clone or Download the Project
```bash
git clone <repository-url>
cd RestaurantManagementSystem


# Restore NuGet packages
dotnet restore

# Add required packages
dotnet add package Microsoft.Extensions.DependencyInjection --version 6.0.0
dotnet add package Microsoft.Extensions.Configuration --version 6.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 6.0.0
dotnet add package Microsoft.Extensions.Http --version 6.0.0

Install-Package Microsoft.Extensions.DependencyInjection -Version 6.0.0
Install-Package Microsoft.Extensions.Configuration -Version 6.0.0
Install-Package Microsoft.Extensions.Configuration.Json -Version 6.0.0
Install-Package Microsoft.Extensions.Http -Version 6.0.0

RestaurantManagementSystem/
├── Models/          # Data models
├── ViewModels/      # UI view models
├── Managers/        # Business logic managers
├── Forms/           # Main application forms
├── Dialogs/         # Dialog forms
├── Services/        # API services
└── Program.cs       # Application entry point


## 8. Installation Scripts

### install-dependencies.ps1 (PowerShell)
```powershell
Write-Host "Installing Restaurant Management System Dependencies..." -ForegroundColor Green

# Check if .NET 6.0 is installed
$dotnetVersion = dotnet --version
if (-not $dotnetVersion.StartsWith("6.")) {
    Write-Host "Error: .NET 6.0 or later is required" -ForegroundColor Red
    exit 1
}

Write-Host "Found .NET $dotnetVersion" -ForegroundColor Green

# Restore NuGet packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore

# Add required packages
$packages = @(
    "Microsoft.Extensions.DependencyInjection",
    "Microsoft.Extensions.Configuration",
    "Microsoft.Extensions.Configuration.Json",
    "Microsoft.Extensions.Http"
)

foreach ($package in $packages) {
    Write-Host "Adding package: $package..." -ForegroundColor Yellow
    dotnet add package $package --version 6.0.0
}

Write-Host "All dependencies installed successfully!" -ForegroundColor Green
Write-Host "`nNext steps:" -ForegroundColor Cyan
Write-Host "1. Update appsettings.json with your API URL" -ForegroundColor White
Write-Host "2. Run 'dotnet build' to build the project" -ForegroundColor White
Write-Host "3. Run 'dotnet run' to start the application" -ForegroundColor White


#!/bin/bash

echo "Installing Restaurant Management System Dependencies..."

# Check if .NET 6.0 is installed
if ! dotnet --version | grep -q "^6\."; then
    echo "Error: .NET 6.0 or later is required"
    exit 1
fi

echo "Found .NET $(dotnet --version)"

# Restore NuGet packages
echo "Restoring NuGet packages..."
dotnet restore

# Add required packages
packages=(
    "Microsoft.Extensions.DependencyInjection"
    "Microsoft.Extensions.Configuration"
    "Microsoft.Extensions.Configuration.Json"
    "Microsoft.Extensions.Http"
)

for package in "${packages[@]}"; do
    echo "Adding package: $package..."
    dotnet add package $package --version 6.0.0
done

echo "All dependencies installed successfully!"
echo ""
echo "Next steps:"
echo "1. Update appsettings.json with your API URL"
echo "2. Run 'dotnet build' to build the project"
echo "3. Run 'dotnet run' to start the application"