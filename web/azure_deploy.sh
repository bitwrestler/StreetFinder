#!/usr/bin/env bash

APPLICATION_NAME=streetfinder
PUBLISH_DIR=../publish
PUBLISH_ZIP=../publish.zip
THIS_PWD=$(pwd)
dotnet publish -c Release -o $PUBLISH_DIR && \
cd $PUBLISH_DIR && \
zip -r $PUBLISH_ZIP ./* && \
cd "$THIS_PWD" && \
az webapp deploy --resource-group WebAppResourceGroup \
  --name $APPLICATION_NAME \
  --type zip \
  --src-path $PUBLISH_ZIP && \
rm $PUBLISH_ZIP
