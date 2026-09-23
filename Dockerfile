FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Restore first (better Docker layer caching), then copy the rest and publish.
COPY src/OrderApi/OrderApi.csproj OrderApi/
RUN dotnet restore OrderApi/OrderApi.csproj
COPY src/OrderApi/ OrderApi/
RUN dotnet publish OrderApi/OrderApi.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# ASP.NET Core 8 containers listen on port 8080 by default.
EXPOSE 8080
ENTRYPOINT ["dotnet", "OrderApi.dll"]
