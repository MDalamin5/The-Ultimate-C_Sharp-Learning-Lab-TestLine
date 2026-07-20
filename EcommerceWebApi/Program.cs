var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// app.UseHttpsRedirection();


// REST API: put, post, put, delete

app.MapGet("/hello", () =>
{
    return "Its a First messages From the Dotnet backend";
});

app.MapGet("/", () =>
{
    return "Welcome to First .Net api Playground.";
});

app.MapGet("/demo", () =>
{
    return "This is a demo api call.";
});

app.MapGet("/femo", () =>
{
    return "This is a Femo api rrr call.";
});

app.Run();
