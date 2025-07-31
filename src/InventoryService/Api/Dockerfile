FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copiar solo los archivos de proyecto primero para aprovechar el cache de Docker
COPY Api/Inventory.Api.csproj InventoryService/Api/
COPY Core/Inventory.Core.csproj InventoryService/Core/
COPY Infrastructure/Inventory.Infrastructure.csproj InventoryService/Infrastructure/
# Copiar el resto del código
COPY . ./InventoryService/
WORKDIR /src/InventoryService/Api
RUN dotnet restore "Inventory.Api.csproj"
RUN dotnet build "Inventory.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/InventoryService/Api
RUN dotnet publish "Inventory.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Ejecutar migraciones antes de iniciar la API
ENTRYPOINT ["/bin/sh", "-c", "dotnet ef database update --no-build --project /app/../Infrastructure/Inventory.Infrastructure.csproj --startup-project /app/Inventory.Api.dll; dotnet Inventory.Api.dll"]
