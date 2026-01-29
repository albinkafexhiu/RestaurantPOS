# ---------- build ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files first (better caching)
COPY RestaurantPOS.Domain/RestaurantPOS.Domain.csproj RestaurantPOS.Domain/
COPY RestaurantPOS.Repository/RestaurantPOS.Repository.csproj RestaurantPOS.Repository/
COPY RestaurantPOS.Service/RestaurantPOS.Service.csproj RestaurantPOS.Service/
COPY RestaurantPOS.Web/RestaurantPOS.Web.csproj RestaurantPOS.Web/

RUN dotnet restore RestaurantPOS.Web/RestaurantPOS.Web.csproj

# Copy everything and publish
COPY . .
RUN dotnet publish RestaurantPOS.Web/RestaurantPOS.Web.csproj -c Release -o /app/publish --no-restore

# ---------- runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Render sets PORT (we default to 8080)
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}
EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RestaurantPOS.Web.dll"]
