# Build stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONGIFURATION=Release
WORKDIR /src
COPY ["src/InvoiceGenerator.API/InvoiceGenerator.API.csproj", "InvoiceGenerator.API/"]
COPY ["src/InvoiceGenerator.Core/InvoiceGenerator.Core.csproj", "InvoiceGenerator.Core/"]
COPY ["src/InvoiceGenerator.Application/InvoiceGenerator.Application.csproj", "InvoiceGenerator.Application/"]
COPY ["src/InvoiceGenerator.Infrastructure/InvoiceGenerator.Infrastructure.csproj", "InvoiceGenerator.Infrastructure/"]
RUN dotnet restore "InvoiceGenerator.API/InvoiceGenerator.API.csproj"
COPY . .
WORKDIR "/src/src/InvoiceGenerator.API"
RUN dotnet build "./InvoiceGenerator.API.csproj" -c $BUILD_CONGIFURATION -o /app/build

FROM build AS publish
ARG BUILD_CONGIFURATION=Release
RUN dotnet publish "./InvoiceGenerator.API.csproj" -c $BUILD_CONGIFURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvoiceGenerator.API.dll"]