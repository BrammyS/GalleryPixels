# Setup base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

LABEL maintainer="BrammyS <https://github.com/BrammyS>"
LABEL org.label-schema.name="GalleryPixels"
LABEL org.label-schema.description="The official web application for GalleryPixels"
LABEL org.label-schema.vendor="Esendam Software"
LABEL org.label-schema.url="https://GalleryPixels.com"

WORKDIR /app
EXPOSE 80
EXPOSE 443

# Setup build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Setup workspace
WORKDIR /src
COPY /src .
WORKDIR /src/GalleryPixels.Web

# Restore dependencies
RUN dotnet restore "GalleryPixels.Web.csproj"

# Publish the application
RUN dotnet publish "GalleryPixels.Web.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GalleryPixels.Web.dll"]
