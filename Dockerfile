# ── Stage 1: Build Angular ────────────────────────────────────────────────────
FROM node:22-alpine AS angular-build
WORKDIR /app
COPY CSharpAcademy.Web/package*.json ./
RUN npm ci --legacy-peer-deps
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

# ✅ INSTALAR AQUI (no runtime)
RUN apt-get update && apt-get install -y \
    libkrb5-3 \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

# Copia o binário do .NET
COPY --from=dotnet-build /publish ./

# Copia o build do Angular
COPY --from=angular-build /app/dist/CSharpAcademy.Web/browser ./wwwroot

RUN mkdir -p /data

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "CSharpAcademy.API.dll"]