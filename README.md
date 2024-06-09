# Library Management System

## Overview
The Library Management System is a web application built with clean architecture principles and powered by .NET 8. It consists of separate API and MVC layers, with the API handling backend operations and the MVC layer serving as the frontend interface. The system provides comprehensive functionalities for managing library operations efficiently.

## Features

- **Book Management**: Add, edit, and delete books from the library inventory.
- **User Authentication**: Secure user login and registration system using JWT authentication.
- **Book Checkout**: Allow users to check out books from the library.
- **Book Return**: Enable users to return borrowed books to the library.
- **Search Functionality**: Search for books by title to easily find specific items in the library inventory.

## Technologies Used

- **ASP.NET Core**: Backend framework for building web applications.
- **Entity Framework Core**: Object-relational mapping (ORM) framework for .NET.
- **JWT Authentication**: JSON Web Token-based authentication for securing APIs.
- **HTML/CSS/JavaScript**: Frontend technologies for building responsive and interactive user interfaces.
- **Bootstrap**: Frontend framework for designing responsive and mobile-first websites.
- **jQuery**: JavaScript library for simplifying HTML DOM manipulation and event handling.
- **SQL Server**: Relational database management system used for storing application data.
- **FluentValidation**: Validation library for validating models.

## Setup
1. **Open CMD and Create a New Server on Your Local PC**: Follow the steps in the image below to create a new server.
![image](https://github.com/ImesashviliIrakli/Library/assets/77686006/4909b2cc-aabe-46d1-b4b8-7b7b7d567d36)
2. **Clone the Repository**: Clone the repository to your local machine.
3. **Build the Project**: Build the project using Visual Studio or the .NET CLI.
4. **Set the API as the Startup Project**: Set the API project as the startup project in Visual Studio.
5. **Set Up the Database for Identity**:
![image](https://github.com/ImesashviliIrakli/Library/assets/77686006/8bd6c222-c5d4-4c35-9b95-08035b99796a)
and write this command
```Update-Database -Context AuthDbContext```
6. **Set Up the Database for Persistence**:
![image](https://github.com/ImesashviliIrakli/Library/assets/77686006/ef8a62fd-cce2-4123-b7eb-1ed34a26d065)
```Update-Database -Context LibraryDbContext```
7. **Set MVC and API as Startup Projects**: Set both the MVC and API projects as startup projects in Visual Studio.
8. **Build Docker Compose File**: This will create the docker container where all the logs are stored using ```Seq```
   - Navigate to the library API directory using the command prompt (CMD) using `CD [directory]`.
   - Run the following command to build the Docker Compose file:
9. **View Docker Logs**: After running the `docker-compose up` command, go to the URL generated in the CMD to view the logs.
10. **Run the Application**: Once the Docker containers are running, you can access and use the application.
    

