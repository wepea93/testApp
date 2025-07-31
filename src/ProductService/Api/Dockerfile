FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# Copiar solo los archivos de proyecto primero para aprovechar el cache de Docker
COPY Api/Product.Api.csproj ProductService/Api/
COPY Core/Product.Core.csproj ProductService/Core/
COPY Infrastructure/Product.Infrastructure.csproj ProductService/Infrastructure/
# Copiar el resto del código
COPY . ./ProductService/
WORKDIR /src/ProductService/Api
RUN dotnet restore "Product.Api.csproj"
RUN dotnet build "Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /src/ProductService/Api
RUN dotnet publish "Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Api.dll"]
