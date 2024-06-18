---
title: "Starting the storage emulator"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1001
type: "subpost"
---

As per the prerequisites, you should have [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage) installed. If not, then do so now.

In the cli, type the follwoing to start Azurite:

```bash
azurite
```

It will start and you should see:

```bash
Azurite Blob service is starting at http://127.0.0.1:10000
Azurite Blob service is successfully listening at http://127.0.0.1:10000
Azurite Queue service is starting at http://127.0.0.1:10001
Azurite Queue service is successfully listening at http://127.0.0.1:10001
Azurite Table service is starting at http://127.0.0.1:10002
Azurite Table service is successfully listening at http://127.0.0.1:10002
```

These are all the services it has started for us and we can now use. We'll be using the table service.


To connect to the emulator from within our app we need to add some settings, which I have already done for you in `appsettings.json`.

```json
 "StorageSettings": {
   "StorageUri" : "http://127.0.0.1:10002/devstoreaccount1",
   "AccountName" :"devstoreaccount1",
   "StorageAccountKey" : "Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==",
   "TableName": "PoniesTable"
 }
```
This key is the same for everyone using the storage emulator, so no need to change this. If you were deploying to Azure, you would then adjust these values to match the ones in your Azure portal.

You'll need [Azure Storage Explorer](https://github.com/microsoft/AzureStorageExplorer/releases) if you want to look at your data.
