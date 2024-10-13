# PZCheeseriaWebAPI

## Overview

PZCheeseriaWebAPI is a .NET Core Web API project that serves as the backend for a cheese shop application. The API provides endpoints for CRUD operations on cheese data, including features like creating, updating, fetching, and deleting cheese.

## Features

- CRUD operations for cheese data
- In-memory data storage
- Exception handling and error responses
- Unit testing for key functionalities
- Swagger documentation for API endpoints

## Getting Started

### Prerequisites

- .NET Core SDK 8.0
- Visual Studio 2022 or later (recommended)

### Installation

1. **Clone the repository:**
   git clone https://github.com/Nugasnakarmi/PZCheeseriaWebAPI.git
2. Install dependencies: Ensure you have the .NET Core SDK installed. Restore the dependencies by running:
   `dotnet restore`

3. Run the API:
   `dotnet run`

4. Run Tests:
   `dotnet test`

Using the API
The API provides the following endpoints:

GET /api/cheese: Retrieve all cheeses.

GET /api/cheese/{id}: Retrieve a specific cheese by ID.

POST /api/cheese: Add a new cheese.

PUT /api/cheese/{id}: Update an existing cheese by ID.

DELETE /api/cheese/{id}: Delete a specific cheese by ID.
