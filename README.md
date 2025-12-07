📘 Project Overview

This project is a fully functional E-Commerce Platform built for both learning and real-world implementation.
It demonstrates how to build scalable enterprise-level applications using modern ASP.NET technologies.

🧰 Technologies Demonstrated

✔ ASP.NET Core MVC (.NET 9)
✔ N-Tier Architecture
✔ Identity Framework for Authentication & Authorization
✔ Stripe Payment Gateway
✔ Entity Framework Core (Code-First Migrations)
✔ Repository & Unit of Work Pattern
✔ Azure Cloud Deployment

The system includes product management, shopping cart, checkout, order processing, role-based access, and email notifications.

🚀 Features
🔐 Authentication & Authorization

ASP.NET Core Identity (RCL-based)

Extended user profile fields (Name, City, Address, etc.)

Role management: Admin, Customer, Employee, Company

Login, Register, Forgot Password, Lockout features

🛍 E-Commerce Core Functionalities

Product Catalog

Categories

Shopping Cart

Full Checkout Process

Order Summary & Order Tracking

Stripe Payment Integration

Email Notifications

🖥 Frontend Features

Responsive UI using Bootstrap v5

Clean Razor Views with Layouts & Partials

Custom Tag Helpers

View Components

⚙ Application Logic

N-Tier Architecture
(UI → Controllers → Models → Data Access)

Repository & Unit of Work Pattern

EF Core (Code-First Migrations)

Automatic Database Seeding

Sessions & TempData

☁ Deployment

Optimized and configured for Microsoft Azure App Service

🏛 Architecture

This project follows a clean, modular N-Tier Architecture:

📂 ECommerceApp
 ├── 📁 ECommerceApp.Web           → Presentation Layer (MVC, Razor)
 ├── 📁 ECommerceApp.Models        → Business Logic, Entities, ViewModels
 ├── 📁 ECommerceApp.DataAccess    → EF Core, Repositories, Migrations

✔ Why N-Tier?

Separation of concerns

Maintainability

Reusability

Testability

Cleaner, scalable codebase

🛠 Technologies Used
Technology	Purpose
ASP.NET Core (.NET 9)	Application framework
ASP.NET Core MVC	Presentation layer
Razor Pages (Identity RCL)	Authentication UI
Entity Framework Core	ORM & Code-First Migrations
SQL Server	Database
Stripe API	Payment processing
Bootstrap v5	Frontend styling
Azure Cloud	Deployment
Repository + Unit of Work	Data access pattern
🎯 Learning Objectives

This project helps you master:

✔ Structure of ASP.NET Core MVC (.NET 9) applications

✔ Structure of ASP.NET Core Razor Identity Projects

✔ MVC Fundamentals (Controllers, Routing, Views)

✔ Building enterprise apps using N-Tier Architecture

✔ Customizing ASP.NET Core Identity

✔ Using Razor Class Library for Identity

✔ EF Core Code-First Migrations

✔ Sessions & TempData usage

✔ Custom Tag Helpers

✔ View Components & Partials

✔ Authentication & Authorization

✔ Role Management

✔ Sending Email Notifications

✔ Integrating Stripe Payment Gateway

✔ Using Repository Pattern & Unit of Work

✔ Automatic DB Seeding

✔ Deploying to Azure

🧱 Database Structure
Main Tables

Users (Identity)

Roles

Products

Categories

Shopping Carts

All tables are generated using EF Core Code-First Migrations.
