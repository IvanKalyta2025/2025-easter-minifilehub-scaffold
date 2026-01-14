
Reload Window









# 🗂️ MiniFileHub

A simple, containerized file management system for backend developers.

This project is designed as a short, hands-on backend engineering challenge. Over the course of the easter, you’ll set up a multi-service system using Docker Compose, implement file management logic in ASP.NET, and integrate storage with PostgreSQL and MinIO.

---

## 📦 Stack Overview

| Component   | Technology        | Purpose                           |
|-------------|-------------------|-----------------------------------|
| Ingress     | Traefik           | Reverse proxy & service routing   |
| Backend API | ASP.NET Core      | File upload, listing, deletion    |
| Database    | PostgreSQL        | File metadata & logs              |
| Storage     | MinIO (S3 API)    | Actual file content               |

### Tooling

#### Required

- Container Runtime with compose support [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET SDK 9.0](https://dotnet.microsoft.com/en-us/download)

#### Handy

- Development Environment [Nix](https://nixos.org/)

---

## 🎯 Project Goals

By completing this project, you will:

- Set up a multi-container architecture with Docker Compose.
- Build and expose an HTTP API using ASP.NET Core.
- Store file metadata in PostgreSQL and file contents in MinIO.
- Configure Traefik to route external requests to your backend.
- Reflect on real-world system patterns: service orchestration, consistency, and interface design.

---

## 🔧 Getting Started

### 1. Clone the repo

```sh
git clone https://github.com/LarsGJobloop/2025-easter-minifilehub-scaffold minifilehub
cd minifilehub
```

### 2. Spin up the stack

```bash
docker compose up --build
```

### Services will include:

- Backend API at http://file-service.localhost
- PostgreSQL available internally on port 5432
- MinIO API at http://localhost:9000

#### Admin Dashboards
- Traefik at http://localhost:9001 (dashboard)
  - Unsecured
- MinIO Dashboard at http://localhost:9002
  - User: `minioadmin`, Password: `minioadmin`
- PostgreSQL Admin Dashboard at http://localhost:9003
  - User: `admin@mail.com`, Password: `postgresql`

## 📁 API Overview

| Method | Endpoint      | Description               |
|--------|---------------|---------------------------|
| POST   | /files/upload | Upload a file (form-data) |
| GET    | /files        | List uploaded files       |
| DELETE | /files/{id}   | Delete file by ID         |

⚠️ Make sure uploaded files go to MinIO, and their metadata (filename, size, date, optional tags) is stored in PostgreSQL.

## 🧠 Reflective Prompts

- What happens if the DB insert succeeds but the MinIO upload fails?
- How might you ensure consistency across these systems?
- Where would you add logging in a production system?
- How might authentication or RBAC be layered onto this architecture?

## 🌱 Bonus Ideas

- Add file tagging or versioning.
- Implement basic auth or TLS for Traefik.
- Add a simple frontend using Razor or a JS framework.
- Log API events to PostgreSQL using Serilog.
- Create an IaC module (e.g., Terraform) to deploy this system to a cloud/VPS provider (e.g., Hetzner).

## ✅ Deliverables

- A working multi-service stack (docker-compose.yml)
- Functional HTTP API with upload/list/delete
- Metadata persisted in Postgres, files stored in MinIO
- Code hosted in a public or shared Git repository

## 😵‍💫 Confused

Create an issue using the GitHub issue tracker and we will get to you as soon as possible. Weekdays, mostly 8-16 (GMT+1).
