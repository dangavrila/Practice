RESOURCEGROUP="learn-0fd59580-7a4c-444a-b6dd-391f92c6f417"
STORAGEACCT=learnstorage$(openssl rand -hex 5)
FUNCTIONAPP=learnfunctions$(openssl rand -hex 5)

az storage account create \
  --resource-group "$RESOURCEGROUP" \
  --name "$STORAGEACCT" \
  --kind StorageV2 \
  --location centralus

az functionapp create \
  --resource-group "$RESOURCEGROUP" \
  --name "$FUNCTIONAPP" \
  --storage-account "$STORAGEACCT" \
  --runtime node \
  --consumption-plan-location centralus \
  --functions-version 3
  
https://azfunwebhooks.azurewebsites.net/api/myhttptrigger?code=lO2KrQVYmbu2aO5ugNAteKEdDkPFrzRTJzCuJt6UQv6QAzFu9evLSw%3D%3D&name=dan.gav103%40hotmail.com

lO2KrQVYmbu2aO5ugNAteKEdDkPFrzRTJzCuJt6UQv6QAzFu9evLSw==

const hmac = Crypto.createHmac("sha1", "lO2KrQVYmbu2aO5ugNAteKEdDkPFrzRTJzCuJt6UQv6QAzFu9evLSw==");
const signature = hmac.update(JSON.stringify(req.body)).digest('hex');