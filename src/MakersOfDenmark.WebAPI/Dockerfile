#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/MakersOfDenmark.WebAPI/MakersOfDenmark.WebAPI.csproj", "src/MakersOfDenmark.WebAPI/"]
COPY ["src/MakersOfDenmark.Infrastructure/MakersOfDenmark.Infrastructure.csproj", "src/MakersOfDenmark.Infrastructure/"]
COPY ["src/MakersOfDenmark.Domain/MakersOfDenmark.Domain.csproj", "src/MakersOfDenmark.Domain/"]
COPY ["src/MakersOfDenmark.Application/MakersOfDenmark.Application.csproj", "src/MakersOfDenmark.Application/"]
RUN dotnet restore "src/MakersOfDenmark.WebAPI/MakersOfDenmark.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/MakersOfDenmark.WebAPI"
RUN dotnet build "MakersOfDenmark.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MakersOfDenmark.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MakersOfDenmark.WebAPI.dll"]