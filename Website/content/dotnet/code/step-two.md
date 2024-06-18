---
title: "Getting the data seeded"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1002
type: "subpost"
---

Now that we have the storage emulator configured, it's time to seed the data.

As mentioned before, I have already created all the data files we will need. But first we need to make them available via dependency injection, which comes built in to ASP.NET.

Let's head over to the `Program.cs` file and start updating it.

### Adding configuration

To read in `StorageSettings` from the `appsettings.json` we need to add the following:

```csharp
var builder = WebApplication.CreateBuilder(args); // 👈 this line already existed

builder.Configuration
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables()
	.AddUserSecrets<Program>()
	.AddCommandLine(args)
	.Build();

var configSection = builder.Configuration.GetSection("StorageSettings");
var storageSettings = new StorageSettings();
configSection.Bind(storageSettings);

builder.Services.AddSingleton<StorageSettings>(storageSettings);

```

The above code first makes the various configurations known to the application, then it fetches the section called `StorageSettings` from the JSON and binds that to a C# object called `StorageSettings`.

We then add the storageSettings object to the [IoC container](https://en.wikipedia.org/wiki/Inversion_of_control) called `Services` which is available on the `builder`.


### Adding the dependencies

To be able to us the `TableStorageContext`, `TableStorageSeed`, and `PonyService`, these too, will need to be added to the IoC container.

Still in the `Porgram.cs` file, add the following:

```csharp
builder.Services.AddScoped<TableStorageContext>();
builder.Services.AddScoped<TableStorageSeed>();
builder.Services.AddScoped<PonyService>();
```

For more information on dependency injection and dependency lifecycles see the [documentation](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0).

Now all that's left to do is to write a `PATCH` API to seed the data.

`PATCH` is usually used to partially modify a resource, but we will use it here as I like to denote that this is a one-time-use kind of API.
We'll add this after the `GET` we wrote earlier:

```csharp
app.MapGet("/", () =>  "Hello KCDC"); 

app/MapPatch("/seed", () => {} ); // 👈 add this line

app.Run();
```

This hasn't done anything yet, other than to create the endpoint. We now need to inject the `TableStorageSeed` class and call its `Seed` method. Minimal APIs allow us to inject straight into the expression like so:

```csharp
app.MapPatch("/seed", async (TableStorageSeed seed) =>
{
	return await seed.Seed();
});
```

As the `Seed` method is asynchronous, we need to add `async/await` to the expression as shown.


Now that we've added that, we can start the application and call the `seed` endpoint.

This method is not ***idempotent***, so only call this once.  Then using Azure Storage Explorer, check out your new table data.

> Challenge: How could we make this process ***idempotent***?