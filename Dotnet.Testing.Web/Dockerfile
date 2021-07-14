#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Dotnet.Testing.Web/Dotnet.Testing.Web.csproj", "Dotnet.Testing.Web/"]
RUN dotnet restore "Dotnet.Testing.Web/Dotnet.Testing.Web.csproj"
COPY . .
WORKDIR "/src/Dotnet.Testing.Web"
RUN dotnet build "Dotnet.Testing.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet.Testing.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Testing.Web.dll"]