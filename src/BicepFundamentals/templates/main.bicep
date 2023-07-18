@allowed([
  'nonprod'
  'prod'
])
param envType string
param location string = 'westus3'

param storageAccountName string = 'toylaunch${uniqueString(resourceGroup().id)}'
param appServiceAppName string = 'toylaunch${uniqueString(resourceGroup().id)}'

var storageAccountSkuName = (envType == 'prod') ? 'Standard_GRS' : 'Standard_LRS'

@description('Storage account for toy launch site')
resource storageAccount 'Microsoft.Storage/storageAccounts@2022-05-01' = {
  name: storageAccountName
  location: location
  sku: {
    name: storageAccountSkuName
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
  }
}

module appService '../modules/appService.bicep' = {
  name: 'appService'
  params: {
    location: location
    envType: envType
    appServiceAppName: appServiceAppName
  }
}

output appServiceAppHostName string = appService.outputs.appServiceAppHostName
