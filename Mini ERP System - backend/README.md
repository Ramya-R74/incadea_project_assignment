# Mini ERP System - backend

## 📌 Project Overview
This is a **Mini ERP System** built with **.NET 8 Web API**, **Angular 19**, and **PostgreSQL**. It includes features such as:
- **User Authentication (JWT-based)** with role management
- **Admin Dashboard** to manage users, products, sales, and purchases
- **Inventory Management** (auto-updates on purchases/sales)
- **Role-based Access Control** (Admin, Sales, Customer, and Purchase roles)

## 🚀 Tech Stack
- **Backend**: .NET 8 Web API, Entity Framework Core
- **Frontend**: Angular 19, Bootstrap
- **Database**: PostgreSQL
- **Authentication**: JWT (JSON Web Token)
- **Containerization**: Docker (optional)

## 📂 Project Structure
```
Mini-ERP-System/
├── Backend/             # .NET 8 Web API
│   ├── Controllers/     # API Controllers
│   ├── Models/         # Entity Models
│   ├── Data/           # EF Core Database Context
│   ├── Services/       # Business Logic
│   └── Program.cs      # Entry Point
│
├── Frontend/           # Angular 19 Application
│   ├── src/
│   ├── components/     # UI Components
│   ├── services/       # API Services
│   ├── pages/          # Feature Pages
│   ├── app.module.ts   # Angular Modules
│   └── main.ts         # Main Entry Point
│
└── README.md           # Project Documentation
```

## 🔧 Setup & Installation
### **1. Clone the Repository**
```sh
git clone https://github.com/your-username/mini-erp-system.git
cd mini-erp-system
```

### **2. Backend Setup (.NET 8 Web API)**
```sh
cd Backend

# Install dependencies
dotnet restore

# Apply database migrations
dotnet ef database update

# Run the API
dotnet run
```

### **3. Frontend Setup (Angular 19)**
```sh
cd Frontend

# Install dependencies
npm install

# Start Angular development server
ng serve --open
```

## 🔑 API Key Configuration
Make sure you have an API key in your `appsettings.json`:
```json
{
  "ApiKey": "your-secure-api-key",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=mini_erp;Username=postgres;Password=yourpassword"
  }
}
```

## 📌 Features
- ✅ **Role-based Authentication** (Admin, Sales, Customer, Purchase)
- ✅ **User & Product Management**
- ✅ **CRUD Operations** for Customers & Suppliers
- ✅ **Inventory Auto-Update** on Purchases/Sales
- ✅ **Admin API Key Middleware**

## 🔗 API Endpoints
| Method | Endpoint | Description |
|--------|---------|-------------|
| `POST` | `/api/auth/login` | User login (JWT authentication) |
| `GET` | `/api/products` | Get all products |
| `POST` | `/api/products/add` | Add a new product (Admin only) |
| `GET` | `/api/customers` | Get customer list (Admin only) |
| `PUT` | `/api/customers/update/{id}` | Update customer info (Admin only) |
| `DELETE` | `/api/customers/delete/{id}` | Delete customer (Admin only) |

## 🛠️ Troubleshooting
**500 Server Error on Swagger?**
- Check if your database is running and migrations are applied: `dotnet ef database update`
- Ensure the API key is set correctly in `appsettings.json`

**Build Errors?**
- Stop any running instance of the app (`taskkill /F /IM "Mini ERP System.exe"` in CMD)
- Clean and rebuild the project:
  ```sh
  dotnet clean
  dotnet build
  ```

## 🤝 Contributing
Pull requests are welcome! Please open an issue first to discuss any changes.

## 📝 License
This project is licensed under the **MIT License**.

---
🔗 **GitHub Repo**: [Your Repository URL](https://github.com/Ramya-R74/incadea_project_assignment)

