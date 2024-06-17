using Microsoft.AspNetCore.Mvc;
using TourOfPonies.Api.Data;
using TourOfPonies.Api.Models;

namespace TourOfPonies.Api.Endpoints;

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
		// get all ponies
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
			})
		.WithOpenApi();

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
		})
		.WithOpenApi();

		app.MapGet("/ponies/{id}/avatar", async (PonyService ponyService, string id, HttpContext context) =>
		{
			if (string.IsNullOrEmpty(id))
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				return;
			}

			var pony = await ponyService.GetAvatarById(id);

			if (pony is null)
			{
				context.Response.StatusCode = StatusCodes.Status404NotFound;
				return;
			}

			context.Response.StatusCode = StatusCodes.Status200OK;
			await context.Response.WriteAsJsonAsync(pony);
		})
		.WithOpenApi();

		app.MapGet("/ponies/name={name}", async (PonyService ponyService, HttpContext context, string name) =>
		{

			//if (context.Request.Query.TryGetValue("name", out var nameValues))
			//{
			//	name = nameValues.FirstOrDefault();
			//}
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
		})
		.WithOpenApi();

		app.MapGet("/ponies/heroes", async (PonyService ponyService, HttpContext context) =>
		{
			context.Response.StatusCode = StatusCodes.Status200OK;
			await context.Response.WriteAsJsonAsync(await ponyService.GetAllHeroes());
		})
		.WithOpenApi();

		return app;
	}

	private static WebApplication UnsafeMethods(this WebApplication app)
	{
		app.MapPost("/ponies", async (PonyService ponyService, Pony pony, HttpContext context) =>
		{
			if (pony is null)
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				return;
			}
			context.Response.StatusCode = StatusCodes.Status200OK;
			await context.Response.WriteAsJsonAsync(await ponyService.AddOrUpdatePony(pony));
		})
		.WithOpenApi();


		app.MapDelete("/ponies/{id}", async (string id, PonyService ponyService, HttpContext context) =>
		{
			if (string.IsNullOrEmpty(id))
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				return;
			}

			
			bool isSuccessful = await ponyService.DeletePony(id);

			context.Response.StatusCode = isSuccessful ? StatusCodes.Status204NoContent : StatusCodes.Status500InternalServerError;
		})
		.WithOpenApi();


		return app;
	}
}

