# Setup base image
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

# Rename appsettings
RUN mv "/src/client/GalleryPixels.UI/wwwroot/appsettings.TEMPLATE.json" "/src/client/GalleryPixels.UI/wwwroot/appsettings.json"

# Restore dependencies
RUN dotnet restore "/src/client/GalleryPixels.UI/GalleryPixels.UI.csproj"

# Publish the application
RUN dotnet publish "/src/client/GalleryPixels.UI/GalleryPixels.UI.csproj" \
    -c Release  \
    -o /app/publish \
    --no-restore

# Serve wwwroot with nginx
FROM nginx:alpine AS final

WORKDIR /app
COPY --from=base /app/publish/wwwroot .

# Create and copy Nginx config
RUN echo 'server { \
    listen 80; \
    server_name localhost; \
    root /app; \
    index index.html; \
    location / { \
        try_files $uri $uri/ /index.html; \
    } \
    location /_framework/ { \
        add_header Cache-Control "no-cache, no-store, must-revalidate"; \
        expires -1; \
    } \
}' > /etc/nginx/conf.d/default.conf

RUN chmod -R 755 /app

EXPOSE 80
ENTRYPOINT ["nginx", "-g", "daemon off;"]
