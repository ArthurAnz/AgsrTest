# Agsr Test Application
Asp.Net Core Web Api, .Net 6, SQL Server
## How to start
From the root folder, execute:
```bash
docker compose up --build
```
This will run:
- Start SQL Server
- Web API (ASP.NET Core)
- Script that populates the database with random 100 patient records

## Postman
The root folder contains a Postman collection file `AgsrPostmanCollection.postman_collection.json`.
Use it to test the API endpoints.

## Swagger UI
After the app starts, the Swagger UI is available at:
```bash
http://localhost:8080
```

**Important:**
Endpoints that require an id must use actual IDs from the seeded data.
These are generated when the app starts, so static test values won't work.

## Cleanup
To free disk space, don't forget to remove the Docker volume created during startup
