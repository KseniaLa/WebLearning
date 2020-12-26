FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

# copy csproj and restore as distinct layers
COPY TaskMicroservice/*.sln ./TaskMicroservice/
COPY TaskMicroservice/TaskMicroservice/*.csproj ./TaskMicroservice/TaskMicroservice/
COPY TaskMicroservice/TaskMicroservice.DataPresentation/*.csproj ./TaskMicroservice/TaskMicroservice.DataPresentation/
COPY Common/*.sln ./Common/
COPY Common/Common/*.csproj ./Common/Common/

#restore packages
WORKDIR /app/TaskMicroservice
RUN dotnet restore 

WORKDIR /app/Common
RUN dotnet restore

WORKDIR /app 

# copy everything else and build app
COPY TaskMicroservice/TaskMicroservice/. ./TaskMicroservice/TaskMicroservice/
COPY TaskMicroservice/TaskMicroservice.DataPresentation/. ./TaskMicroservice/TaskMicroservice.DataPresentation/
COPY Common/Common/. ./Common/Common/

WORKDIR /app/TaskMicroservice/TaskMicroservice
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

COPY --from=build /app/TaskMicroservice/TaskMicroservice/out ./
ENTRYPOINT ["dotnet", "TaskMicroservice.dll"]
