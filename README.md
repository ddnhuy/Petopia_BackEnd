# Petopia Backend

Petopia is a social network and pet management project, combining a sharing platform for pet lovers and an e-commerce platform specialized in pet products and services. This project provides an API for core features like user registration, pet post management, user interfaces, and e-commerce functionalities.

## Objective
The Petopia Backend project provides an API for key features of the app, including user registration, pet post management, user interfaces, and pet product/service functionalities.

## Product Links
- **App**: [Access the Petopia App](https://personal-rcltnk5a.outsystemscloud.com/PreviewInDevices/ShareMobileApp.aspx?URL=/PETOPIA/)
- **API**: [Access the Petopia API](https://api.ddnhuy.tech/petopia/swagger/index.html)

## API Features
- **User Management**: Register, login, and manage user information.
- **Posts**: Manage pet posts, including create, update, and delete operations.
- **Products and Services**: Manage pet products and services.
- **Search**: Search for posts, users, and pet products/services.

## Installation and Setup

### Install dependencies:
- Install required software: Node.js, Express, MongoDB (or another database).
- Configure necessary environment variables (API keys, URLs).

### Start the server:
- Clone the repository to your local machine.
- Run `npm install` to install dependencies.
- Run `npm start` to start the server.

### Test the API:
- Access the Swagger UI to test the Petopia API endpoints: [Swagger UI](http://localhost:8080/swagger)

## Technologies Used
- **Backend Framework**: .NET 8.0, ASP.NET Core
- **Database**: PostgreSQL 
- **Authentication**: JWT (JSON Web Tokens)
- **API Documentation**: Swagger
- **Containerization**: Docker
    
## Contributing
We welcome contributions from the community! If you want to contribute to this project, feel free to create a Pull Request or open an Issue if you find a bug or have suggestions for improvements.

## License
This project is licensed under the MIT License.

# Clean Architecture Template

What's included in the template?

- SharedKernel project with common Domain-Driven Design abstractions.
- Domain layer with sample entities.
- Application layer with abstractions for:
  - CQRS
  - Example use cases
  - Cross-cutting concerns (logging, validation)
- Infrastructure layer with:
  - Authentication
  - Authorization using Identity
  - EF Core, PostgreSQL
  - Serilog
- Seq for searching and analyzing structured logs:
  - Seq is available at [http://localhost:8081](http://localhost:8081) by default
- Testing projects:
  - Architecture testing

Stay awesome!
