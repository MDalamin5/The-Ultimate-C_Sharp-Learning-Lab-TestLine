var builder = WebApplication.CreateBuilder(args);

// 1. Add these two lines BEFORE builder.Build()
// This tells .NET to look at your endpoints and generate Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
