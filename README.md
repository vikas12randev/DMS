# Clean Architecture Based Delivery Management System

This solution is a template for creating a ASP.NET Core Web API following the principles of Clean Architecture and this allows you to run all CRUD operations (CREATE, READ, UPDATE, DELETE). The API uses Clean architecture with CQRS alongise the repository and unit of work patterns, it manages the centralized logging and for validation it uses Fluent Validations.

# Overview

The API allows users or partners to create a delivery and each delivery can have 5 different states (Created, approved, completed, cancelled or expired).

Newly created delivery is set to Created state and access window is asssigned to it for an hour. Once the delivery is created then user can either approve or cancelled the delivery by using the update endpoint. API caters for user and business/partner roles and Update endpoint needs to be sent the role as only user and partner can cancel the delivery which is in a pending state.


### Domain

This contains all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. This layers contains all the handlers for mediatr, and you can extend the WebApi by adding additional command or query handlers here. This layer also contains the validators for each command or query handlers.

### Infrastructure

This layer contains classes for accessing external resources. This layer contains the repository and unit of work. Currently it uses FakeDataSource which gets consumed by the repositoy and this repository gets furhter used in the command/query handlers. The layer also includes a background task which runs in the background and wakes up every 60 minutes to check the existing deliveries where AccessWindows has gone past an hour and then the job sets it to expired state.

### WebApi

This layer is a web api application based on ASP.NET 6.0.x. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. It exposes endpoints to create, update or delete delivery.


# To do
- Complete integration with EF Core
- Authentication
- Eventing

## Technologies
* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq)
* [Docker](https://www.docker.com/)

## Getting Started

**The swagger page can be accessed from below link:**

https://localhost:44325/swagger/index.html

Endpoints:

## Get Delivery by passing the oderId

###### GET- https://localhost:44325/deliveries/1

```

{
  "state": "Created",
  "accessWindow": {
    "startTime": "2023-06-25T13:31:25.562221+01:00",
    "endTime": "2023-06-25T14:31:25.5622245+01:00"
  },
  "recipient": {
    "name": "Receiver1",
    "address": "London",
    "email": "recepient1@gmail.com",
    "phoneNumber": "11111xxxxxx"
  },
  "order": {
    "orderId": 1,
    "orderNumber": 1,
    "sender": "User1"
  }
}
```
#### Create a delivery:

###### POST - https://localhost:44325/deliveries/Create

Request body:

```
{
  "deliveryDto": {
    "recipient": {
      "name": "vik",
      "address": "North London, W9 3JS",
      "email": "vik@gmail.com",
      "phoneNumber": "074045982xxx"
    },
    "order": {
      "orderId": 10,
      "orderNumber": 10,
      "sender": "Argos"
    }
  }
}
```

Response:
```
{
  "state": "Created",
  "accessWindow": {
    "startTime": "2023-06-25T13:40:04.5388089+01:00",
    "endTime": "2023-06-25T15:40:04.5388193+01:00"
  },
  "recipient": {
    "name": "vik",
    "address": "North London, W9 3JS",
    "email": "vik@gmail.com",
    "phoneNumber": "074045982xxx"
  },
  "order": {
    "orderId": 0,
    "orderNumber": 10,
    "sender": "Argos"
  }
}
```
### Update the delivery state from created to approved by customer

######  PUT https://localhost:44325/deliveries/Update

### Requst body:

```
{
  "updateDeliveryDto": {
    "orderId": 10,
    "state": 1,
    "roles": 0
  }
}
```

### Response body:

```
{
  "state": "Approved",
  "accessWindow": {
    "startTime": "2023-06-25T13:40:04.5388271+01:00",
    "endTime": "2023-06-25T15:40:04.5388274+01:00"
  },
  "recipient": {
    "name": "vik",
    "address": "North London, W9 3JS",
    "email": "vik@gmail.com",
    "phoneNumber": "074045982xxx"
  },
  "order": {
    "orderId": 10,
    "orderNumber": 10,
    "sender": "Argos"
  }
}
```

### Delete the delivery by OrderId

###### DELETE - https://localhost:44325/deliveries/1

