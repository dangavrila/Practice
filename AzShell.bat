RESOURCEGROUP="learn-0fd59580-7a4c-444a-b6dd-391f92c6f417"
STORAGEACCT=learnstorage$(openssl rand -hex 5)
FUNCTIONAPP=learnfunctions$(openssl rand -hex 5)
AZREGION=""

az storage account create \
  --resource-group "$RESOURCEGROUP" \
  --name "$STORAGEACCT" \
  --kind StorageV2 \
  --location "$AZREGION"

az functionapp create \
  --resource-group "$RESOURCEGROUP" \
  --name "$FUNCTIONAPP" \
  --storage-account "$STORAGEACCT" \
  --runtime node \
  --consumption-plan-location "$AZREGION" \
  --functions-version 4
  
https://azdurablefunc444a.azurewebsites.net/api/orchestrators/{functionName}?code=