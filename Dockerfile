FROM microsoft/dotnet:2.0-sdk AS build

ARG ConnectionStringForProd

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BLL/*.csproj ./BLL/
COPY DAL/*.csproj ./DAL/
COPY DomainObjects/*.csproj ./DomainObjects/
COPY WebApplication/*.csproj ./WebApplication/
COPY WebSocketManager/*.csproj ./WebSocketManager/
COPY WebSocketManager.Common/*.csproj ./WebSocketManager.Common/

RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR /app/WebApplication
RUN sed -i -- "s/#ConnectionStringForProd/${ConnectionStringForProd}/g" appsettings.Production.json
RUN dotnet build


FROM build AS publish
WORKDIR /app/WebApplication
RUN dotnet publish -c Release -o out


FROM microsoft/dotnet:2.0-runtime AS runtime
WORKDIR /app
COPY --from=publish /app/WebApplication/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "WebApplication.dll"]