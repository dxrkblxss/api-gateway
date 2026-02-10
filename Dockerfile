# -----------------------------
# 1) Build stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["YARP.csproj", "./"]
RUN dotnet restore "YARP.csproj"

COPY . .
RUN dotnet publish "YARP.csproj" -c Release -o /app/publish --no-restore /p:UseAppHost=false

# -----------------------------
# 2) Runtime stage
# -----------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_RUNNING_IN_CONTAINER=true
EXPOSE 80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "YARP.dll"]