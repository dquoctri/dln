#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Uzer/Uzer.Api/Uzer.Api.csproj", "Uzer/Uzer.Api/"]
RUN dotnet restore "Uzer/Uzer.Api/Uzer.Api.csproj"
COPY . .
WORKDIR "/src/Uzer/Uzer.Api"
RUN dotnet build "Uzer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Uzer.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Uzer.Api.dll"]