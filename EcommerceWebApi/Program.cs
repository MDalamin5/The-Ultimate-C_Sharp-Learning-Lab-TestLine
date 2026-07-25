using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// 1. Add these two lines BEFORE builder.Build()
// This tells .NET to look at your endpoints and generate Swagger documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add controllerServices
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
   options.InvalidModelStateResponseFactory = context =>
   {
        var errors = context.ModelState
        .Where(e => e.Value != null && e.Value.Errors.Count > 0)
        .Select(e => new
        {
            Field = e.Key,
            Message = e.Value != null ? e.Value.Errors.Select(x => x.ErrorMessage).ToArray() : new string[0]
        }).ToList();

        //Join all error messages
        var errorString = string.Join("; ", errors.Select(e => $"{e.Field} : {string.Join(", ", e.Message)}"));

        return new BadRequestObjectResult(new
        {
            Message = "Validation Failed",
            Errors = errorString
        });
   }; 
});

var app = builder.Build();

// middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();


// REST API: put, post, put, delete

app.MapGet("/health", () =>
{
    return Results.Ok("Ok");
});

app.MapGet("/", () =>
{
    var response = new {
        message = "This is Demo JSOn Object.",
        success = true
    };

    return Results.Ok(response);
});

app.MapControllers();
app.Run();