FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /sln

# Copy solution and all .csproj 
COPY ./*.sln ./
COPY src/*/*.csproj ./

# Reconstruct directory
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Restore all required nuget package
RUN dotnet restore --disable-parallel

# Copy all source code
COPY . .

# Build the project
WORKDIR /sln/src/YowServer
RUN dotnet build "YowServer.csproj" -c Release -o /app

# Publish the project
FROM build AS publish
RUN dotnet publish "YowServer.csproj" -c Release -o /app

# Copy the published binary and set the entry point
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YowServer.dll"]
