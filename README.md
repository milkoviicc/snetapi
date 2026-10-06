# Social Network API

A social network backend built with **ASP.NET Core** and **PostgreSQL**, developed as a final thesis project (Zavrsni rad).

> ⚠️ **Legacy project** — built early in my ASP.NET Core learning journey. Not actively maintained; may require updates to run with current dependencies. Kept here as a reference for early full-stack work with auth, social features, and media handling.

## Features

- **JWT Authentication** — token-based auth with access/refresh token flow
- **Posts & Comments** — full CRUD for user-generated content
- **Friend/Follow System** — connect with other users, manage relationships
- **Media Handling** — image uploads and storage via **Azure Blob Storage**

## Tech Stack

- **Backend**: ASP.NET Core Web API, C#
- **Database**: PostgreSQL
- **Storage**: Azure Blob Storage
- **Auth**: JWT (access + refresh tokens)

## Setup

```bash
git clone https://github.com/novosel2/snetapi.git
cd snetapi

# Configure environment
# Add your PostgreSQL connection string, JWT secret, and Azure Blob Storage credentials to config

# Run migrations
dotnet ef database update

# Start the API
dotnet run
```
