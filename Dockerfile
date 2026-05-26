FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copiar la solución y archivos .csproj con las mayúsculas perfectas
COPY ["ItlaSocialMedia.sln", "./"]
COPY ["ItlaSocialmedia/ItlaSocialMedia.csproj", "ItlaSocialmedia/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure.Identity/Infrastructure.Identity.csproj", "Infrastructure.Identity/"]
COPY ["Infrastructure.Shared/Infrastructure.Shared.csproj", "Infrastructure.Shared/"]
COPY ["Infrastructure.Persistence/Infrastructure.Persistence.csproj", "Infrastructure.Persistence/"]

RUN dotnet restore

# 2. Copiar todo el resto del código y compilar
COPY . .
WORKDIR "/src/ItlaSocialmedia"
RUN dotnet build "ItlaSocialMedia.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItlaSocialMedia.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Usar el entorno de ejecución de .NET 9 para que no de crash
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ItlaSocialMedia.dll"]