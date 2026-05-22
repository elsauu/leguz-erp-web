# LEGUZ — Distribution & Sales Management System

Web system for managing the daily distribution and sales operations of a tortilla manufacturing company in Reynosa, Tamaulipas.

## Tech Stack

- ASP.NET Core 8 MVC
- Entity Framework Core 8 (SQL Server LocalDB)
- Bootstrap 5.3.3
- Chart.js 4.4
- BCrypt.Net-Next 4.0.3

## Modules

| Module | Status |
|---|---|
| Login / Auth | ✅ Done |
| Dashboard (KPIs + Chart.js) | ✅ Done |
| Routes (`/Rutas`) | ✅ Done |
| Salespersons (`/Vendedores`) | ✅ Done |
| Customers (`/Clientes`) | 🔄 Pending |
| Products (`/Productos`) | 🔄 Pending |
| Daily Route Record / Cuadranza (`/Cuadranza`) | 🔄 Pending |
| Credit Notes (`/NotasCredito`) | 🔄 Pending |
| Daily Sales (`/Ventas`) | 🔄 Pending |
| Deposits (`/Depositos`) | 🔄 Pending |
| Supermarkets (`/Autoservicios`) | 🔄 Pending |
| Motorcycles (`/Motos`) | 🔄 Pending |
| Reports (`/Reportes`) | 🔄 Pending |
| Users (`/Usuarios`) | 🔄 Pending |

## Changelog

### Update — Routes & Salespersons modules
- Fixed 404 on `/Rutas` module
- Added `/Vendedores` module: list, create, edit, and toggle active status
- Salesperson types supported: Route, Motorcycle, Store
- Route assignment dropdown auto-hides for non-route salesperson types
-
