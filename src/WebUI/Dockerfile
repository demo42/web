ARG REGISTRY_NAME
FROM ${REGISTRY_NAME}baseimages/microsoft/aspnetcore-runtime:linux-2.1 AS base
WORKDIR /app

FROM ${REGISTRY_NAME}baseimages/microsoft/dotnet-sdk:linux-2.1 AS build
WORKDIR /src
COPY src/WebUI/WebUI.csproj src/WebUI/
COPY NuGet.Config .
RUN dotnet restore src/WebUI/WebUI.csproj
COPY . .
WORKDIR /src/src/WebUI
RUN dotnet build WebUI.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish WebUI.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV VERSION=0.0.10
ENTRYPOINT ["dotnet", "WebUI.dll"]
