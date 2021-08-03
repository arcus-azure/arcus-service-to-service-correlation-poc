FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.4-alpine3.10 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.202-alpine3.10 AS build
WORKDIR /src
COPY ["Arcus.API.Bacon.csproj", ""]

COPY . .
WORKDIR "/src/."
RUN dotnet build "Arcus.API.Bacon.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Arcus.API.Bacon.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Arcus.API.Bacon.dll"]