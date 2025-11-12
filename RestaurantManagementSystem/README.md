# Restaurant Management System - Windows Forms

A comprehensive Windows Forms application for restaurant management, featuring user management, order processing, menu management, and more.

## Prerequisites

- .NET 6.0 or later
- Windows 10 or later
- Visual Studio 2022 or VS Code with C# extensions

## Installation

### 1. Clone or Download the Project
```bash
git clone https://github.com/Zardd99/RestaurantManagementSystem
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



# 🍽️ Restaurant Management System API Documentation

## Table of Contents
- [Introduction](#introduction)
- [Base URL](#base-url)
- [Authentication](#authentication)
- [API Endpoints](#api-endpoints)
  - [Authentication Endpoints](#authentication-endpoints)
  - [Menu Management Endpoints](#menu-management-endpoints)
  - [Order Management Endpoints](#order-management-endpoints)
  - [Review & Rating Endpoints](#review--rating-endpoints)
  - [Inventory Management Endpoints](#inventory-management-endpoints)
  - [Receipt Management Endpoints](#receipt-management-endpoints)
  - [User Management Endpoints (Admin Only)](#user-management-endpoints-admin-only)
- [Request/Response Examples](#requestresponse-examples)
- [Error Handling](#error-handling)
- [Rate Limiting](#rate-limiting)
- [Testing](#testing)

---

## Introduction

The Restaurant Management System API provides a complete set of endpoints to manage restaurant operations including **menu items**, **orders**, **inventory**, **customers**, and **payments**. This **RESTful API** uses **JSON** for request and response payloads.

---

## Base URL

**Development URL:**
`http://localhost:5000/api`

**Production URL:**
`https://nontenurial-fawn-socketless.ngrok-free.dev/api`

---

## Authentication

### JWT Authentication
Most endpoints require **JWT authentication**. Include the token in the **`Authorization`** header:

```http
Authorization: Bearer <your_jwt_token>
Getting an Authentication TokenRegister a New UserPOST /api/auth/registerRequest Body:JSON{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "password123",
  "role": "customer"
}
Response:JSON{
  "success": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "_id": "507f1f77bcf86cd799439011",
    "name": "John Doe",
    "email": "john@example.com",
    "role": "customer"
  }
}
LoginPOST /api/auth/loginRequest Body:JSON{
  "email": "john@example.com",
  "password": "password123"
}
Response: Same as register response.API EndpointsAuthentication EndpointsMethodEndpointDescriptionAuth RequiredPOST/auth/registerRegister new userNoPOST/auth/loginUser loginNoGET/auth/meGet current userYesPUT/auth/updateUpdate user profileYesPUT/auth/change-passwordChange passwordYesMenu Management EndpointsGet All Menu ItemsGET /api/menuQuery Parameters:category (string) - Filter by category namedietary (string) - Filter by dietary tagssearch (string) - Search in name and descriptionavailable (boolean) - Filter by availabilitychefSpecial (boolean) - Filter chef specialsExample:GET /api/menu?category=Main Course&dietary=vegetarian&available=trueResponse:JSON[
  {
    "_id": "507f1f77bcf86cd799439011",
    "name": "Vegetable Pasta",
    "description": "Fresh pasta with seasonal vegetables",
    "price": 15.99,
    "category": {
      "_id": "507f1f77bcf86cd799439012",
      "name": "Main Course"
    },
    "dietaryTags": ["vegetarian"],
    "availability": true,
    "preparationTime": 20,
    "chefSpecial": false,
    "averageRating": 4.5,
    "reviewCount": 23
  }
]
Get Single Menu ItemGET /api/menu/:idCreate Menu ItemPOST /api/menuRequest Body:JSON{
  "name": "New Dish",
  "description": "Delicious new dish",
  "price": 12.99,
  "category": "Main Course",
  "dietaryTags": ["vegetarian"],
  "availability": true,
  "preparationTime": 15,
  "chefSpecial": true
}
Update Menu ItemPUT /api/menu/:idDelete Menu ItemDELETE /api/menu/:idOrder Management EndpointsGet All OrdersGET /api/ordersQuery Parameters:status (string) - Filter by order statuscustomer (string) - Filter by customer IDorderType (string) - Filter by order typestartDate (string) - Filter orders from date (YYYY-MM-DD)endDate (string) - Filter orders to date (YYYY-MM-DD)minAmount (number) - Minimum order amountmaxAmount (number) - Maximum order amountExample:GET /api/orders?status=preparing&startDate=2024-01-01&orderType=dine-inCreate OrderPOST /api/ordersRequest Body:JSON{
  "items": [
    {
      "menuItem": "507f1f77bcf86cd799439011",
      "quantity": 2,
      "specialInstructions": "No onions please",
      "price": 15.99
    }
  ],
  "totalAmount": 31.98,
  "customer": "507f1f77bcf86cd799439013",
  "tableNumber": 5,
  "orderType": "dine-in"
}
Update Order StatusPUT /api/orders/:id/statusRequest Body:JSON{
  "status": "preparing"
}
Get Order StatisticsGET /api/orders/statsResponse:JSON{
  "dailyEarnings": 1250.75,
  "weeklyEarnings": 8450.25,
  "yearlyEarnings": 152300.50,
  "bestSellingDishes": [
    {
      "name": "Vegetable Pasta",
      "quantity": 45,
      "revenue": 719.55
    }
  ]
}
Review & Rating EndpointsGet All ReviewsGET /api/reviewsQuery Parameters:user (string) - Filter by user IDmenuItem (string) - Filter by menu item IDrating (number) - Filter by exact ratingdateFrom (string) - Filter from datedateTo (string) - Filter to dateCreate ReviewPOST /api/reviewsRequest Body:JSON{
  "user": "507f1f77bcf86cd799439013",
  "menuItem": "507f1f77bcf86cd799439011",
  "rating": 5,
  "comment": "Excellent food and service!"
}
Get Rating StatisticsGET /api/ratings/statisticsResponse:JSON{
  "success": true,
  "data": {
    "totalReviews": 156,
    "averageRating": 4.3,
    "highestRating": 5,
    "lowestRating": 1,
    "ratingDistribution": [
      { "rating": 1, "count": 5 },
      { "rating": 2, "count": 12 },
      { "rating": 3, "count": 25 },
      { "rating": 4, "count": 64 },
      { "rating": 5, "count": 50 }
    ]
  }
}
Inventory Management EndpointsGet All SuppliersGET /api/suppliersQuery Parameters:active (boolean) - Filter by active statusCreate SupplierPOST /api/suppliersRequest Body:JSON{
  "name": "Fresh Produce Co.",
  "contactPerson": "Jane Smith",
  "email": "jane@freshproduce.com",
  "phone": "0123456789",
  "address": {
    "street": "123 Market St",
    "city": "Phnom Penh",
    "state": "Phnom Penh",
    "zipCode": "12000",
    "country": "Cambodia"
  },
  "paymentTerms": "Net 30",
  "notes": "Reliable supplier of fresh vegetables"
}
Receipt Management EndpointsCreate ReceiptPOST /api/receiptsRequest Body:JSON{
  "orderId": "507f1f77bcf86cd799439014",
  "paymentMethod": "credit_card",
  "discount": 5.00
}
Get Receipt by Order IDGET /api/receipts/order/:orderIdUser Management Endpoints (Admin Only)Get All UsersGET /api/usersUpdate UserPUT /api/users/:idRequest Body:JSON{
  "name": "Updated Name",
  "email": "updated@example.com",
  "role": "manager",
  "isActive": true
}
Request/Response ExamplesComplete Order Flow Example1. Get Available Menu ItemsHTTPGET /api/menu?available=true
2. Create OrderHTTPPOST /api/orders
JSON{
  "items": [
    {
      "menuItem": "507f1f77bcf86cd799439011",
      "quantity": 1,
      "price": 15.99
    },
    {
      "menuItem": "507f1f77bcf86cd799439012", 
      "quantity": 2,
      "price": 8.99
    }
  ],
  "totalAmount": 33.97,
  "customer": "507f1f77bcf86cd799439013",
  "orderType": "takeaway"
}
3. Update Order StatusHTTPPUT /api/orders/507f1f77bcf86cd799439015/status
JSON{
  "status": "preparing"
}
4. Create ReceiptHTTPPOST /api/receipts
JSON{
  "orderId": "507f1f77bcf86cd799439015",
  "paymentMethod": "cash"
}
5. Add ReviewHTTPPOST /api/reviews
JSON{
  "user": "507f1f77bcf86cd799439013",
  "menuItem": "507f1f77bcf86cd799439011",
  "rating": 5,
  "comment": "Great food, will order again!"
}
Error HandlingThe API uses standard HTTP status codes and returns error messages in JSON format.Common Error ResponsesStatus CodeExample MessageDescription400 Bad Request{"message": "Validation error", "errors": ["Name is required"]}Request body or parameters are invalid.401 Unauthorized{"message": "Invalid email or password"}Authentication failed (e.g., bad credentials or missing token).403 Forbidden{"message": "Access denied. Admin role required."}Authenticated user lacks necessary permissions.404 Not Found{"message": "Menu item not found"}The requested resource does not exist.409 Conflict{"message": "User already exists with this email"}The request conflicts with the current state of the server.500 Internal Server Error{"message": "Server error", "error": "..."}General server-side error.Rate LimitingThe API implements rate limiting per 15-minute window to prevent abuse:General endpoints: 100 requests per 15 minutesAuthentication endpoints: 10 requests per 15 minutesAdmin endpoints: 1000 requests per 15 minutesRate limit headers are included in responses:HTTPX-RateLimit-Limit: 100
X-RateLimit-Remaining: 95
X-RateLimit-Reset: 1640995200 (Timestamp of reset)
TestingUsing PostmanImport Postman Collection:Download the Postman collection from /docs/postman-collection.json (Assumed path).Set Environment Variables:baseUrl: http://localhost:5000/apitoken: {{auth_token}} (Used to store the JWT after login/register)Test Flow:Start with registration/login to get the token.Set the token in environment variables.Test protected endpoints.Using cURL ExamplesRegister User:Bashcurl -X POST http://localhost:5000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Test User",
    "email": "test@example.com",
    "password": "password123",
    "role": "customer"
  }'
Get Menu Items:Bashcurl -X GET "http://localhost:5000/api/menu?available=true" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
Create Order:Bashcurl -X POST http://localhost:5000/api/orders \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -d '{
    "items": [
      {
        "menuItem": "507f1f77bcf86cd799439011",
        "quantity": 1,
        "price": 15.99
      }
    ],
    "totalAmount": 15.99,
    "customer": "507f1f77bcf86cd799439013",
    "orderType": "dine-in"
  }'