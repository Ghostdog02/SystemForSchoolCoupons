# 🎯 System For School Coupons

A full-stack ASP.NET Core MVC web application built using the **Code-First** approach with **Entity Framework Core** and **Microsoft Identity** for robust authentication and role-based access control. It features scaffolded Identity UI pages for rapid development and user management.

---

## 🚀 Features

- ✅ ASP.NET Core MVC architecture  
- ✅ **Code-First** Entity Framework Core database setup  
- ✅ **Microsoft Identity** for authentication and role-based access control  
- ✅ Scaffolded Identity pages for login, register, user roles, etc.  
- ✅ Secure authorization using `[Authorize]` attributes  
- ✅ Responsive UI using Bootstrap (or your CSS framework of choice)  

---

## 🛠️ Tech Stack

- ASP.NET Core MVC  
- Entity Framework Core (Code-First)  
- Microsoft Identity  
- SQL Server or SQLite (configurable)  
- Bootstrap 5 (or other CSS frameworks)  

---

## 🔧 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/project-name.git
cd project-name
```
### 2. Apply Migrations and Update Database
```bash
dotnet ef database update
```
Ensure dotnet-ef is installed globally with:
```bash
dotnet tool install --global dotnet-ef
```
### 3. Run the Application
```bash
dotnet run
```
## 🧑‍💻 Identity Scaffold Pages
This project includes scaffolded Identity pages for:

- User registration and login

- Role management

- Password reset and email confirmation

## 🛡️ Role-Based Authorization
Example usage:
```csharp
[Authorize(Roles = "Admin")]
public IActionResult AdminOnly()
{
    return View();
}
```
Roles can be seeded or managed directly in the database.

Custom pages are located in Areas/Identity/Pages/Account
