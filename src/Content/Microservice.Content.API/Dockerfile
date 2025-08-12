FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Microservice.Content.API/Microservice.Content.API.csproj", "Microservice.Content.API/"]
COPY ["Microservice.Content.Application/Microservice.Content.Application.csproj", "Microservice.Content.Application/"]
COPY ["Microservice.Content.Domain/Microservice.Content.Domain.csproj", "Microservice.Content.Domain/"]
COPY ["Microservice.Content.EFCore/Microservice.Content.EFCore.csproj", "Microservice.Content.EFCore/"]
COPY ["Microservice.Content.SharedKernel/Microservice.Content.SharedKernel.csproj", "Microservice.Content.SharedKernel/"]
RUN dotnet restore "Microservice.Content.API/Microservice.Content.API.csproj"
COPY . .
WORKDIR "/src/Microservice.Content.API"
RUN dotnet build "Microservice.Content.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Microservice.Content.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Content.API.dll"]
