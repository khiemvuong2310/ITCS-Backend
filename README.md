# 🧬 Fertility Service and Cryobank Management System - Backend

## 📖 Overview
This repository contains the **Backend API** for the **Fertility Service and Cryobank Management System** — a digital solution to manage fertility clinic operations, cryobank services, treatment cycles, and staff coordination efficiently.

Built with **ASP.NET Core 8.0 Web API** following **Clean Architecture** and **Domain-Driven Design (DDD)** principles for scalability, maintainability, and testability.

---

## ⚙️ Tech Stack

| Category | Technologies |
|-----------|--------------|
| **Framework** | ASP.NET Core 8.0 |
| **Architecture** | Clean Architecture, Domain-Driven Design (DDD) |
| **ORM** | Entity Framework Core |
| **Database** | Microsoft SQL Server |
| **Authentication** | JWT (JSON Web Token) |
| **Logging** | Serilog |
| **Mapping** | AutoMapper |
| **Validation** | FluentValidation |
| **Documentation** | Swagger / OpenAPI |
| **Caching** | MemoryCache / Redis (optional) |
| **Containerization** | Docker (optional) |

---

## 🌐 Live Demo

You can access the hosted API on MonsterASP here:

👉 **Swagger UI:** [https://cryofert.runasp.net/swagger/index.html](https://cryofert.runasp.net/swagger/index.html)

> 🧭 This is the live deployment of the ASP.NET Core Web API for demonstration and testing purposes.

---

## 🧩 Core Features

### 👩‍⚕️ Patient Management
- Create, update, delete, and view patients  
- Manage relationships between patients (e.g., couples)  
- View treatment and appointment history  

### 🧑‍⚕️ Doctor Management
- Manage doctor profiles, specialties, and availability  
- Assign doctors to appointments or treatment cycles  

### 📅 Appointment Management
- Booking, approving, and canceling appointments  
- Doctor confirmation workflow  
- Appointment and schedule tracking  

### 💉 Treatment Cycle
- Create and manage patient treatment cycles  
- Track treatment progress and reports  
- Link with cryobank storage records  

### 🧊 Cryobank Management
- Manage frozen embryos, sperm, and eggs  
- Track storage location and retrieval history  
- Manage storage capacity and automated reminders  

### 🔔 Notification (optional)
- System notifications for cycle updates or schedule changes  

---

## 🏗️ Project Structure (Clean Architecture)

```

src/
│
├── Application/
│   ├── Interfaces/
│   ├── Services/
│   ├── DTOs/
│   └── Validation/
│
├── Domain/
│   ├── Entities/
│   ├── Enums/
│   └── ValueObjects/
│
├── Infrastructure/
│   ├── Persistence/
│   ├── Repository/
│   └── Configurations/
│
└── WebAPI/
├── Controllers/
├── Middlewares/
└── Program.cs

````

---

## 🚀 Getting Started

### 1️⃣ Prerequisites
Before you begin, ensure you have the following installed:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) (for API testing)

### 2️⃣ Setup Database
```bash
dotnet ef database update
````

### 3️⃣ Run the API

```bash
dotnet run --project WebAPI
```

The API will be available locally at:

```
https://localhost:5001
http://localhost:5000
```

Or use the hosted version:

```
https://cryofert.runasp.net/swagger/index.html
```

---

## 📘 API Documentation

Swagger UI is automatically generated at runtime.

**🔗 Local:**
`https://localhost:5001/swagger`

**🔗 Online:**
[https://cryofert.runasp.net/swagger/index.html](https://cryofert.runasp.net/swagger/index.html)

You can explore all available endpoints, models, and responses directly in Swagger UI.

---

## 🔐 Authentication

All secure endpoints require a **Bearer Token**.
Login API returns a JWT token that must be attached to every authorized request.

**Example Header:**

```
Authorization: Bearer <your_token_here>
```

---

## 🧠 Key Entities

* **Patient**
* **Doctor**
* **Appointment**
* **TreatmentCycle**
* **CryoStorage**
* **Relationship**

---

## 🧰 Libraries & Tools

| Purpose         | Library               |
| --------------- | --------------------- |
| Logging         | Serilog               |
| Object Mapping  | AutoMapper            |
| Validation      | FluentValidation      |
| CQRS (optional) | MediatR               |
| Documentation   | Swagger / Swashbuckle |
| Unit Testing    | xUnit / Moq           |

---

## 🧾 Example API Endpoints

| Endpoint              | Method | Description             | Auth |
| --------------------- | ------ | ----------------------- | ---- |
| `/api/patient`        | GET    | Get all patients        | ✅    |
| `/api/patient/{id}`   | GET    | Get patient by ID       | ✅    |
| `/api/patient`        | POST   | Create new patient      | ✅    |
| `/api/appointment`    | POST   | Book a new appointment  | ✅    |
| `/api/treatmentcycle` | GET    | Get treatment cycles    | ✅    |
| `/api/auth/login`     | POST   | Login and get JWT token | ❌    |

---

## 📦 Deployment

### 🖥️ Local

Use Visual Studio → **Publish → Folder or IIS**

### ☁️ Production

CI/CD pipeline setup (GitHub Actions, Azure, or Docker)

**Docker example:**

```bash
docker build -t fertility-backend.
docker run -d -p 8080:80 fertility-backend
```

---

## 🧭 Future Enhancements

* Payment gateway integration
* Email & SMS notification service
* Cloud storage for reports and documents
* Role-based dashboard analytics

---

## 👥 Authors

**ITCS Team**
Faculty of Information Technology, University of [Your University Name]

---

## 📄 License

This project is licensed under the [Apache 2.0 License](LICENSE).

---

⭐ **If you like this project, don’t forget to star the repository!**
