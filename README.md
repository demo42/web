# Build2018App

## Building the image locally
```sh
docker build -t demo42/web:dev  -f ./src/WebUI/Dockerfile --build-arg demo42.azurecr.io .
```

## Building the image with ACR Build
```sh
az acr build -t demo42/web:{{.Build.ID}} -f ./src/WebUI/Dockerfile --build-arg demo42.azurecr.io .
```