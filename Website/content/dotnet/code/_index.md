+++
archetype = "chapter"
title = "Writing Minimal APIs"
weight = 4
chapter= 3.4
+++

Download or clone this [repository]() if you haven't done so already.
We'll need to be in the `main` branch and in the `dotnet` folder.

Open the `*.csproj` folder in your IDE of choice.


We're going to build our first minimal API endpoint straight away!

Open the `program.cs` file and then add the following line before the `app.Run();` call

```csharp
app.MapGet("/", () =>  "Hello KCDC"); // 👈 add this line
app.Run();
```

Then start your application.

You can view this endpoint either by clicking on it in the swagger dashboard, or by navigating to the home page.