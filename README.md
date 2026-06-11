# kyc-verification-api
ASP.NET Core Web API for KYC verification with JWT authentication, PostgreSQL, EF Core, and layered architecture (Controllers, Services, Interfaces).

# KYC Verification API

A backend application built using ASP.NET Core and PostgreSQL to simulate a KYC (Know Your Customer) verification workflow.

## Features

* KYC document verification
* JWT Authentication
* Verification history tracking
* PostgreSQL database integration
* Entity Framework Core
* Dependency Injection
* Layered Architecture
* Swagger API documentation

## Technology Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* Swagger

## Project Structure

```text
Controllers/
Services/
Interfaces/
Models/
Data/
```

## API Endpoints

### Authentication

* POST /api/auth/login

### KYC

* POST /api/kyc/verify
* GET /api/kyc/status/{id}
* GET /api/kyc/history/{userId}

## Architecture Flow

Client Request
→ Controller
→ Service Layer
→ DbContext (EF Core)
→ PostgreSQL Database
→ Response

## Learning Outcomes

This project helped me understand:

* ASP.NET Core Web API development
* Dependency Injection
* JWT Authentication
* Entity Framework Core
* PostgreSQL integration
* Layered application architecture
* Asynchronous programming using async/await

