# 📚 BookStore Ecommerce - ASP.NET Core 9

![.NET 9](https://img.shields.io/badge/.NET-9.0-purple) ![Status](https://img.shields.io/badge/Status-Completed-success) ![License](https://img.shields.io/badge/License-MIT-blue)

A complete **Book Ecommerce Application** built using **ASP.NET Core MVC (.NET 9)**. This project demonstrates enterprise-level development practices, including **N-Tier Architecture**, the **Repository Pattern**, and integration with third-party services like **Stripe** and **Identity**.

## 🚀 Overview

This application serves as a comprehensive solution for managing an online bookshop. It handles the entire flow from product management (for admins) to browsing, cart management, and secure checkout (for customers). It was built to master the fundamentals and advanced features of .NET 9.

## 🏗️ Architecture

The solution follows a strict **N-Tier Architecture** to ensure separation of concerns and maintainability:

* **📂 Web (`/Web`)**: The presentation layer. Contains Controllers, Views, ViewModels, and `wwwroot` (CSS/JS). It depends on all other layers.
* **📂 DataAccess (`/DataAccess`)**: Handles database interactions. Contains the `DbContext`, Repository implementations, and Migrations.
* **📂 Models (`/Models`)**: Contains the domain entities (POCO classes) and Data Annotations.
* **📂 Utility (`/Utility`)**: Cross-cutting concerns. Contains static constants, email services, and custom helper classes.

---

## ✨ Key Features

### 🛒 Functional Modules
* **Product Management:** Complete CRUD operations for Categories and Products with image upload support.
* **Shopping Cart:** Session-based shopping cart allowing users to add/remove items and adjust quantities.
* **Order Management:** Admin dashboard to view order status, update shipping details, and process cancellations.
* **Payment Integration:** Secure checkout flow integrated with **Stripe** for credit card processing.

### 🔐 Authentication & Security
* **Identity Framework:** Customized User Identity implementation (extending `IdentityUser` with additional fields).
* **Role-Based Authorization:** Distinct roles for **Admin**, **Customer**, and **Company** users.
* **Razor Class Library:** Integration for modifying the default Identity UI.

### ⚙️ Technical Highlights
* **Repository Pattern & Unit of Work:** Abstraction layer over Entity Framework Core to decouple logic from the database.
* **Entity Framework Core:** Code-First approach with automatic database seeding and migrations.
* **Tag Helpers & View Components:** Custom UI components for dynamic navigation and notifications.
* **TempData:** Used for toast notifications across redirects.
* **Email Notifications:** Integrated email service for account verification and order confirmation.

### 🎨 User Interface
* **Bootstrap 5:** Responsive design for mobile and desktop.
* **DataTables.js:** Rich grid features (searching, sorting, paging) for Admin tables.

---

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 9 (MVC & Razor)
* **Language:** C#
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Front-End:** HTML5, CSS3, Bootstrap 5, jQuery
* **Payment Gateway:** Stripe API
* **Cloud Deployment:** Microsoft Azure App Service / Azure SQL

---

## ☁️ Deployment

This project is configured for easy deployment to **Microsoft Azure**.
1.  Create an Azure App Service and Azure SQL Database.
2.  Update the connection string in the Azure Configuration tab.
3.  Publish directly from Visual Studio via the "Publish" profile.
