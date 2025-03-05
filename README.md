TrainingCourse - Restaurant Management System

Overview

TrainingCourse is an ASP.NET Core web application designed for restaurant management. The system supports multiple user roles, each with specific functionalities, and integrates Stripe for online payments.

Roles & Features

The application includes the following user roles:

Customer: Browse menu, place orders, and make online payments using Stripe.

Manager: Manage restaurant settings, menu items, and user access.

Kitchen: View and prepare incoming orders.

Front Desk (Front): Manage orders

Key Features

Role-based authentication & authorization using ASP.NET Core Identity

Secure online payments via Stripe integration

Order management system for both customers and staff

Reservation handling for front desk staff

Configurable settings via appsettings.json

Structured architecture using MVC and Razor Pages

Technologies Used

ASP.NET Core (.NET 8.0)

Entity Framework Core (for database management)

Stripe API (for online payments)

Razor Pages & MVC (for frontend logic)

JSON-based configuration (appsettings.json)

Installation & Setup

Prerequisites:

Install .NET SDK 8.0 or later

Install SQL Server or configure a database in appsettings.json

Set up Stripe API keys in appsettings.json

Steps to Run:

Clone the repository:

git clone https://github.com/Isebiu/TrainingCourse.git
cd TrainingCourse

Restore dependencies:

dotnet restore

Apply database migrations:

dotnet ef database update

Run the application:

dotnet run

Open your browser and go to https://localhost:5001

Configuration

Modify appsettings.json to update:

Database connections

Stripe API keys

Logging settings

Future Improvements

Adding API endpoints for mobile or third-party integrations

Enhancing reporting & analytics for managers

Implementing a delivery tracking system
