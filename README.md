# Demo42 Web Image

## Building the image locally
```sh
docker build -t demo42/web:dev  -f ./src/WebUI/Dockerfile --build-arg demo42.azurecr.io .
```

## Building the image with ACR Build
```sh
az acr build -t demo42/web:{{.Build.ID}} -f ./src/WebUI/Dockerfile --build-arg demo42.azurecr.io .
```

## Build, Test, Deploy the image(s) with ACR Tasks
```sh
az acr run -f acr-task.yaml  .
```

## Create an ACR Task

Get the environment variables from [deploy/readme.md](../deploy/readme.md#Get-the-credentials-from-KeyVault)
```sh
ACR_NAME=demo42
BRANCH=master
az acr task create \
  -n demo42-web \
  --file acr-task.yaml \
  --context https://github.com/demo42/web.git \
  --branch $BRANCH \
  --set-secret TENANT=$TENANT \
  --set-secret SP=$SP \
  --set-secret PASSWORD=$PASSWORD \
  --set CLUSTER_NAME=demo42-staging-eus \
  --set CLUSTER_RESOURCE_GROUP=demo42-staging-eus \
  --set-secret REGISTRY_USR=$ACR_PULL_USR \
  --set-secret REGISTRY_PWD=$ACR_PULL_PWD \
  --git-access-token ${GIT_TOKEN} \
  --registry $ACR_NAME 
```
