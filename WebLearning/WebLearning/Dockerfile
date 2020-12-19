FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app 

# copy csproj and restore as distinct layers
COPY WebLearning/*.sln ./WebLearning/
COPY WebLearning/WebLearning/*.csproj ./WebLearning/WebLearning/
COPY WebLearning/WebLearning.DataPresentation/*.csproj ./WebLearning/WebLearning.DataPresentation/
COPY Common/*.sln ./Common/
COPY Common/Common/*.csproj ./Common/Common/

#restore packages
WORKDIR /app/WebLearning
RUN dotnet restore 

WORKDIR /app/Common
RUN dotnet restore

WORKDIR /app 

# copy everything else and build app
COPY WebLearning/WebLearning/. ./WebLearning/WebLearning/
COPY WebLearning/WebLearning.DataPresentation/. ./WebLearning/WebLearning.DataPresentation/
COPY Common/Common/. ./Common/Common/

WORKDIR /app/WebLearning/WebLearning
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app 

COPY --from=build /app/WebLearning/WebLearning/out ./
ENTRYPOINT ["dotnet", "WebLearning.dll"]
