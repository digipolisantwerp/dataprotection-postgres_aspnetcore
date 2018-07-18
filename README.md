# DataProtection.Postgres

Description

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [Versions](#versions)
- [Installation](#installation)
- [Description](#description)
- [Configuration in Startup.ConfigureServices](#configuration-in-startupconfigureservices)
- [Development](#development)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Versions

The latest version of this package targets **.NET Standard 2.0**.

Use version 2.x if you need a version targetting **.NET Standard 1.6**. The source code of this version is available in [branch 2.x](https://github.com/digipolisantwerp/dataprotection-postgres_aspnetcore/tree/2.x) for maintenance reasons.

## Installation

To add the package to a project, you add the package to the csproj project file:

```xml
  <ItemGroup>
    <PackageReference Include="Digipolis.DataProtection.Postgres" Version="3.0.0" />
  </ItemGroup>
```

or if your project still works with project.json :

``` json
"dependencies": {
    "Digipolis.DataProtection.Postgres":  "3.0.0"
 }
```

ALWAYS check the latest version [here](https://github.com/digipolisantwerp/dataprotection-postgres_aspnetcore/blob/master/src/Digipolis.DataProtection.Postgres/Digipolis.DataProtection.Postgres.csproj) before adding the above line !

Make sure you have our Nuget feed configured (https://www.myget.org/F/digipolisantwerp/api/v3/index.json).

In Visual Studio you can also use the NuGet Package Manager to do this.

## Description

This package offers an extension method on the **IDataProtectionBuilder** that allows configuring a Postgres store to persist the keys.

## Configuration in Startup.ConfigureServices

In the **ConfigureServices** method of your **Startup** add this line:

``` csharp
    services.AddDataProtection().PersistKeysToPostgres(connectionString, appId, instanceId);
```

You need to pass a connection string to the database, an application id and an instance id.

Method signature:

``` csharp
    PersistKeysToPostgres(string connectionString, Guid appId, Guid instanceId)
```

## Development

An integration test is available.  Run `./run_integration_tests.sh` to run the integration tests.
