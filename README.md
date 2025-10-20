# OwhyteePhone Backend API

A .NET 8 project for a relative shop with cooperative network management, phonestore front and magic mode feature. An e commerce with whatsApp integartion.

## Features

### Core Functionality

- **Shopping Cart** - session-based cart for users
- **Order Processing** - complete order lifecycle with status tracking
- **Cooperative Network** - cooperative selection
- **WhatsApp Integration** - automatic message generation for cooperatives
- **Magic Mode Preferences** - persistent user/session preferences
- - **User Authentication** with JWT-based security not implemented yet

### Admin Tools
Not implemented yet

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
    "DefaultConnections": "*****************************"
  },
  "Jwt": {
    "Key": "*****************************",
    "Issuer": "*****************************",
    "Audience": "*****************************"
  },
  "Cloudinary": {
    "CloudName": "*****************************",
    "ApiKey": "*****************************",
    "ApiSecret": "*****************************"
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

The API is available at `https://localhost:7180`.

## Frontend Integration

The backend serves as the API layer for the **OwhyteePhone Frontend** 

Built with Next.js 14, featuring magic mode, cooperative checkout, and admin dashboard.

---

Built for real-world phone retail with a touch of magic

#  DEMO VIDEO
Watch the full demo here:  
[View on Google Drive](https://drive.google.com/file/d/1RYmz3m9vWptCPeVYpAFmfTQC0f5Xcaku/view?usp=sharing)
