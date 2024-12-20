ThamcoProducts Web API

ThamcoProducts is a RESTful Web API designed to manage product data. It integrates with external services to retrieve product information and exposes endpoints to perform CRUD operations on the products. The API is built using ASP.NET Core, supports secure communication, and is designed to be resilient with Polly for handling transient failures.

Features

Product Management: Provides endpoints for retrieving products.
External Product Integration: Integrates with external services to fetch product data.
Error Handling: Uses Polly for retry and circuit-breaker policies to handle transient errors in HTTP requests.
Secure: Supports authentication and authorization using JWT Bearer tokens.

Technologies Used
ASP.NET Core Web API: Framework for building the Web API.
Polly: Resilience library for handling transient faults, with retry and circuit-breaker policies.
JWT Authentication: Secure API access with token-based authentication.
Swagger: API documentation and testing via Swagger UI.

Setup and Installation
Prerequisites
.NET 6 or higher
Visual Studio or Visual Studio Code
Postman or any other API client to test the endpoints.

Clone the Repository
-git clone https://github.com/p1118231/ThamcoProductService.git
-cd ThamcoProducts

Install Dependencies
Run the following command to restore the required dependencies:
-dotnet restore

Run the Application
Build and run the project:
-dotnet build
-dotnet run

Accessing the API
Once the API is running, it will be accessible at https://thamco-products-enhuhhfgajgzcgee.uksouth-01.azurewebsites.net/api/product/Undercutters by default.
You can test the API using any API client like Postman or Swagger.

API Endpoints
GET /api/products
Description: Retrieves all products from the database.

Features
Product Management

External Product Integration: Supports external services to provide product data and updates.
Resilient HTTP Calls with Polly
The API uses Polly for retry policies, circuit breakers, and fallback mechanisms to handle failures during HTTP requests (such as fetching product data from external services).
JWT Authentication
The API uses JWT Bearer tokens for authentication and authorization.

Deployment
To deploy the API to production, configure the connection strings and JWT settings for your production environment. You can deploy it to any platform supporting .NET Core, such as Azure, AWS, or your preferred hosting service.

Azure Deployment
Publish the application:
dotnet publish -c Release
Deploy the published application to Azure App Services or any other hosting provider.

Troubleshooting
Authentication Issues
Make sure that the JWT configuration is correct, and that the Issuer, Audience, and Key match those in your authentication provider.
API Errors
Check the logs for detailed error messages. You can also use Swagger UI to test the API endpoints and confirm their behavior.
