FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiar todos los archivos .csproj para restaurar dependencias
COPY ["ItlaSocialMedia.sln", "./"]
COPY ["ItlaSocialmedia/ItlaSocialmedia.csproj", "ItlaSocialmedia/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.Identity/Infrastructure.Identity.csproj", "Infrastructure.Identity/"]
COPY ["Infrastructure.Shared/Infrastructure.Shared.csproj", "Infrastructure.Shared/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]

# 2. Restaurar los paquetes de toda la solución
RUN dotnet restore

# 3. Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/ItlaSocialmedia"
RUN dotnet build "ItlaSocialmedia.csproj" -c Release -o /app/build

# 4. Publicar la app optimizada
FROM build AS publish
RUN dotnet publish "ItlaSocialmedia.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 5. Imagen final para correr la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Railway le asigna un puerto dinámico, .NET 8 lee la variable PORT automáticamente
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ItlaSocialmedia.dll"]