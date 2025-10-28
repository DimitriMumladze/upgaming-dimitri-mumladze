# Book Management API

A RESTful Web API built with **.NET 6.0** for managing authors and books. This project demonstrates clean architecture principles with CQRS pattern, providing a solid foundation for scalable and maintainable enterprise applications.

## 🚀 Features

- **Author Management**: Complete CRUD operations for authors
- **Book Management**: Full CRUD operations with filtering and sorting capabilities
- **Relationship Management**: Books are linked to authors with cascade delete functionality
- **Advanced Queries**: 
  - Get all books by a specific author
  - Filter books by publication year
  - Sort books by title, publication year, or author
- **Input Validation**: Comprehensive validation using FluentValidation
- **Auto-Mapping**: DTO mapping using AutoMapper
- **API Documentation**: Swagger UI for interactive API exploration
- **Logging**: Built-in logging for all operations
- **Seed Data**: Pre-populated with sample authors and books

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

### Layers

1. **Domain** - Core entities and interfaces
   - Entities (`Author`, `Book`)
   - Repository interfaces

2. **Application** - Business logic and use cases
   - DTOs for data transfer
   - Command/Query handlers (CQRS pattern via MediatR)
   - Validation rules (FluentValidation)
   - Mapping profiles (AutoMapper)
   - Request/Response models

3. **Infrastructure** - Data access and external concerns
   - Entity Framework Core DbContext
   - Repository implementations
   - Database migrations
   - Seed data configuration

4. **Presentation** - API controllers and configuration
   - RESTful API controllers
   - Swagger documentation
   - Program setup and configuration

## 🛠️ Technology Stack

- **.NET 6.0**
- **Entity Framework Core 6.0** - ORM for database operations
- **SQL Server** - Database
- **MediatR** - CQRS implementation
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation
- **Swagger/OpenAPI** - API documentation

## 📋 Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server Express
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## 🔧 Installation & Setup

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/upgaming-dimitri-mumladze.git
cd upgaming-dimitri-mumladze
```

### 2. Update Connection String (Secret)

### 3. Apply Migrations

Run the Entity Framework migrations to create the database:

```bash
cd Infrastructure
dotnet ef database update --startup-project ../upgaming-dimitri-mumladze
```

Or from the root directory:

```bash
dotnet ef database update --project Infrastructure --startup-project upgaming-dimitri-mumladze
```

### 4. Run the Application

```bash
cd upgaming-dimitri-mumladze
dotnet run
```

The API will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger UI**: `https://localhost:5001/swagger`

## 📚 API Endpoints

### Authors

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Authors` | Get all authors with their books |
| GET | `/api/Authors/{id}` | Get author by ID with their books |
| GET | `/api/Authors/{id}/books` | Get all books by a specific author |
| POST | `/api/Authors` | Create a new author |
| PUT | `/api/Authors/{id}` | Update an existing author |
| DELETE | `/api/Authors/{id}` | Delete an author (cascades to books) |

### Books

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/Books` | Get all books (supports filtering and sorting) |
| GET | `/api/Books/{id}` | Get book by ID |
| POST | `/api/Books` | Create a new book |
| PUT | `/api/Books/{id}` | Update an existing book |
| DELETE | `/api/Books/{id}` | Delete a book |

#### Query Parameters for GET `/api/Books`
- `publicationYear` (optional): Filter books by publication year
- `sortBy` (optional): Sort by field (`title`, `publicationyear`, `author`)

### Example Requests

#### Create Author
```bash
POST /api/Authors
Content-Type: application/json

{
  "name": "Jane Austen"
}
```

#### Get Books by Year
```bash
GET /api/Books?publicationYear=1949&sortBy=title
```

#### Create Book
```bash
POST /api/Books
Content-Type: application/json

{
  "title": "Pride and Prejudice",
  "authorId": 1,
  "publicationYear": 1813
}
```

## 🗂️ Project Structure

```
upgaming-dimitri-mumladze/
├── Application/                 # Business logic layer
│   ├── Dtos/                   # Data Transfer Objects
│   ├── Features/               # CQRS implementation
│   │   ├── AuthorFeatures/
│   │   │   ├── Commands/       # Write operations
│   │   │   └── Queries/        # Read operations
│   │   └── BookFeatures/
│   ├── Mapping/                # AutoMapper profiles
│   ├── Validations/            # FluentValidation rules
│   └── Extensions/             # DI configurations
├── Domain/                     # Core domain layer
│   ├── Entities/              # Domain models
│   └── Interfaces/            # Repository contracts
├── Infrastructure/             # Data access layer
│   ├── Persistence/           # DbContext and configurations
│   ├── Repositories/          # Repository implementations
│   └── Migrations/            # EF Core migrations
└── upgaming-dimitri-mumladze/ # Presentation layer
    ├── Controllers/           # API controllers
    ├── Program.cs           # Application entry point
    └── appsettings.json     # Configuration
```

## 🎯 Key Design Patterns

- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations
- **Repository Pattern**: Abstracts data access logic
- **Unit of Work**: Managed by EF Core DbContext
- **Dependency Injection**: Native .NET DI container
- **Mediator Pattern**: Via MediatR for decoupled communication
- **Validation Pipeline**: Pipeline behavior for automatic validation

## 🔒 Validation Rules

### Author
- Name is required
- Name length: 3-100 characters

### Book
- Title is required and cannot be empty
- Publication year cannot be in the future
- AuthorId must correspond to an existing author

## 📦 NuGet Packages

### Application Layer
- `MediatR` - CQRS implementation
- `AutoMapper` - Object mapping
- `FluentValidation` - Validation

### Infrastructure Layer
- `Microsoft.EntityFrameworkCore` - ORM
- `Microsoft.EntityFrameworkCore.SqlServer` - SQL Server provider
- `Pomelo.EntityFrameworkCore.MySql` - MySQL provider (alternative)

### Presentation Layer
- `Swashbuckle.AspNetCore` - Swagger/OpenAPI

## 🧪 Database Schema

### Author
- `Id` (int, Primary Key)
- `Name` (string)

### Book
- `Id` (int, Primary Key)
- `Title` (string)
- `AuthorId` (int, Foreign Key to Author)
- `PublicationYear` (int)

**Relationship**: One Author to Many Books (Cascade Delete)

## 📊 Seed Data

The database is automatically seeded with the following sample data:

**Authors:**
- J.K. Rowling
- George Orwell
- J.R.R. Tolkien

**Books:**
- Harry Potter series (J.K. Rowling)
- 1984 and Animal Farm (George Orwell)
- The Hobbit and The Lord of the Rings (J.R.R. Tolkien)

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📝 License

This project is part of an Upgaming technical assessment.

## 👤 Author

**Dimitri Mumladze**

---

For more information about the API, visit the Swagger documentation at `/swagger` when running the application.
