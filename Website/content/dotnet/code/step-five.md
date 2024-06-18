---
title: "Writing the unsafe methods"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1004
type: "subpost"
---

Now we have all the `GET` or Safe methods written, let's write our Unsafe methods.

First we want to add or update a pony. In our API this will be covered under the same method as that is the way the Table storage API behaves. In other APIs you may wish to write a `POST` method for adding a new record, and a `PUT` method for updating an existing record.

In the `UnsafeMethods` method, add the following:

```csharp
app.MapPost("/ponies", async (PonyService ponyService, Pony pony, HttpContext context) =>
{
	if (pony is null)
	{
		context.Response.StatusCode = StatusCodes.Status400BadRequest;
		return;
	}
	context.Response.StatusCode = StatusCodes.Status200OK;
	await context.Response.WriteAsJsonAsync(await ponyService.AddOrUpdatePony(pony));
});
```

Here we are checking the pony object being passed in is not null and if it's not, then continuing to pass the data to the service to either add a new record or update an existing one.

> Challenge: write a `DELETE` method using `app.MapDelete` to delete a pony record. The endpoint should be RESTful.