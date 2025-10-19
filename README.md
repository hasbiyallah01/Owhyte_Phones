# OwhyteePhone Backend API

A .NET 8 Web API powering a phone storefront with cooperative network management, admin dashboard, and magic mode features. Built for real-world e-commerce with WhatsApp integration and bulk operations.

## Magic Mode Integration

The backend supports the frontend's Magic Mode - a fun feature that transforms the regular phone store into a mystical shopping experience. When users toggle magic mode, button text changes ("Browse Products" → "Discover Enchanted Devices"), styling gets magical gradients, and the whole interface becomes more engaging.

## Features

### Core Functionality

- **Product Management** - create, update, delete products with variants and images
- **Shopping Cart** - session-based cart for anonymous users
- **Order Processing** - complete order lifecycle with status tracking
- **Cooperative Network** - partner management and order assignment
- **WhatsApp Integration** - automatic message generation for cooperatives
- **Bulk Operations** - Excel import for mass product creation
- **Magic Mode Preferences** - persistent user/session preferences
- - **User Authentication** with JWT-based security not implemented yet

### Admin Tools
- Real-time dashboard with statistics
- Order management and cooperative assignment
- Product analytics and stock management
- Bulk product imports via Excel
- User preference management

## Tech Stack

**Language**: C#  
**Framework**: .NET 8 Web API  
**Database**: PostgreSQL  
**Image Storage**: Cloudinary  
**Authentication**: JWT-based auth  
**Real-time**: SignalR (planned)  
**Hosting**: Vercel (frontend) 

## Getting Started

### Prerequisites
- .NET 8 SDK
- PostgreSQL access
- Cloudinary account

### Environment Variables (appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnections": "Host=localhost;Database=OwhyteePhone;Username=postgres;Password=yourpassword;Port=5432"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "https://yourdomain.com",
    "Audience": "https://yourdomain.com"
  },
  "Cloudinary": {
    "CloudName": "your-cloud-name",
    "ApiKey": "your-api-key",
    "ApiSecret": "your-api-secret"
  }
}
```

### Installation

1. **Clone the repository**
```bash
git clone https://github.com/hasbiyallah01/whyte.git
cd Owhytee
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Run database migrations**
```bash
dotnet ef database update
```

4. **Start the application**
```bash
dotnet run
```

The API will be available at `https://localhost:7180` with Swagger docs at `/swagger`.


## Key Features

### Cooperative Network
This is the unique selling point - instead of direct fulfillment, orders get assigned to partner cooperatives who handle the actual sales. The system:
- Automatically assigns orders to available cooperatives
- Generates WhatsApp messages for order notifications
- Tracks cooperative performance and order completion

### Magic Mode
A UX feature where users can toggle between normal and "magical" interface:
- Button text transforms ("Add to Cart" → "Cast Shopping Spell")
- Magical gradients and animations appear
- Preferences are saved to database session storage
- Works seamlessly without breaking functionality

## Frontend Integration

The backend serves as the API layer for the **OwhyteePhone Frontend**

Built with Next.js 14, featuring magic mode, cooperative checkout, and admin dashboard.

---

Built for real-world phone retail with a touch of magic