# Gestao Escolar

[![CircleCI](https://circleci.com/gh/erlonfs/gestao-escolar.svg?style=shield)](https://circleci.com/gh/erlonfs/gestao-escolar) 
[![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

### Concepts
* Domain driven design (DDD)
    * Domain Events
    * Anti-corruption layer
    * Aggregates
    * Repositories
    * Services
    * Value Objects
* Test-driven development (TDD)
    * Unit tests
    * Integrations tests
* SOLID Concepts
* Inversion of control principles (IoC)
* Unit of work
* Message BUS
* CI/CD continuous integration/continuous delivery

### Technologies
Project is created with:
* ASP.NET Core 3.1
* Web API
* EF Core
* AutoFac
* Dapper
* Swagger
* NLog
* MassTransit
* RabbitMQ
* SQL Server
* Moq
* XUnit
* FluentAssertions
* AutoFixture
* CircleCI

**API methods**

| Route  | Method   | 
|---|---|
|​/api​/alunos​/matricular  | POST  |
|​/api​/alunos|GET|
|​/api​/alunos​/{id}​/rematricular|PUT|
|​/api​/alunos​/{id}​/transferir|PUT|
|​/api​/escolas|POST|
|​/api​/escolas|GET|
|​/api​/escolas​/{id}​/sala|POST|
|​/api​/pessoas-fisicas|POST|
|​/api​/pessoas-fisicas|GET|
|​/api​/pessoas-fisicas​/{id}​/alterar-cpf|PUT|

**Running the tests**

![Recordit GIF](http://g.recordit.co/oHwRvRcPRf.gif)




