BookStore Project
Description
A modern book shopping platform built with ASP.NET Core MVC and MongoDB, featuring user authentication, dynamic book categories, and special offers for featured, popular, and discounted books.
![Ekran görüntüsü 2025-02-28 153235](https://github.com/user-attachments/assets/2eacc025-6583-40f6-a527-f3ab5b945bd7)
Technologies & Libraries
Framework: ASP.NET Core MVC 8.0
Database: MongoDB
Authentication: Cookie-based Authentication
Frontend Libraries:
Bootstrap 5.1.0
jQuery 3.5.1
jQuery Validation 1.19.3
Key Features
User Authentication (Register/Login)
Book Management (CRUD operations)
Book Categories
Special Book Lists:
Featured Books
Popular Books
Discounted Books
Responsive Design
Image Management for Books
User Profile Management
----->>>>Prerequisites
.NET 8.0 SDK
MongoDB Server
Visual Studio 2022 or VS Code


Nuget packages:

PackageReference Include="MongoDB.Driver" 
PackageReference Include="Microsoft.AspNetCore.Authentication.Cookies" 

Project Structure;
MyMvcApp/
├── Controllers/
│   ├── AccountController.cs
│   ├── BooksController.cs
│   └── HomeController.cs
├── Models/
│   ├── Book.cs
│   ├── User.cs
│   └── HomeViewModel.cs
├── Views/
│   ├── Account/
│   ├── Books/
│   ├── Home/
│   └── Shared/
├── Services/
│   └── MongoDbService.cs
└── Settings/
    └── MongoDbSettings.cs 

MongoDb Configuration;
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "BookStore",
    "BooksCollectionName": "Books"
  }
}


Getting Started;
Clone the repository
Install MongoDB on your machine
Update the connection string in appsettings.json if needed
Run dotnet restore to install dependencies
Run dotnet run to start the application

Features in Detail;
Book Management:
Add new books with details (title, author, price, etc.)
Edit existing books
Delete books
View book details
Special tags for featured and discounted items
User Features:
User registration with validation
Secure login system
Profile management
Password hashing for security
UI Features:
Responsive design for all devices
Clean and modern interface
Image display for books
Price formatting with discount support
Horizontal scrolling for book lists


Future Enhancements;
Shopping Cart functionality
Order Management
Admin Dashboard
Advanced Search Features
User Reviews and Ratings
Payment Integration
