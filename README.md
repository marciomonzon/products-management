# Products Management - Study Application
This project was made for studying purpose. Please do not use on production environment withouth the proper modification and adjustments.

# Stack
- ASP.NET Core 8 and C#;
- EF Core and SQL Server, including Migrations;
- RabbitMQ Broker;
- Fluent Validations;
- Background Service (Workers);
- Xunit Framework for Unit Tests.

# Architecture Style
I implemented the project using Clean Architecture style. Please take a look in the follow image to understand it better.

![image](https://github.com/user-attachments/assets/c09c8aee-a826-4041-a180-ff081eb9a65b)

- Presentation Layer: It is the API and get requests from the users and sends them to the Application Layer;
- Application Layer: It is where the Use Case were implemented. This has the business rules and communicates with the Infrastructure Layer;
- Domain Layer: It has the Business Entities and Domain Events. This layer should not communicate with nobody.
- Infrastructure Layer: This layer is responsible to persists data into database and sends events to the RabbitMQ Broker.



