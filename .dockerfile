# Imagen base de .NET SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar archivos
COPY . ./
WORKDIR /app/TiendaNetApi

# Restaurar dependencias y compilar
RUN dotnet publish -c Release -o /out

# Imagen base para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

# Puerto por defecto (opcional, puede variar)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# Comando de inicio
ENTRYPOINT ["dotnet", "TiendaNetApi.dll"]