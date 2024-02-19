#!/usr/bin/bash
cd ../web/StreetFinder/
rm -rf pub
dotnet publish -o pub -c Release
cd pub
zip -r site.zip *
az webapp deployment source config-zip --src site.zip --resource-group "spencResourceGroup" --name "novastreets"
