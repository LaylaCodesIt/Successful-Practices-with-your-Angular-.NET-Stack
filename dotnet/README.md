# Installs

- [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Storage explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer/)
- [Azurite](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio%2Cblob-storage)


From the CLI start the Azurite service:

```bash
Azurite
```

From the CLI, navigate to the top level of the web API project, the type:

```bash
dotnet restore
dotnet run
```
Navigate to [http://localhost:5099/swagger/index.html](http://localhost:5099/swagger/index.html)

The `seed` patch endpoint needs to be run once to create the data. You can use the Storage Explorer to look at the data.

Then play with the other endpoints!