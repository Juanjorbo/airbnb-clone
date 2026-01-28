# 🏡 Airbnb Clone — Full Stack Project

A simplified Airbnb clone built as a full-stack portfolio project.

This project includes:
- Backend with **.NET 8 + Entity Framework Core + PostgreSQL**
- Frontend with **Next.js + React + TypeScript + Tailwind CSS**
- Authentication using **JWT**
- Real search filters
- Professional, production-style architecture

---

## 🚀 Tech Stack

### Backend
- .NET 8 (ASP.NET Core Web API)
- Entity Framework Core
- PostgreSQL (Docker)
- JWT Authentication
- Swagger / OpenAPI

### Frontend
- Next.js (App Router)
- React + TypeScript
- Tailwind CSS
- Server Components + Client Components
- Direct fetch to own REST API

### Infrastructure
- Docker + Docker Compose (PostgreSQL)
- Git + GitHub

---

## 🧱 Architecture

```
airbnb-clone/
├── src/
│   └── AirbnbClone.Api/      # .NET Backend
├── frontend/                # Next.js Frontend
├── infra/
│   └── docker-compose.yml   # PostgreSQL
└── README.md
```

- REST API separated from frontend  
- HTTP communication (`/listings`, `/auth`, etc.)  
- Query-based filtering  
- Database migrations with EF Core  

---

## ✨ Implemented Features

### Backend
- User registration and login with JWT  
- Users CRUD  
- Listings CRUD  
- Search with filters:
  - city  
  - minimum / maximum price  
  - number of guests  
  - sorting  
  - pagination  
- Protected endpoints with authentication  
- Automatic migrations  

### Frontend
- Airbnb-like header  
- Main search bar  
- Responsive listings grid  
- Cards with image, rating and price  
- Real connection to the API  
- Mobile + desktop design  
- Listing detail page with dynamic routing (`/listings/[id]`)

---

## 🖥️ Run the project locally

### 1️⃣ Start PostgreSQL

From the project root:

```bash
docker compose -f infra/docker-compose.yml up -d
```

Make sure it is running on `localhost:5432`.

---

### 2️⃣ Start the backend

```bash
cd src/AirbnbClone.Api
dotnet run
```

Backend available at:

```
http://localhost:5088
http://localhost:5088/swagger
```

---

### 3️⃣ Start the frontend

In another terminal:

```bash
cd frontend
npm install
npm run dev
```

Frontend available at:

```
http://localhost:3000
```

---

## 🔑 Environment Variables

### Backend (`appsettings.json`)

```json
{
  "ConnectionStrings": {
    "Default": "Host=localhost;Port=5432;Database=airbnb;Username=airbnb_user;Password=airbnb_pass"
  },
  "Jwt": {
    "Key": "super-secret-key",
    "Issuer": "AirbnbClone",
    "Audience": "AirbnbClone"
  }
}
```

### Frontend (`frontend/.env.local`)

```env
NEXT_PUBLIC_API_BASE_URL=http://localhost:5088
```

---

## 🧪 Quick test flow

1. Register user via Swagger (`/auth/register`)  
2. Login (`/auth/login`)  
3. Create a listing (`/listings`)  
4. Open `http://localhost:3000`  
5. Test filters using the search bar  

---

## 📌 Project Goal

This project is designed as:

- A demonstration of **real full-stack architecture**  
- Good practices in:
  - layer separation  
  - authentication  
  - migrations  
  - efficient filtering  
  - modern UI  
- A project ready to show in technical interviews  

---

## 👤 Author

**Juan José Rincón**  
Full-Stack Developer  

- GitHub: https://github.com/Juanjorbo  
- Portfolio: https://portfolio-juanjorbo.vercel.app  

---

## 📝 Roadmap

- Listing detail page `/listings/[id]` (DONE) 
- Booking system with calendar  
- Host booking management  
- Real image uploads  
- Deployment (Railway / Fly.io / Vercel)  