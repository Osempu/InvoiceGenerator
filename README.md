# 📄 Invoice Generator

A modern, full-stack invoice management system built with **Clean Architecture** principles using .NET 9, Entity Framework Core, and Blazor WebAssembly.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%209.0-512BD4?style=flat-square)
![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?style=flat-square)
![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)

## 🌟 Features

### 💼 **Business Features**
- ✅ **Customer Management** - Create, update, and manage customer information
- 📄 **Invoice Creation** - Generate professional invoices with line items
- 💰 **Automated Calculations** - Tax, subtotals, and totals calculated automatically  
- 🎨 **Professional Templates** - Clean, printable invoice layouts
- 📊 **Dashboard Overview** - Quick insights into your business metrics
- 🔍 **Search & Filter** - Find invoices and customers quickly

### 🛠️ **Technical Features**
- 🏗️ **Clean Architecture** - Maintainable and testable codebase
- 🌐 **RESTful API** - Built with Carter for minimal APIs
- ⚡ **Blazor WebAssembly** - Rich, interactive client-side UI
- 🗄️ **Entity Framework Core** - Code-first database approach
- 📱 **Responsive Design** - Works on desktop, tablet, and mobile
- 🔐 **Secure** - Following security best practices
- 📊 **Structured Logging** - Comprehensive observability with Serilog

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────────────────────────────────────────────────┐
│                     Presentation Layer                      │
├─────────────────────┬───────────────────────────────────────┤
│   Blazor Client     │            Web API                    │
│   (UI/Components)   │      (Controllers/Endpoints)          │
├─────────────────────┴───────────────────────────────────────┤
│                    Application Layer                        │
│              (Services, DTOs, Interfaces)                   │
├─────────────────────────────────────────────────────────────┤
│                     Domain Layer                            │
│                (Entities, Value Objects)                    │
├─────────────────────────────────────────────────────────────┤
│                  Infrastructure Layer                       │
│            (Data Access, External Services)                 │
└─────────────────────────────────────────────────────────────┘
```

### 📁 **Project Structure**
```
InvoiceGenerator/
├── 📂 InvoiceGenerator.Core/           # Domain entities and business rules
├── 📂 InvoiceGenerator.Application/    # Business logic and services
├── 📂 InvoiceGenerator.Infrastructure/ # Data access and external services
├── 📂 InvoiceGenerator.API/           # Web API endpoints
├── 📂 InvoiceGenerator.Client/        # Blazor WebAssembly frontend
└── 📄 InvoiceGenerator.sln           # Solution file
```

## 🚀 Getting Started

### 📋 **Prerequisites**

Make sure you have the following installed:
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (9.0 or later)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB is fine)
- [Git](https://git-scm.com/)

### 🔧 **Installation**

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/InvoiceGenerator.git
   cd InvoiceGenerator
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   
   Open `InvoiceGenerator.API/appsettings.json` and update the connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InvoiceGeneratorDb;Trusted_Connection=true;"
     }
   }
   ```

4. **Create and seed the database**
   ```bash
   cd InvoiceGenerator.API
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   # Start the API (from InvoiceGenerator.API folder)
   dotnet run
   
   # In another terminal, start the Blazor client (from InvoiceGenerator.Client folder)
   cd ../InvoiceGenerator.Client
   dotnet run
   ```

6. **Open in browser**
   - API: `https://localhost:7071` (Swagger UI available)
   - Client: `https://localhost:7072`

## 🎯 **Usage**

### 👥 **Managing Customers**
1. Navigate to the **Customers** page
2. Click **"Add Customer"** to create a new customer
3. Fill in customer details (name, email, address)
4. Save and start creating invoices!

### 📄 **Creating Invoices**
1. Go to **Invoices** → **"Create New"**
2. Select a customer from the dropdown
3. Add line items with descriptions, quantities, and prices
4. Review the automatically calculated totals
5. Save and generate PDF (coming soon!)

### 📊 **Dashboard Insights**
- View recent invoices
- See customer statistics
- Monitor revenue trends
- Quick access to frequent actions

## 🧪 **Testing**

Run the test suite to ensure everything works correctly:

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test InvoiceGenerator.Core.UnitTests
```

## 📚 **API Documentation**

The API documentation is available via Swagger UI when running the application:

- **Local**: `https://localhost:7071/swagger`
- **Key Endpoints**:
  - `GET /api/customers` - List all customers
  - `POST /api/customers` - Create new customer
  - `GET /api/invoices` - List all invoices
  - `POST /api/invoices` - Create new invoice

## 🛠️ **Built With**

### **Backend**
- **[.NET 9](https://dotnet.microsoft.com/)** - Cross-platform framework
- **[Entity Framework Core 9](https://docs.microsoft.com/en-us/ef/core/)** - ORM for data access
- **[Carter](https://github.com/CarterCommunity/Carter)** - Minimal API framework
- **[Serilog](https://serilog.net/)** - Structured logging
- **[AutoMapper](https://automapper.org/)** - Object-to-object mapping

### **Frontend**
- **[Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/)** - Interactive web UI
- **[Bootstrap 5](https://getbootstrap.com/)** - CSS framework
- **[Blazored Components](https://github.com/Blazored)** - Additional Blazor components

### **Database**
- **[SQL Server](https://www.microsoft.com/en-us/sql-server/)** - Primary database
- **[SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)** - Development database

### **Testing**
- **[xUnit](https://xunit.net/)** - Testing framework
- **[Moq](https://github.com/moq/moq4)** - Mocking framework
- **[FluentAssertions](https://fluentassertions.com/)** - Assertion library

## 🤝 **Contributing**

Contributions are welcome! Please follow these steps:

1. **Fork** the repository
2. **Create** a feature branch (`git checkout -b feature/AmazingFeature`)
3. **Commit** your changes (`git commit -m 'Add some AmazingFeature'`)
4. **Push** to the branch (`git push origin feature/AmazingFeature`)
5. **Open** a Pull Request

### 📝 **Development Guidelines**
- Follow **Clean Architecture** principles
- Write **unit tests** for new features
- Use **structured logging** for observability
- Follow **C# coding conventions**
- Update documentation as needed

## 🗺️ **Roadmap**

### 🎯 **Version 1.0** (Current)
- [x] Customer management
- [x] Basic invoice creation
- [x] Clean architecture setup
- [x] API with Swagger documentation

### 🚀 **Version 1.1** (Next)
- [ ] PDF invoice generation
- [ ] Email invoice sending
- [ ] Advanced search and filtering
- [ ] Invoice templates customization

### 🌟 **Version 2.0** (Future)
- [ ] Multi-tenant support
- [ ] Authentication & authorization
- [ ] Payment tracking
- [ ] Reporting and analytics
- [ ] Mobile app (MAUI)

## 📄 **License**

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 **Author**

**Oscar** - *Initial work* - [GitHub Profile](https://github.com/yourusername)

## 🙏 **Acknowledgments**

- Inspired by modern Clean Architecture patterns
- Thanks to the .NET community for excellent tooling
- Carter framework for simplified minimal APIs
- Blazor team for making WebAssembly development enjoyable

---

## 📞 **Support**

If you have any questions or need help getting started:

1. Check the [Issues](https://github.com/yourusername/InvoiceGenerator/issues) for common problems
2. Create a [new issue](https://github.com/yourusername/InvoiceGenerator/issues/new) if you find a bug
3. Start a [discussion](https://github.com/yourusername/InvoiceGenerator/discussions) for general questions

---

⭐ **Star this repository if you found it helpful!**

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/yourusername)