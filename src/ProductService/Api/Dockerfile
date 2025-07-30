FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ProductService/Api/Product.Api.csproj", "Product.Api/"]
COPY ["ProductService/Core/Product.Core.csproj", "Product.Core/"]
COPY ["ProductService/Infrastructure/Product.Infrastructure.csproj", "Product.Infrastructure/"]
RUN dotnet restore "ProductsApi/ProductsApi.csproj"
COPY . .
WORKDIR "/src/Product.Api"
RUN dotnet build "Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Product.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Product.Api.dll"]
