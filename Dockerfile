# 1. Build a�amas�
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /XVerify
EXPOSE 80

# �nce csproj dosyalar�n� kopyala (restore cache i�in)
COPY WebUI/WebUI.csproj WebUI/
COPY Application/Application.csproj Application/

# Restore yap
RUN dotnet restore WebUI/WebUI.csproj

# T�m solution kodlar�n� kopyala
COPY . .

# Publish et
WORKDIR WebUI
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime a�amas�
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebUI.dll"]
