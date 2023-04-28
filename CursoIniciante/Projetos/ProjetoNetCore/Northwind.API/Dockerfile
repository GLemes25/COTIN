#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Northwind.API/Northwind.API.csproj", "Northwind.API/"]
COPY ["Common.Api/Common.Api.csproj", "Common.Api/"]
COPY ["Common.Extensions/Common.Extensions.csproj", "Common.Extensions/"]
COPY ["Common.Logging/Common.Logging.csproj", "Common.Logging/"]
COPY ["Common.UnitOfWork/Common.UnitOfWork.csproj", "Common.UnitOfWork/"]
COPY ["Northwind.Data/Northwind.Data.csproj", "Northwind.Data/"]
RUN dotnet restore "Northwind.API/Northwind.API.csproj"
COPY . .
WORKDIR "/src/Northwind.API"
RUN dotnet build "Northwind.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Northwind.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Northwind.API.dll"]