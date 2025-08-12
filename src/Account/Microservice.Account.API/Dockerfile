# ---------- base (runtime) ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ---------- build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY Microservice.Account.API/Microservice.Account.API.csproj Microservice.Account.API/
COPY Microservice.Account.Application/Microservice.Account.Application.csproj Microservice.Account.Application/
COPY Microservice.Account.Domain/Microservice.Account.Domain.csproj Microservice.Account.Domain/
COPY Microservice.Account.EFCore/Microservice.Account.EFCore.csproj Microservice.Account.EFCore/
COPY Microservice.Account.SharedKernel/Microservice.Account.SharedKernel.csproj Microservice.Account.SharedKernel/
RUN dotnet restore Microservice.Account.API/Microservice.Account.API.csproj

COPY . .

WORKDIR /src/Microservice.Account.API
RUN dotnet build Microservice.Account.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# ---------- publish ----------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/Microservice.Account.API
RUN dotnet publish Microservice.Account.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------- final ----------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Account.API.dll"]
