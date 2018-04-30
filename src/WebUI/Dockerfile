ARG REGISTRY_NAME
FROM ${REGISTRY_NAME}baseimages/microsoft/aspnetcore-runtime:linux-2.1 AS base
RUN echo ---- FROM runtime
RUN date
RUN echo ------------------
WORKDIR /app
ARG REGISTRY_NAME
ENV REGISTRY_NAME=${REGISTRY_NAME}

FROM ${REGISTRY_NAME}baseimages/microsoft/dotnet-sdk:linux-2.1 AS build
RUN echo ---- FROM sdk
RUN date
RUN echo ------------------
WORKDIR /src
COPY src/WebUI/WebUI.csproj src/WebUI/
COPY NuGet.Config .
RUN dotnet restore src/WebUI/WebUI.csproj
COPY . .
WORKDIR /src/src/WebUI
RUN dotnet build WebUI.csproj -c Release -o /app

FROM build AS publish
RUN echo ---- FROM publis
RUN date
RUN echo ------------------
RUN dotnet publish WebUI.csproj -c Release -o /app

FROM base AS final
RUN echo ---- FROM final
RUN date
RUN echo ------------------
WORKDIR /app
COPY --from=publish /app .
ENV VERSION=0.0.10
ENTRYPOINT ["dotnet", "WebUI.dll"]
RUN echo ---- build-complete
RUN date
RUN echo ------------------
