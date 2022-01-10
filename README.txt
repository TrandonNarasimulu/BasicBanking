Basic Banking API

1. Project Description:
This project is an internal dotnet core API that can perform some basic banking functions such as creating an account, 
transferring money and getting account balances and transfer history.

2. Prerequisites:
dotnet core v3.1

3. 3rd party libraries used:
Serilog
MediatR
FluentValidation
XUnit
Moq
Entity Framework core
Swagger UI
Microsofts Dependency Injection

4. Soltuion Architecture:
Clean Architecture Pattern

5. How to run the API:
Once you build the solution in Visual Studio, you can either run the BasicBanking.API projectdirectly 
from Visual Studio which will open a console window with the API running on url "http://localhost:9002".
Or you can navigate to the built files in the bin folder of the BasicBanking.API project and execute the 
BasicBanking.API.exe file directly. To access the Swagger page of the API you can navigate to "http://localhost:9002/api",
which will provide the full documentation on how to access and use the API.

6. More about the code:
- Clean Architecture
    This API is written using the clean architecture pattern. This is where we split our code into 3 main layers, the Presentation, 
    Application and Infrastructure layers. The Presentation layer handles all our front end related code, so we can easily switch from using an 
    API to using a desktop application and still make use of the same core functionality that we needed in our API. The Application 
    layer handles all our business logic. The Infrastructure layer handles all the core functionality needed for our business logic.
    Naming is also a very important aspect of clean architecture. The goal of clean architecture is to minimise the amount of comments you make 
    in code by naming your classes, functions, and variables appropiately so that anyone who reads it will be able to understand its purpose without
    having to read through all the code. you will notice that I have no comments in my code because I have given all classes, functions and variabels 
    names which are self explanatory. You can read more about clean architecture here "https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html".

- MediatR and FluentValidation
    This API makes use of MediatR. This library is meant for API's. It helps to keep your controllers looking clean and follows a command and query structure.
    Commands are anything that can change the state of your API, and queries are anything that only request information from your API. You will see that I have 3
    behaviour classes in the BasicBanking.Application project. These 3 classes are the  RequestLogger, PerformanceBehaviour and the ValidationBehaviour. These behaviours
    are all part of the MediatR pattern, which allow you to automatically add functionality to every command and query that you try to execute with MediatR. The 
    RequestLogger is meant to only log out every time a request is made using mediatR. The PerformanceBehaviour is used to measure the duration of any request. This is 
    good for finding requests that could be taking longer than expected. The ValidationBehaviour is used to validate the parameters of any request. This request is command
    or query in the form of a model which gets validated using FluentValidation. Each request will have its own validation criteria that it needs to pass before MediatR 
    starts to execute it. You can read up more about MediatR here "https://dotnetcoretutorials.com/2019/04/30/the-mediator-pattern-part-3-mediatr-library/".
    You can find more information about FluentValidation here "https://fluentvalidation.net/".

- Microsofts Dependency Infrastructure
    This API makes use of Microsofts Dependency Injection. You will see that the Application and Infrastructure projects both have a class called DependencyInjection.
    This file handles all everything that needs to be in the dependency container from those two projects. The API project references both those files in its startup class.
    You can find more information about Microsofts Dependency Injection here "https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1".

- Entity Framework Core
    This API makes use of an in memory database. It does this with the help of the Entity Framework Core library. All database related tasks are performed usig the 
    Entity Framework Core library. All database related tasks are in the Infrastructure layer, so if we decide to stop using a database we can change that one layer without
    interferring with anything in Application layer. The business logic layer does not know how we store the data, so we can change that easily at will. That is one of the
    advantages of using clean architecture. You can find more information about Entity Framework Core in memory database here 
    "https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core"

- Serilog
    The entire API makes use of Serilog for its logging. You can the log file in the following location, "C:\Logs". This API makes use of the Microsofts ILogger interface which
    abstracts which ever logging we wish to use. We are able to change to use any other logging library without changing any code in all the files. You can find more information
    about Serilog here "https://serilog.net/". You can find more information about Microsofts ILogger here "https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1".

- Swagger UI
    This API has made use of Swagger UI to document the usage of the API. Read the "How to run the API" section for information on accessing the swagger page. For more infomation on how to use 
    swagger in dotenet core you can visit this link "https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-3.1&tabs=visual-studio".

- XUnit and Moq
    This API makes use of XUnit and Moq for all its unit tests. There are Application and Integration unit tests. The Application tests are only to test the flow of the code and 
    to ensure that you can reach all code paths with the corresponding conditions. The Integration tests are meant to test the actual responses from the API controller when you call
    an endpoint. XUnit is used to label any test methods as well as to Assert any values. Moq is used to mock the Infrastructure services, so that you can specify what kind of result
    it should return. You can also verify how many times a method should be called using Moq. For more infomation on XUnit you can go here "https://xunit.net/". For more information 
    on Moq you can go here "https://github.com/moq/moq4". All the unit tests are arranged using the Arrange Act Assert pattern. You can read up more about this pattern
    here "https://docs.telerik.com/devtools/justmock/basic-usage/arrange-act-assert". All unit tests can be run directly from Visual Studio. If any integration tests fail, you can try 
    rerun them on their own.