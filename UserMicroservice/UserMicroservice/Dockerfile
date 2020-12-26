FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

# copy csproj and restore as distinct layers
COPY UserMicroservice/*.sln ./UserMicroservice/
COPY UserMicroservice/UserMicroservice/*.csproj ./UserMicroservice/UserMicroservice/
COPY UserMicroservice/UserMicroservice.DataPresentation/*.csproj ./UserMicroservice/UserMicroservice.DataPresentation/
COPY Common/*.sln ./Common/
COPY Common/Common/*.csproj ./Common/Common/

#restore packages
WORKDIR /app/UserMicroservice
RUN dotnet restore 

WORKDIR /app/Common
RUN dotnet restore

WORKDIR /app 

# copy everything else and build app
COPY UserMicroservice/UserMicroservice/. ./UserMicroservice/UserMicroservice/
COPY UserMicroservice/UserMicroservice.DataPresentation/. ./UserMicroservice/UserMicroservice.DataPresentation/
COPY Common/Common/. ./Common/Common/

WORKDIR /app/UserMicroservice/UserMicroservice
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

COPY --from=build /app/UserMicroservice/UserMicroservice/out ./
ENTRYPOINT ["dotnet", "UserMicroservice.dll"]
