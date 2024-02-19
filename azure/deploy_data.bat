SET STREETS_AZURE_CONTAINER=streetsdata
for /F "delims=" %%x in (.env) do (set "%%x")
az storage container create -n "%STREETS_AZURE_CONTAINER%" --account-name "%AZURE_STORAGE_ACCOUNT%" --auth-mode login
az storage blob upload --file "..\data\streets.json" --account-name "%AZURE_STORAGE_ACCOUNT%" --container-name "%STREETS_AZURE_CONTAINER%" --name "nova_data.json" --auth-mode key