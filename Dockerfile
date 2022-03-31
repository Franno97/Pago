#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/Mre.Visas.Pago.Api/Mre.Visas.Pago.Api.csproj", "./Mre.Visas.Pago.Api/"]
COPY ["src/Mre.Visas.Pago.Application/Mre.Visas.Pago.Application.csproj", "./Mre.Visas.Pago.Application/"]
COPY ["src/Mre.Visas.Pago.Domain/Mre.Visas.Pago.Domain.csproj", "./Mre.Visas.Pago.Domain/"]
COPY ["src/Mre.Visas.Pago.Infrastructure/Mre.Visas.Pago.Infrastructure.csproj", "./Mre.Visas.Pago.Infrastructure/"]
RUN dotnet restore "Mre.Visas.Pago.Api/Mre.Visas.Pago.Api.csproj"

COPY ["src/Mre.Visas.Pago.Api", "./Mre.Visas.Pago.Api/"]
COPY ["src/Mre.Visas.Pago.Application", "./Mre.Visas.Pago.Application/"]
COPY ["src/Mre.Visas.Pago.Domain", "./Mre.Visas.Pago.Domain/"]
COPY ["src/Mre.Visas.Pago.Infrastructure", "./Mre.Visas.Pago.Infrastructure/"]
RUN dotnet build "Mre.Visas.Pago.Api/Mre.Visas.Pago.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mre.Visas.Pago.Api/Mre.Visas.Pago.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mre.Visas.Pago.Api.dll"]