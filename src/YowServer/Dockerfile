FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["src/YowServer/YowServer.csproj", "src/YowServer/"]
RUN dotnet restore "src/YowServer/YowServer.csproj"
COPY . .
WORKDIR "/src/src/YowServer"
RUN dotnet build "YowServer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "YowServer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YowServer.dll"]
