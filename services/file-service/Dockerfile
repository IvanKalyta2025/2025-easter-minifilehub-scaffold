# Используем SDK 9.0 для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .
COPY Api/*.csproj ./Api/
COPY Api.Tests/*.csproj ./Api.Tests/

RUN dotnet restore

COPY . .

# Собираем основной проект
RUN dotnet publish Api/Api.csproj --configuration Release --output /app/publish

# Используем рантайм 9.0 для запуска
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Api.dll"]