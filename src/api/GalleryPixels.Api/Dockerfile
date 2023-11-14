# Setup base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

LABEL maintainer="BrammyS <https://github.com/BrammyS>"
LABEL org.label-schema.name="GalleryPixels"
LABEL org.label-schema.description="The official API for GalleryPixels"
LABEL org.label-schema.vendor="Esendam Software"
LABEL org.label-schema.url="https://GalleryPixels.com"

WORKDIR /app
EXPOSE 80
EXPOSE 443

# Setup build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Setup workspace
WORKDIR /src
COPY /src .
WORKDIR /src/api/GalleryPixels.Api

# Restore dependencies
RUN dotnet restore "GalleryPixels.Api.csproj"

# Publish the application
RUN dotnet publish "GalleryPixels.Api.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GalleryPixels.Api.dll"]
