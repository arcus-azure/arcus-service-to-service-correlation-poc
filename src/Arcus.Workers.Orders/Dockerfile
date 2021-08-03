FROM mcr.microsoft.com/dotnet/core/sdk:3.1.202-alpine3.10 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.202-alpine3.10 AS build
WORKDIR /src
COPY ["Arcus.Workers.Orders.csproj", ""]

COPY . .
WORKDIR "/src/."
RUN dotnet build "Arcus.Workers.Orders.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Arcus.Workers.Orders.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Arcus.Workers.Orders.dll"]