# ── Stage 1: Build & Publish ──────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar solution e projetos primeiro (melhor cache das camadas)
COPY StoresService.slnx ./
COPY StoresService/StoresService.csproj StoresService/
COPY StoresService.Tests/StoresService.Tests.csproj StoresService.Tests/

RUN dotnet restore

# Copiar todo o código e publicar
COPY . .
RUN dotnet publish StoresService/StoresService.csproj -c Release -o /app/publish

# ── Stage 2: Runtime ───────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "StoresService.dll"]
