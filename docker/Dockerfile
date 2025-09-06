# 1. Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /XVerify
EXPOSE 80

# Önce csproj dosyalarýný kopyala (restore cache için)
COPY ../src/WebUI/WebUI.csproj WebUI/
COPY ../src/Application/Application.csproj Application/

# Restore yap
RUN dotnet restore WebUI/WebUI.csproj

# Tüm solution kodlarýný kopyala
COPY ../src/ ./src/

WORKDIR /XVerify
# Publish et
RUN dotnet publish src/WebUI/WebUI.csproj -c Release -o /app/publish

# 2. Runtime aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
