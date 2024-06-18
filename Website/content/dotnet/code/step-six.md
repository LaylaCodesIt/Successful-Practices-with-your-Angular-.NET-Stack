---
title: "It's always CORS!"
date: 2024-01-05T16:36:46Z
draft: false
weight: 1006
type: "subpost"
---

![Screenshot of Laylas Twitter status regarding CORS](cors.png)
<iframe src="https://giphy.com/embed/sIE0hveuiwCNG" width="960" height="538" style="" frameBorder="0" class="giphy-embed" allowFullScreen></iframe><p><a href="https://giphy.com/gifs/angry-rage-anger-sIE0hveuiwCNG">via GIPHY</a></p>

Using Swagger or the browser, we can easily explore our API. However, if we want to use the API with another app, say an Angular app, we will need to add support for CORS.

> CORS (Cross-Origin Resource Sharing) is a security feature implemented by web browsers that allows or restricts web applications from making requests to a domain different from the one that served the web page. It is essential for enabling secure cross-origin requests and data sharing between different domains, by using HTTP headers to inform browsers whether to allow such cross-origin requests.


Luckily, adding support for CORS is easy in .NET.

Back in our `Program.cs` file we need to add the following to the `Services`:

```csharp
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    
                });
});
```

The above isn't very secure as we are allowing any origin, any method, and any header - but we could tailor this to our specific requirements at a later date.


Finally we need to add the middleware to the `WebApplication` so just add the following to the `Program.cs` file also:

```csharp
app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCors(); // 👈 add this line
```

