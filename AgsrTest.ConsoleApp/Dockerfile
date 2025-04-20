FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:6.0
RUN apt-get update && apt-get install -y curl
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "AgsrTest.ConsoleApp.dll"]