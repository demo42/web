# Demo42 Web Image
See [deploy/readme.md](../deploy/readme.md) for an overview of demo42

## Building the image locally
```sh
docker build -t demo42/web:dev -f ./src/WebUI/Dockerfile --build-arg REGISTRY_NAME=demo42.azurecr.io .
```

## Building the image with ACR Build GA
```sh
az acr build -t demo42/web:{{.Run.ID}} -f ./src/WebUI/Dockerfile --build-arg REGISTRY_NAME=demo42.azurecr.io .
```

## Build, Test, Deploy the image(s) with ACR Tasks
```sh
az acr run -f acr-task.yaml  .
```

## Create an ACR Task

While ACR Tasks are limited to dogfood, get the environment variables from [deploy/readme.md](../deploy/readme.md#Get-the-credentials-from-KeyVault)
```sh
ACR_NAME=demo42
BRANCH=master

az acr task create \
  -n demo42-web \
  --file acr-task.yaml \
  --context https://github.com/demo42/web.git \
  --git-access-token $(az keyvault secret show \
            --vault-name ${AKV_NAME} \
            --name demo42-git-token \
            --query value -o tsv) \
  --set CLUSTER_NAME=demo42-staging-eus \
  --set CLUSTER_RESOURCE_GROUP=demo42-staging-eus \
  --set-secret SP=$(az keyvault secret show \
            --vault-name ${AKV_NAME} \
            --name demo42-serviceaccount-user \
            --query value -o tsv) \
  --set-secret PASSWORD=$(az keyvault secret show \
            --vault-name ${AKV_NAME} \
            --name demo42-serviceaccount-pwd \
            --query value -o tsv) \
  --set-secret TENANT=$(az keyvault secret show \
            --vault-name ${AKV_NAME} \
            --name demo42-serviceaccount-tenant \
            --query value -o tsv) \
  --registry $ACR_NAME 
```
Run the scheduled task
```sh
az acr task run -n demo42-web
```
