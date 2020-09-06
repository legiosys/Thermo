#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8000:80
EXPOSE 8001:443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Thermo Server WebApi/Thermo Server WebApi.csproj", "Thermo Server WebApi/"]
RUN dotnet restore "Thermo Server WebApi/Thermo Server WebApi.csproj"
COPY . .
WORKDIR "/src/Thermo Server WebApi"
RUN dotnet build "Thermo Server WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Thermo Server WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Thermo Server WebApi.dll"]