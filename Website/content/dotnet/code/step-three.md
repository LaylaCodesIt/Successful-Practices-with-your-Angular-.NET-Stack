---
title: "Organising minimal APIs"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1003
type: "subpost"
---

> Looking at the `Program.cs` file, what do you think will happen as we add more dependencies and more APIs?


### Extension Methods

We can leverage extension methods to better organise our application.

Let's start with writing one to abstract our dependencies away.

In the root of the app, add a new file called `ServiceExtensions.cs`.  Then update the code to the following:

```csharp
namespace TourOfPonies.Api;

public static class ServiceExtensions
{
	public static IServiceCollection AddDependencies(this IServiceCollection services)
	{
		return services;
	}
}
```

Note the use of both the `static` and the `this` keyword.  We can now use this `AddDependencies` method in our `Program.cs` file.

```csharp
builder.Services.AddDependencies();
```

Then cut the three dependencies; `TableStorageContext`, `TableStorageSeed`, and `PonyService`. We then re-add those in the new `AddDependencies` method, like so:

```csharp
public static IServiceCollection AddDependencies(this IServiceCollection services)
{
    services.AddScoped<TableStorageContext>();
    services.AddScoped<TableStorageSeed>();
    services.AddScoped<PonyService>();
    return services;
}
```

You can see how this will really help with the organisation of the application as it gets bigger.

> Challenge: Create an extension method for the `WebApplication app` in a class called `PonyEndpoints` so we can add our API endpoints to it.
