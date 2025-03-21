﻿# Setup base image
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS base

LABEL maintainer="BrammyS <https://github.com/BrammyS>"
LABEL org.label-schema.name="GalleryPixels"
LABEL org.label-schema.description="The official client for GalleryPixels"
LABEL org.label-schema.vendor="Esendam Software"
LABEL org.label-schema.url="https://GalleryPixels.com"

# Setup workspace
WORKDIR /src
COPY /src .

# install node
RUN apk add --no-cache nodejs npm

# Restore dependencies
RUN dotnet restore "/src/client/GalleryPixels.UI/GalleryPixels.UI.csproj"

# Publish the application
RUN dotnet publish "/src/client/GalleryPixels.UI/GalleryPixels.UI.csproj" \
    -c Release  \
    -o /app/publish \
    --no-restore

# Serve wwwroot with nginx
FROM nginx:alpine AS final

WORKDIR /usr/share/nginx/html
COPY --from=base /app/publish/wwwroot .

# Create and copy Nginx config
RUN echo 'server { \
    listen 80; \
    server_name localhost; \
    root /usr/share/nginx/html; \
    index index.html; \
    location / { \
        try_files $uri $uri/ /index.html; \
    } \
    location /_framework/ { \
        add_header Cache-Control "no-cache, no-store, must-revalidate"; \
        expires -1; \
    } \
}' > /etc/nginx/conf.d/default.conf

RUN chmod -R 755 /usr/share/nginx/html

EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
