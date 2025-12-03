📘 Project Overview

This project is a fully functional E-Commerce Platform built for learning and real-world implementation.
It demonstrates how to build scalable enterprise-level applications using:

✔ ASP.NET Core MVC (.NET 9)
✔ N-Tier Architecture
✔ Identity Framework for Authentication & Authorization
✔ Stripe for Payments
✔ EF Core with Code-First Migrations
✔ Repository Pattern
✔ Azure Deployment

The system includes product management, shopping cart, checkout, order processing, role management, and email notifications.

🚀 Features
🔐 Authentication & Authorization

ASP.NET Core Identity (RCL-based)

Extended user fields (Name, City, Address, etc.)

Role management (Admin, Customer, Employee , Company)

Login, Register, Forgot Password, Lockout

🛍 E-Commerce Core Functionalities

Product Catalog

Categories 

Shopping Cart

Checkout Process

Order Summary & Order Tracking

Stripe Payment Integration

Email Notifications

🖥 Frontend Features

Responsive UI using Bootstrap v5

Clean Razor Views + Layout + Partials

Custom Tag Helpers

View Components

⚙ Application Logic

N-Tier Architecture (UI , Controllers (Web) , Models, Data Access)

Repository & Unit of Work Pattern

Entity Framework Core (Code-First)

Automatic Database Seeding

Sessions & TempData

☁ Deployment

Configured for Microsoft Azure App Service


🏛 Architecture

This project uses a clean N-Tier Architecture:

📂 ECommerceApp
 ├── 📁 ECommerceApp.Web          → Presentation Layer (MVC, Razor)
 ├── 📁 ECommerceApp.Models         → Business Logic, Entities, Models
 ├── 📁 ECommerceApp.DataAccess   → EF Core, Repositories, Migrations


✔ Why N-Tier?

Separation of concerns

Maintainability

Reusability

Testability

Cleaner codebase

🛠 Technologies Used
Technology	Purpose
ASP.NET Core (.NET 9)	Main framework
ASP.NET Core MVC	Presentation layer
Razor Pages (Identity RCL)	User authentication
Entity Framework Core	ORM + Migrations
SQL Server	Database
Stripe API	Payment processing
Bootstrap v5	UI styling
Microsoft Azure	Deployment
Repository + Unit of Work	Data access pattern

🎯 Learning Objectives

This project helps you learn:

✔ Structure of ASP.NET Core MVC (.NET 9) Applications

✔ Structure of ASP.NET Core Razor Projects

✔ Fundamentals of MVC, Routing, View Rendering

✔ How to build large systems using N-Tier Architecture

✔ Integrating Identity Framework & customizing user tables

✔ Using Razor Class Library for Identity

✔ Entity Framework Core + Code First Migrations

✔ Using Sessions and TempData

✔ Creating custom Tag Helpers

✔ Creating View Components and Partial Views

✔ Implementing Authentication & Authorization

✔ Role Management

✔ Sending Email Notifications

✔ Integrating Stripe Payment Gateway

✔ Using Repository Pattern & Unit of Work

✔ Seeding Database Automatically

✔ Deploying to Microsoft Azure

🧱 Database Structure
Main Tables:

Users (Identity)

Roles

Products

Categories

Shopping Carts

All tables generated through Code-First Migrations.
