# Auth Demo

## Tech Stack
- [x] code generator
- [x] jwt token
- [x] hot reloads- dotnet watch
- [x] httpclientfactory/typedclient
- [x] polly
- 
![Alt text](./doc/jwt-demo.gif)

```dotnetcli

$ dotnet new webapi --use-controllers -o AuthApi

$ cd AuthDemo
$ dotnet build

# Trust the HTTPS development certificate by running the following command
$ dotnet dev-certs https --trust

# Run the following command to start the app on the https profile
$ dotnet run --launch-profile https

# code generator

$ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
$ dotnet add package Microsoft.EntityFrameworkCore.Design
$ dotnet add package Microsoft.EntityFrameworkCore.SqlServer
$ dotnet add package Microsoft.EntityFrameworkCore.Tools
$ dotnet tool install -g dotnet-aspnet-codegenerator
$ dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers

# code watch
$ dotnet watch


# cd ../AuthDemo
$ dotnet new xunit -o UnitTests
$ dotnet sln add .\UnitTests\UnitTests.csproj

# mockhttp
$ dotnet add .\UnitTests\ package RichardSzalay.MockHttp
$ dotnet add .\UnitTests\ package Moq

# build test object
$ dotnet add .\UnitTests\ package Bogus

# fluent assertion
$ dotnet add .\UnitTests\ package FluentAssertions
```