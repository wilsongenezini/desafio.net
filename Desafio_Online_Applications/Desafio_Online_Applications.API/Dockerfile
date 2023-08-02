#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Desafio_Online_Applications.API/Desafio_Online_Applications.API.csproj", "Desafio_Online_Applications.API/"]
RUN dotnet restore "Desafio_Online_Applications.API/Desafio_Online_Applications.API.csproj"
COPY . .
WORKDIR "/src/Desafio_Online_Applications.API"
RUN dotnet build "Desafio_Online_Applications.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desafio_Online_Applications.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio_Online_Applications.API.dll"]