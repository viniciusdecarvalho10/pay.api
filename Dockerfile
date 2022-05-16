FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers

COPY ./src .
RUN dotnet restore

# Copy everything else and build
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base

EXPOSE 9000
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Pay.Api.Host.dll"]