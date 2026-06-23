## Architecture

[剛才那段英文貼這裡]

## How to Run

### Prerequisites
- .NET 10 SDK
- Docker

### Start SQL Server
```bash
docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=YourPassword123! -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

### Run the app
```bash
cd src/RecipeSocial.Web
dotnet run
```

Open `http://localhost:5031/Recipe`
