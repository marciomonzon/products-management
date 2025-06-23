# Products Management - Study Application
This project was made for studying purpose. Please do not use on production environment withouth the proper modification and adjustments.

**Note: This is a Work in Progress**

# Stack
- ASP.NET Core 8 and C#;
- EF Core and SQL Server, including Migrations;
- RabbitMQ Broker;
- Fluent Validations;
- Background Service (Workers);
- Xunit Framework for Unit Tests;
- Quartz.NET to Schedule Database Update.

# Architecture Style
I've implemented the project using Clean Architecture style. Please take a look in the follow image to understand it better.

- Presentation Layer: It is the API and get requests from the users and sends them to the Application Layer;
- Application Layer: It is where the Use Case were implemented. This has the business rules and communicates with the Infrastructure Layer;
- Domain Layer: It has the Business Entities and Domain Events. This layer should not communicate with nobody.
- Infrastructure Layer: This layer is responsible to persists data into database and sends events to the RabbitMQ Broker.

The Application has a Service which publish a message into the Rabbit MQ and the Notification Email Worker sends an e-mail.
In summary, I applied the Publish/Subscribe architecture style as well.

<br/>
  
<img alt="Clean Architecture applied" title="Clean Architecture applied" src="https://github.com/user-attachments/assets/825a1f35-a317-48a3-8cd9-39bb5a0b5097" />

<br/>

# How does the basic flow work?
1) The User create/update a new Product;
2) After the Product is created/updated and save the changes into the DB, a new event is sent to the Message Broker (Rabbit MQ);
3) The Notification Email Worker gets the Event of the Product created/updated and sends a new e-mail (sending simulation);

![image](https://github.com/user-attachments/assets/46143448-2eda-4bbc-84d0-13e66f78e7a8)

# How does the Update Status Worker work?
1) I've installed Quartz.NET to schedule the update, daily;
2) This schedule can be done changing the parameters in appsettings.json, hour and time;
3) After the Time and Hour set is reached, the Worker will Update the Status of the Product based on a specific Business Rule.

![image](https://github.com/user-attachments/assets/6c801db1-0a6a-4f01-8cfd-c4e47fb58ba9)


