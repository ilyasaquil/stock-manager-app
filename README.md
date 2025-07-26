# Stock Manager App (.NET) - Documentation

## Overview
A Windows Forms desktop application for inventory management built with .NET Framework. Manages products, clients, orders, and provides reporting capabilities.

## Features
- **Authentication**: Secure login system
- **Products**: Add, edit, and manage inventory items
- **Clients**: Customer database and management
- **Orders**: Create and track customer orders
- **Order Details**: Manage individual order line items
- **Statistics**: Reports and analytics dashboard

## System Requirements
- .NET Framework 4.5+
- Microsoft SQL Server 2014 (local instance required)

## Installation
1. **SQL Server 2014 Setup**
   - Install Microsoft SQL Server 2014 (Express or full version)
   - Create database named "Project"
   
2. **Application Setup**
   - Download and extract the application files
   - Ensure .NET Framework is installed
   - Update connection string in code if needed
   - Run `StockAPP.exe`

## Database Configuration
The application uses Microsoft SQL Server 2014 with the following connection:
```
Server: AQUIL\GSTR2_SERVER
Database: Project
Authentication: Windows Integrated Security
SQL Server Version: 2014
```

**Required Tables:**
- `utilisateur` - User authentication (nomutilisateur, password)
- Additional tables for products, clients, orders, etc.

## Main Forms
- **login.cs** - User authentication
- **acceuil.cs** - Main dashboard
- **produits.cs** - Product management
- **clients.cs** - Client management  
- **commandeForm.cs** - Order management
- **detail_commandes.cs** - Order details
- **StatistiquesForm.cs** - Reports and statistics

## Project Structure
```
StockAPP/
├── models/           # Data models
├── Repo/            # Data access layer
├── Properties/      # App settings
├── Forms (.cs)      # User interface
├── Program.cs       # Entry point
└── App.config       # Configuration
```

## Technical Details
- **Database**: Microsoft SQL Server 2014 with direct SqlConnection
- **Authentication**: Parameterized queries for security
- **Data Access**: ADO.NET with SqlCommand and SqlDataReader
- **Connection String**: Integrated Security with SSL encryption
- **Architecture**: Windows Forms with direct database access
