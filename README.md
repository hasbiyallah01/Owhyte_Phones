# OwhyteePhone Backend API

A comprehensive .NET 8 Web API that powers the phone storefront experience, featuring cooperative network management, admin dashboard, and complete e-commerce functionality.

## Magic Mode Integration

This backend seamlessly supports the frontend's Magic Mode experience, transforming the regular phone store interface into an enhanced mystical theme:

- **Dynamic Branding**: API responses support magical terminology transformations
- **Admin Portal**: Enhanced theming for the administrative interface  
- **Product Display**: Supports magical product presentation modes
- **Cooperative Network**: Order assignment with enhanced user experience

## Architecture Overview

```
Owhytee/
├── Controllers/           # API endpoints and request handling
├── Core/
│   ├── Application/      # Business logic and services
│   └── Domain/          # Entity models and domain logic
├── Infrastructure/       # Data access and external services
├── Models/              # DTOs and request/response models
├── Migrations/          # Database schema evolution
└── Program.cs           # Application bootstrap and configuration
```

## Core Features

### Product Management
- **CRUD Operations**: Create, read, update, delete products
- **Brand Management**: Samsung, Tecno, Infinix, Redmi, Xiaomi support
- **Product Variants**: Color and storage options with price adjustments
- **Image Management**: Multiple images per product with Cloudinary integration
- **Bulk Upload**: Excel file import for mass product creation
- **Stock Management**: Real-time inventory tracking
- **Advanced Filtering**: Search by brand, price range, specifications

### Shopping Cart & Orders
- **Session-based Cart**: Persistent shopping cart without user accounts
- **Order Processing**: Complete order lifecycle management
- **WhatsApp Integration**: Automatic message generation for cooperatives
- **Order Status Tracking**: Pending → Assigned → Payment Received → Completed
- **Customer Information**: Name, WhatsApp, email, delivery address

### Cooperative Network
- **Partner Management**: CRUD operations for cooperative partners
- **Order Assignment**: Manual and automatic order distribution
- **Communication**: WhatsApp message generation for order notifications
- **Status Tracking**: Active/inactive cooperative management
- **Load Balancing**: Smart order distribution among active cooperatives

### Authentication & Authorization
- **JWT Authentication**: Secure token-based authentication
- **Admin Role Management**: Role-based access control
- **Password Management**: Secure password hashing with BCrypt
- **Profile Management**: Admin profile updates and password changes
- **Session Management**: Token expiration and refresh handling

### Admin Dashboard Features
- **Real-time Statistics**: Products, orders, cooperatives metrics
- **Order Management**: Status updates, cooperative assignment
- **Product Analytics**: Stock levels, popular brands, sales data
- **Cooperative Performance**: Order distribution and completion rates
- **Bulk Operations**: Mass product imports and updates

## Technology Stack

### Core Framework
- **.NET 8**: Latest LTS version with enhanced performance
- **ASP.NET Core Web API**: RESTful API framework
- **Entity Framework Core**: ORM with PostgreSQL support
- **AutoMapper**: Object-to-object mapping

### Database & Storage
- **PostgreSQL**: Primary database for all data storage
- **Cloudinary**: Image storage and optimization service
- **Entity Framework Migrations**: Database schema versioning

### Security & Authentication
- **JWT Bearer Tokens**: Stateless authentication
- **BCrypt.Net**: Password hashing and verification
- **CORS**: Cross-origin resource sharing configuration
- **Authorization Policies**: Role-based access control

### External Integrations
- **Cloudinary**: Image upload, storage, and transformation
- **WhatsApp Business**: Order notification system
- **Excel Processing**: EPPlus for bulk product imports
- **Email Services**: MailKit for notifications

### Development Tools
- **Swagger/OpenAPI**: API documentation and testing
- **Serilog**: Structured logging
- **FluentValidation**: Request validation

## Getting Started

### Prerequisites
- .NET 8 SDK
- PostgreSQL database
- Cloudinary account (for image storage)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
```bash
git clone <repository-url>
cd Owhytee
```

2. **Configure Database**
```json
// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnections": "Host=localhost;Database=Owhytee;Username=postgres;Password=yourpassword;Port=5432"
  }
}
```

3. **Configure Cloudinary**
```json
// appsettings.json
{
  "Cloudinary": {
    "CloudName": "your-cloud-name",
    "ApiKey": "your-api-key",
    "ApiSecret": "your-api-secret"
  }
}
```

4. **Run Database Migrations**
```bash
dotnet ef database update
```

5. **Start the Application**
```bash
dotnet run
```

The API will be available at `https://localhost:7180` with Swagger documentation at `/swagger`.

## API Endpoints

### Authentication
```http
POST /api/auth/login              # Admin login
GET  /api/auth/profile            # Get admin profile
POST /api/auth/change-password    # Change password
```

### Products
```http
GET    /api/products              # Get products (with filtering)
GET    /api/products/{id}         # Get product by ID
GET    /api/products/brands       # Get available brands
POST   /api/products              # Create product (Admin)
PUT    /api/products/{id}         # Update product (Admin)
DELETE /api/products/{id}         # Delete product (Admin)
POST   /api/products/bulk-upload  # Bulk upload via Excel (Admin)
PATCH  /api/products/{id}/stock   # Update stock status (Admin)

# Product Variants
GET    /api/products/{id}/variants     # Get product variants
POST   /api/products/{id}/variants     # Create variant (Admin)
PUT    /api/products/variants/{id}     # Update variant (Admin)
DELETE /api/products/variants/{id}     # Delete variant (Admin)

# Product Images
GET    /api/products/{id}/images       # Get product images
POST   /api/products/{id}/images       # Add image (Admin)
DELETE /api/products/images/{id}       # Delete image (Admin)
PATCH  /api/products/images/{id}/primary # Set primary image (Admin)
```

### Shopping Cart
```http
GET    /api/cart/{sessionId}      # Get cart contents
POST   /api/cart/add              # Add item to cart
PUT    /api/cart/update           # Update cart item
DELETE /api/cart/remove           # Remove cart item
DELETE /api/cart/clear            # Clear entire cart
```

### Orders
```http
POST /api/orders                  # Create new order
GET  /api/orders                  # Get orders (Admin)
GET  /api/orders/{id}             # Get order details
PUT  /api/orders/{id}/status      # Update order status (Admin)
PUT  /api/orders/{id}/assign      # Assign to cooperative (Admin)
POST /api/orders/{id}/whatsapp-message # Generate WhatsApp message
```

### Cooperatives
```http
GET    /api/cooperatives          # Get all cooperatives
GET    /api/cooperatives/{id}     # Get cooperative details
POST   /api/cooperatives          # Create cooperative (Admin)
PUT    /api/cooperatives/{id}     # Update cooperative (Admin)
DELETE /api/cooperatives/{id}     # Delete cooperative (Admin)
```

## Key Business Logic

### Order Processing Flow
1. **Cart Creation**: Session-based cart for anonymous users
2. **Order Placement**: Convert cart to order with customer details
3. **Cooperative Assignment**: Automatic or manual assignment to partners
4. **WhatsApp Notification**: Generate message for assigned cooperative
5. **Status Tracking**: Monitor order progress through completion

### Product Management
- **Variant Support**: Multiple colors and storage options per product
- **Image Optimization**: Cloudinary handles resizing and optimization
- **Stock Tracking**: Real-time inventory management
- **Bulk Operations**: Excel import for efficient product creation

### Cooperative Network
- **Load Balancing**: Distribute orders evenly among active cooperatives
- **Communication**: Automated WhatsApp message generation
- **Performance Tracking**: Monitor cooperative order completion rates

## Security Features

- **JWT Authentication**: Stateless, secure token-based auth
- **Password Hashing**: BCrypt with salt for secure password storage
- **Role-based Authorization**: Admin-only endpoints protection
- **CORS Configuration**: Controlled cross-origin access
- **Input Validation**: Comprehensive request validation
- **SQL Injection Prevention**: Entity Framework parameterized queries

## Performance Optimizations

- **Pagination**: Efficient data loading for large datasets
- **Lazy Loading**: On-demand relationship loading
- **Image Optimization**: Cloudinary CDN and transformations
- **Caching**: Strategic caching for frequently accessed data
- **Database Indexing**: Optimized queries for common operations

## Magic Mode Features

When the frontend activates Magic Mode, this backend supports the transformation:

- **Dynamic Responses**: API can return enhanced terminology
- **Enhanced Logging**: Special logging for magic mode interactions
- **Usage Analytics**: Track magic mode usage and engagement
- **Seamless Integration**: Order assignments and cooperative management with enhanced theming

The magic mode functionality operates seamlessly while maintaining full business functionality and API compatibility.

Built with modern .NET technologies for a comprehensive phone shopping experience.