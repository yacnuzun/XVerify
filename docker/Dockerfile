# 1. Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /XVerify
EXPOSE 80

# Önce csproj dosyalarýný kopyala (restore cache için)
COPY WebUI/WebUI.csproj WebUI/
COPY Application/Application.csproj Application/

# Restore yap
RUN dotnet restore WebUI/WebUI.csproj

# Tüm solution kodlarýný kopyala
COPY . .

# Publish et
WORKDIR WebUI
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
