# Используем SDK 10 (потому что в проекте net10.0)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY *.sln .
COPY Api/*.csproj ./Api/
COPY Api.Tests/*.csproj ./Api.Tests/

RUN dotnet restore

COPY . .

# Собираем основной проект
RUN dotnet publish Api/Api.csproj --configuration Release --output /app/publish

# Используем ASP.NET 10 для запуска
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]