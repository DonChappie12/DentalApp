# Dental Web API

## Running the Dental Web API (VS Code)
To run the application run this command
`dotnet run`

An alternative that will keep watching after saving a file run this command
`dotnet watch run`
NOTE: This will stop the application and re-run with the new changes automatically

## Entity Framework Core tools reference - .NET Core CLI
Before any DB (DataBase) migration run this command verify if dotnet ef is installed
`dotnet ef`

If not installed run this command
`dotnet tool install --global dotnet-ef`

If you have EF Core Tools installed run this command to update to latest version
`dotnet tool update --global dotnet-ef`

For more information on EF Core Tools check this [documentation](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)