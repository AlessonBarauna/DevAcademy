# ── Stage 1: Build Angular ────────────────────────────────────────────────────
FROM node:22-alpine AS angular-build
WORKDIR /app
COPY CSharpAcademy.Web/package*.json ./
RUN npm ci --prefer-offline
COPY CSharpAcademy.Web/ ./
RUN npm run build

# ── Stage 2: Build .NET ───────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS dotnet-build
WORKDIR /app
COPY CSharpAcademy.API/*.csproj ./
RUN dotnet restore
COPY CSharpAcademy.API/ ./
RUN dotnet publish -c Release -o /publish

# ── Stage 3: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Copia o binário do .NET
COPY --from=dotnet-build /publish ./

# Copia o build do Angular para wwwroot (servido como arquivos estáticos)
COPY --from=angular-build /app/dist/CSharpAcademy.Web/browser ./wwwroot

# Cria o diretório persistente para o SQLite
RUN mkdir -p /data

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "CSharpAcademy.API.dll"]
