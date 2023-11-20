WrsSolutions Test

The project WrsSolutions Test was developed using .NET Core 7 with architecture SOA, IoC, HEXAGONAL, DDD, TDD, Entity Framework, Dapper, MonboDB Repository, Rabbit MQ, Docker, CQRS, SOLID, Clean Code, Swagger and Dependency Injection.

| — src

| — — Adapters

| — — — Infra

| — — — — Configs

| — — — — Data

| — — — — IoC

| — — Core

| — — — Domain

| — — — Services

| — — Microservices

| — — — ApiAuth

| — — — ApiRabbitMQ

---## Usage

##Setup Database 

--LOCAL HOST SQL EXPRESS...

--DB MASTER user WrsSolutions 

Create Dabase from script into repository

Create Database 

	wrssolutions

SQL AZURE -> Production Later

	-> Create the SQL Azure and get the connection string, but to more security we can add all those connections string on Key Vaults.


Replace the connection string on file below.

	-> appsettings.Development
