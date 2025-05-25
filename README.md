# 📝 Note API

This is a simple **Note-Taking REST API** built with **ASP.NET Core Web API**. It allows users to register, login, and perform CRUD (Create, Read, Update, Delete) operations on their personal notes. It supports JWT authentication and is designed to work with mobile or web clients like the [NoteApp React Native frontend](https://github.com/FaruqHAO/NoteApp).

## 🔗 Live API (Hosted on Render)

You can access the deployed API here:  
**https://notesapi-7r9d.onrender.com/swagger/index.html**

Example endpoints:
- `GET /api/Notes` – Get all notes (auth required)
- `POST /api/Auth/register` – Register a new user
- `POST /api/Auth/login` – Login and get JWT token

## 🧰 Technologies Used

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- PostgreSQL (database)
- JWT Bearer Authentication
- CORS enabled
- Hosted on [Render](https://render.com)

---

## 📦 API Features

- ✅ User Registration & Login (with JWT auth)
- ✅ Create, Read, Update, Delete Notes
- ✅ Only authenticated users can access their own notes
- ✅ RESTful API structure
- ✅ Deployed for free on Render

---

## 🚀 How to Run Locally

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- PostgreSQL installed locally or a connection string to a cloud database

### Steps

1. **Clone the repo**

```bash
git clone https://github.com/FaruqHAO/NoteAPI.git
cd NoteAPI
