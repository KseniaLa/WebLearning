FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

# copy csproj and restore as distinct layers
COPY ApiGateway/*.sln ./ApiGateway/
COPY ApiGateway/ApiGateway/*.csproj ./ApiGateway/ApiGateway/

#restore packages
WORKDIR /app/ApiGateway
RUN dotnet restore 

WORKDIR /app 

# copy everything else and build app
COPY ApiGateway/ApiGateway/. ./ApiGateway/ApiGateway/

WORKDIR /app/ApiGateway/ApiGateway
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

COPY --from=build /app/ApiGateway/ApiGateway/out ./
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
