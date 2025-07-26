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

## System Requiremens
- .NET Framework 4.5+
- Microsoft SQL Server 2014 (local instance required)

## Installation
1. **SQL Server 2014 Setup**
   - Install Microsoft SQL Server 2014 (Express or full version)
   - Create database named "Project"
   - Ensure SQL Server instance "GSTR2_SERVER" is running
   
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

**Required Database Tables:**
- `utilisateur` - User authentication (nomutilisateur, password)
- `client` - Customer information (id, nom, prenom, adresse, telephone, gmail)
- `Commandes` - Order records
- `DetailCommandes` - Order line items
- `Products` - Product inventory

## Main Forms & Features
- **login.cs** - User authentication with SQL Server
- **acceuil.cs** - Main dashboard and navigation
- **produits.cs** - Product listing and management
- **createProduit.cs** - Add new products to inventory
- **clients.cs** - Client database and management  
- **createClient.cs** - Register new customers
- **commandeForm.cs** - Order management interface
- **createCommande.cs** - Create new customer orders
- **detail_commandes.cs** - View order details and line items
- **createDetailCommande.cs** - Add items to orders
- **crDetailCommande.cs** - CRUD operations for order details
- **StatistiquesForm.cs** - Reports, analytics, and statistics

## Project Structure
```
StockAPP/
├── Properties/                    # Assembly information
├── References/                    # Project dependencies
├── models/                        # Data Models
│   ├── Client.cs                 # Client entity
│   ├── Commandes.cs              # Order entity
│   ├── DetailCommandes.cs        # Order details entity
│   └── ProductsMODEL.cs          # Product entity
├── Repo/                         # Repository Layer (Data Access)
│   ├── Clientrepo.cs            # Client data operations
│   ├── Commanderepo.cs          # Order data operations
│   ├── DetailCommandeRepo.cs    # Order details operations
│   └── produitsrepo.cs          # Product data operations
├── Forms/                        # User Interface Layer
│   ├── acceuil.cs               # Main dashboard
│   ├── clients.cs               # Client management
│   ├── commandeForm.cs          # Order management
│   ├── crDetailCommande.cs      # Order details CRUD
│   ├── createClient.cs          # Add new client
│   ├── createCommande.cs        # Create new order
│   ├── createDetailCommande.cs  # Add order details
│   ├── createProduit.cs         # Add new product
│   ├── detail_commandes.cs      # Order details view
│   ├── login.cs                 # Authentication
│   ├── produits.cs              # Product management
│   └── StatistiquesForm.cs      # Reports and statistics
├── Program.cs                    # Application entry point
├── App.config                    # Configuration settings
└── packages.config              # NuGet packages
```

## Technical Details
- **Database**: Microsoft SQL Server 2014 with direct SqlConnection
- **Architecture**: 3-Layer Architecture (Models, Repository, UI)  
- **Data Models**: Simple POCO classes with public fields
- **Repository Pattern**: Dedicated repo classes with full CRUD operations
- **UI Framework**: Windows Forms with DataGridView controls
- **Connection Management**: Using statements for proper resource disposal
- **Security**: Parameterized queries to prevent SQL injection
- **Error Handling**: Try-catch blocks with console logging
- **Search Functionality**: Dynamic SQL query building with LIKE operators
## Key Features in Detail

### Dashboard Features (acceuil.cs)
- **Statistics Display**: Real-time metrics on load
  - Total products count with 📦 icon
  - Total orders count with 📟 icon  
  - Total revenue in DH (Moroccan Dirham) with 💰 icon
- **Product Alerts**: DataGridView showing products near expiration
- **Quick Actions**: Direct access buttons for:
  - Add new product (createProduit form)
  - Add new order (commandeForm)
  - Add new client (createClient form)
- **Navigation Menu**: Buttons to access all main modules
- **Auto-refresh**: Statistics and alerts load automatically on form load
### Client Management Module
- **Full CRUD Operations**: Create, Read, Update, Delete clients
- **Data Grid View**: Sortable table with client information
- **Form Validation**: Parameterized queries prevent SQL injection
- **Navigation**: Seamless switching between modules

### Client Data Model
```csharp
public class Client {
    public int id;
    public string nom;      // Last name
    public string prenom;   // First name  
    public string adresse;  // Address
    public string telephone; // Phone number
    public string gmail;    // Email address
}
```

### Repository Pattern Implementation
- **Clientrepo.cs**: Handles all client database operations
- **Produitrepo.cs**: Product operations including expiration alerts and statistics
- **CommanderRepo.cs**: Order management with revenue calculations
- **Error Handling**: Try-catch blocks with console logging
- **Parameterized Queries**: Protection against SQL injection
- **Advanced Search**: Dynamic query building with LIKE operators
2. Login with your credentials
3. Use the main dashboard to navigate to different modules
4. Start by adding products and clients
5. Create orders and manage inventory
6. View reports in the Statistics section
