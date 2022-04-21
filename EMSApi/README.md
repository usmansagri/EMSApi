# EMSApi

## Simplest Employee management system WebApi
EMSApi is a simple WebApi built using ASP.NET core WebApi(.net 6 framework)

## Features
- User authentication
- User authorization using JWT Bearer token
- Made using entity framework code first approach
- Bearer token on swagger ui configured

## Migrations
Add your local database connection string in `appsettings.json` and run migrations to create users table in database.

###### Enable Migration if not enabled
```
Enable-Migrations 
```
###### Create migration
```
Add-Migration <migration name>
```
###### Apply latest migration to database

```
Update-Database
```