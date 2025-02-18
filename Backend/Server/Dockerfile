# ==============================
# Stage 1: base (runtime)
# ==============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Lắng nghe tất cả địa chỉ IP trên cổng được chỉ định
ENV ASPNETCORE_URLS=http://+:${PORT}


EXPOSE 8080
EXPOSE 8081

# ==============================
# Stage 2: build (SDK)
# ==============================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy file .csproj
COPY ["Server.csproj", "."]
RUN dotnet restore "Server.csproj"

# Copy toàn bộ code
COPY . .
WORKDIR "/src"
RUN dotnet build "Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ==============================
# Stage 3: publish
# ==============================
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ==============================
# Stage 4: final (runtime)
# ==============================
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]
