# Setup base image
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS base

LABEL maintainer="BrammyS <https://github.com/BrammyS>"
LABEL org.label-schema.name="GalleryPixels"
LABEL org.label-schema.description="The official API for GalleryPixels"
LABEL org.label-schema.vendor="Esendam Software"
LABEL org.label-schema.url="https://GalleryPixels.com"

# Setup workspace
WORKDIR /src
COPY /src .
EXPOSE 80

# Restore dependencies
RUN dotnet restore "/src/api/GalleryPixels.Api/GalleryPixels.Api.csproj" --runtime linux-musl-x64

# Publish the application
RUN dotnet publish "/src/api/GalleryPixels.Api/GalleryPixels.Api.csproj" \
    -c Release -o /app/publish \
    --no-restore \
    --runtime linux-musl-x64 \
    --self-contained true \
    /p:PublishSingleFile=true

# Setup final runtime image
FROM mcr.microsoft.com/dotnet/runtime-deps:10.0-alpine AS final

# upgrade musl to remove potential vulnerability
RUN apk upgrade musl
WORKDIR /app
COPY --from=base /app/publish .

EXPOSE 8080
ENTRYPOINT ["./GalleryPixels.Api"]