#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["memory-caching-api/memory-caching-api.csproj", "memory-caching-api/"]
RUN dotnet restore "memory-caching-api/memory-caching-api.csproj"
COPY . .
WORKDIR "/src/memory-caching-api"
RUN dotnet build "memory-caching-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "memory-caching-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "memory-caching-api.dll"]