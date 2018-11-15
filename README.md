#### Checkout.com Code Challenge

nugetPackages used: Microsoft.AspNetCore, Moq, Xunit 

##### P1 -

> Your part of the prototype will be to develop a Web API that will be used by **customers** to manage a **basket** of **items**. The business describes the basic workflow is as follows:
 
> _"This API will allow our **users** to ***set up*** and ***manage*** an order of **items**. The API will allow users to ***add*** and ***remove*** items and ***change the quantity*** of the items they want. They should also be able to simply ***clear out all items*** from their **order** and start again."_

> _"The ***functionality*** to ***complete the purchase of the items*** will be handled separately and will be written by a different team once this prototype is complete."_

---
##### Takeaways:

 We can identify a ubitiquous languagefrom the requirement snippet above...

* _Nouns_ - A thing, we can use to model out domain (**Bold**).  
  

* _Verbs_ - An action, we can use to define commands, or actions that out system needs to perform (***Italic***).

* Additionaly, as the purchasing is done via another team/system, the basket management domain can ___only submit what it has___ to the purchasing system in order to complete the purchase. i.e a complete Purchase Order will consist of...

- [x] Customer name
- [ ] PurchaseOrder number
- [x] List products and their quantities, prices
- [ ] Billing, Shipping address

All we have in this context is the product/quantity list, and the customer name *(really just the ID - but that can be used in the Payment/Purchasing system for name lookup, lets say).*

##### Code Explained:

```cs 
Layer: Checkout.P1.BasketManagement.Domain
```
- Entities
    - Basket <sub><sup>[Aggregate Root]</sup></sub>
    > <sub><sup>A basket is the **entry to our consistency boundary (aggregate)**, the customers basket must always ensure that it (and its contained items) remain consistent at all times.</sub></sup>
    - Item 
    > <sub><sup>An **Item** encapulates the product **and quantity**. It is identified by its encapsulated **productId**. </sub></sup>

- Value Objects 
     - Quantity

- Domain Services
    - ICustomerBasketRepository 
    > <sub><sup> Will create, retrieve and update customer baskets. A customer can only have one basket - to which they can manupulate (add items, clear etc..)

#### Assumptions:
***- Product***: products have been created and defined in another context, products have a unique identifier in the system.

***- Customer***: customers have been created and defined in another customer admin context, customers have a unique identifier in the system.

```cs 
Layer: Checkout.P1.BasketManagement.Infrastructure
```
 Contains concreteimplementations that of services/repositories i.e InMemoryDb, Logging.

```cs 
Layer: Checkout.P1.BasketManagement.Application
```
Application service layer is the entry point to the accessing and using the application functionality.

Contains application commands - one for each identified **noun**
 -   ```AddItem()```
 -   ```RemoveItem() ```
 -   ```Clear() ```
 -   ```ChangeQuantity() ```

As well as ability to Retrieve a basket, and create a new basket for customer.

```cs 
Layer: Checkout.P1.BasketManagement.Application.API 
```
This is the hosted REST-ful API (WebApi) service. It's an API component that just forward the intent down to the application service which will orchestrate with the domain to perform the work.

> ##### Test project implmented for core domain ``` Checkout.P1.BasketManagement.Domain``` - per use case basis.
----

##### P2 - 

> _"Create a client library that makes use of the API endpoints created in Part 1. The purpose of this code to provide authors of client applications a simple framework to use in their applications."_

##### Code Explained:

Written as a in .NETCore Console Application utilizing HttpClient to communicate with the P1 WebApi endpoint.

Utilises the [Facade Pattern](https://www.dofactory.com/net/facade-design-pattern) to combine the 'command, then query, to allow the client to retrieve the new state after the command has completed, all within the same method call.

> ##### Integration Test project implmented for HttpClient Library  ``` Checkout.P2.BasketManagementClient.Tests`` - per use case basis.

### Steps for running Integration Tests. 

## 1) Run/'Start w/o debugging' on 
> Checkout.P1.BasketManagement.Api webapi project first.

## so that the service is hosted.


## 2) Run the Checkout.P2.BasketManagementClient.Tests
##     ..or run from tasks.json
> Task Name - "Run BasketManagementClient Integration Tests (run WebApi project first!)"

----
