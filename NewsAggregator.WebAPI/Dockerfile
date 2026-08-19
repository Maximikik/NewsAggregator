FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NewsAggregator.WebAPI/NewsAggregator.WebAPI.csproj", "NewsAggregator.WebAPI/"]
RUN dotnet restore "./NewsAggregator.WebAPI/NewsAggregator.WebAPI.csproj"
COPY . .
WORKDIR "/src/NewsAggregator.WebAPI"
RUN dotnet build "./NewsAggregator.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NewsAggregator.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsAggregator.WebAPI.dll"]