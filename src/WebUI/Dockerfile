ARG REGISTRY_NAME=demo42.azurecr.io/
FROM ${REGISTRY_NAME}baseimages/microsoft/dotnet-sdk:linux-2.1 AS build
WORKDIR /src
COPY ./test/demo42tests/demo42tests.csproj ./test/demo42tests/
COPY ./src/WebUI/WebUI.csproj src/WebUI/
COPY ./Web.sln .

RUN dotnet restore ./src/WebUI/WebUI.csproj
COPY . .
RUN dotnet build -c Release ./src/WebUI/WebUI.csproj

FROM build as test
WORKDIR /src
RUN dotnet test ./test/demo42tests/demo42tests.csproj

FROM build AS publish
WORKDIR ./src/WebUI
RUN dotnet publish WebUI.csproj -c Release -o /app

ARG REGISTRY_NAME=demo42.azurecr.io/
FROM ${REGISTRY_NAME}baseimages/microsoft/aspnetcore-runtime:linux-2.1 AS base
WORKDIR /app
ARG REGISTRY_NAME
ENV REGISTRY_NAME=${REGISTRY_NAME}

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV VERSION=0.0.18
ENV ASPNETCORE_ENVIRONMENT=development
ENTRYPOINT ["dotnet", "WebUI.dll"]
