---
title: "Writing an API endpoint"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1004
type: "subpost"
---

If you didn't get to the challenge in step three, don't fret - we'll do it now.

Either in the root of the application, or a directory called `Endpoints` add a new file called `PonyEndpoints.cs`.

Then update the file to havve an extension method for `WebApplication` like so:

```csharp
using TourOfPonies.Api.Data;
using TourOfPonies.Api.Models;

namespace TourOfPonies.Api.Endpoints;
public static class PonyEndpoints
{
	public static WebApplication AddPoniesEndpoints(this WebApplication app)
	{
		return app;
	}
}
```

To continue organising the API, let's add a further two extension methods, but this time they will be private:

```csharp
public static class PonyEndpoints
{
	public static WebApplication AddPoniesEndpoints(this WebApplication app)
	{
        app.SafeMethods();
        app.UnsafeMethods();
		return app;
	}
    private static WebApplication SafeMethods(this WebApplication app)
    {
    }
    private static WebApplication UnsafeMethods(this WebApplication app)
    {
    }
}

```

We can split our methods into `Safe` - think requests like `GET` where we are just retrieving data and not altering it, and `Unsafe` - where we are changing the data or adding new data.

 We can then add the `AddPoniesEndpoints` method call to `app` in the `Program.cs` file.

 ## RESTful APIs

 Differentiating calls to the `/ponies` endpoint based on the HTTP verb is a RESTful design principle. In REST, a resource is represented by a URL, and the action performed on the resource is determined by the HTTP method used in the request.
* GET /ponies: Retrieve a list of all ponies.
* POST /ponies: Create a new pony.
* GET /ponies/{id}: Retrieve details of a specific pony by ID.
* PUT /ponies/{id}: Update a specific pony by ID.
* DELETE /ponies/{id}: Delete a specific pony by ID.
* PATCH /ponies/{id}: Partially update a specific pony by ID.


 ### Making a GET request to Ponies

 Let's write our first endpoint using the Ponies data store.

 In the `SafeMethods` method, write a new `GET` request to return all the ponies:

 ```csharp
app.MapGet("/ponies", async (PonyService ponyService, HttpContext context) =>
	{

    });
```

Above we are passing in the `PonyService` and the `HttpContext` as dependencies. We need the service to access the data, and the context to handle the response.

Update the request with the following:

```csharp
	app.MapGet("/ponies", async (PonyService ponyService, HttpContext context) =>
		{
			context.Response.StatusCode = StatusCodes.Status200OK;
			await context.Response.WriteAsJsonAsync(await ponyService.GetAll());
		});

```

In this code, we're assuming that the request will be okay and returning a 200, followed by all the pony data.

> How could we write this better to ensure we handle a failure in the app?

Let's now write a `GET` request to get a pony by ID.

```csharp
	app.MapGet("/ponies/{id}", async (PonyService ponyService, string id, HttpContext context) =>
{
	if (string.IsNullOrEmpty(id))
	{
		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		return;
	}

	var pony = await ponyService.Get(id);

	if (pony is null)
	{
		context.Response.StatusCode = StatusCodes.Status404NotFound;
		return;
	}

	context.Response.StatusCode = StatusCodes.Status200OK;
	await context.Response.WriteAsJsonAsync(pony);
});

```

This time we are passing in the `id` as a string and the appliucation is clever enough to work our what is a parameter and what is a dependency. You can see we are also defining `id` in the query `/ponies/{id}` - which is a standard practice when getting via ID.

We also have some check going on:
* we check that `id` is not null or empty
* we then try to get the pony from the service
* we then check to see if the response is null, 
* and assign an appropriate status code, returning the pony if it's all okay.


Next we have a requirement to get a pony by its name. We could write it in a similar way to getting a pony by ID, but it would be more RESTful if we used a query parameter on the get all ponies method, so let's do that.

Update the `/ponies` endpoint with the following code which checks for a a query parameter. A query parameter looks like this `/ponies?name=applejack`:

```csharp
app.MapGet("/ponies", async (PonyService ponyService, HttpContext context) =>
	{
		if (context.Request.Query.TryGetValue("name", out var nameValues))
		{
			var name = nameValues.FirstOrDefault();
			if(string.IsNullOrEmpty(name))
			{
				var pony = await ponyService.GetByPartialName(name);

				if (pony is null)
				{
					context.Response.StatusCode = StatusCodes.Status404NotFound;
					return;
				}

				context.Response.StatusCode = StatusCodes.Status200OK;
				await context.Response.WriteAsJsonAsync(pony);
				return;
			}
		}

		context.Response.StatusCode = StatusCodes.Status200OK;
		await context.Response.WriteAsJsonAsync(await ponyService.GetAll());
	});
```

If you wanted to write a specific endpoint for it, you would need to write it as follows so as not to conflict with the get by id method:

```csharp
app.MapGet("/ponies/name={name}", async (PonyService ponyService, HttpContext context, string name) =>
{
	if (string.IsNullOrEmpty(name))
	{
		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		return;
	}

	var ponies = await ponyService.GetByPartialName(name);

	if (ponies.Count == 0)
	{
		context.Response.StatusCode = StatusCodes.Status404NotFound;
		return;
	}

	context.Response.StatusCode = StatusCodes.Status200OK;
	await context.Response.WriteAsJsonAsync(ponies);
});
```

> Challenge: write an endpoint for `/ponies/name={name}` and one for `/ponies/heroes`.