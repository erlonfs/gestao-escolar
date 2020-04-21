# Gestao Escolar

[![CircleCI](https://circleci.com/gh/erlonfs/gestao-escolar.svg?style=shield)](https://circleci.com/gh/erlonfs/demo-gestao-escolar) [![License](http://img.shields.io/:license-mit-blue.svg?style=flat-square)](http://badges.mit-license.org)

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
* Unit of work (WOW)
* Message BUS

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

**API methods**
POST ​/api​/alunos​/matricular
GET ​/api​/alunos
PUT ​/api​/alunos​/{id}​/rematricular
PUT ​/api​/alunos​/{id}​/transferir\
POST ​/api​/escolas
GET ​/api​/escolas
POST ​/api​/escolas​/{id}​/sala
POST ​/api​/pessoas-fisicas
GET ​/api​/pessoas-fisicas
PUT ​/api​/pessoas-fisicas​/{id}​/alterar-cpf

**Running the tests**

![Recordit GIF](http://g.recordit.co/oHwRvRcPRf.gif)




