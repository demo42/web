#!/bin/sh

set -e
# SP, PASSWORD , CLUSTER_NAME, CLUSTER_RESOURCE_GROUP
az configure --defaults acr=$RUN_REGISTRYNAME

az login \
    --service-principal \
    --username $SP \
    --password $PASSWORD \
    --tenant $TENANT  > /dev/null

az aks get-credentials \
    -g $CLUSTER_RESOURCE_GROUP \
    -n $CLUSTER_NAME 

echo -- helm init --client-only --
helm init --client-only # > /dev/null

echo -- az acr helm repo add --
az acr helm repo add 

echo -- helm fetch --untar $RUN_REGISTRYNAME/importantThings --
ls

helm fetch --untar $RUN_REGISTRYNAME/importantThings

echo -- helm upgrade demo42 ./helm/importantThings --
helm upgrade demo42 ./helm/importantThings \
    --reuse-values \
    --set web.image=$RUN_REGISTRY/demo42/web:$RUN_ID
