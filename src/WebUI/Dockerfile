ARG REGISTRY_NAME=demo42.azurecr.io/
FROM ${REGISTRY_NAME}baseimages/microsoft/aspnetcore-runtime:linux-2.1 AS base
WORKDIR /app
ARG REGISTRY_NAME
ENV REGISTRY_NAME=${REGISTRY_NAME}

FROM ${REGISTRY_NAME}baseimages/microsoft/dotnet-sdk:linux-2.1 AS build
WORKDIR /src
COPY ./src/WebUI/WebUI.csproj src/WebUI/
COPY ./test/demo42tests/demo42tests.csproj ./test/demo42tests/
COPY ./Web.sln .
COPY ./NuGet.Config .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release

FROM build as test
WORKDIR /src
RUN dotnet test ./test/demo42tests/demo42tests.csproj

FROM build AS publish
WORKDIR ./src/WebUI
RUN dotnet publish WebUI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV VERSION=0.0.15
ENV REGISTRY_NAME=demo42.azurecr.io
ENV ASPNETCORE_ENVIRONMENT=Development
ENTRYPOINT ["dotnet", "WebUI.dll"]
