using System.Security.Cryptography.X509Certificates;
using EcommerceWebApi.Controllers;
using EcommerceWebApi.data;
using EcommerceWebApi.Interfaces;
using EcommerceWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        .SelectMany(e => e.Value?.Errors != null ? e.Value.Errors.Select(x => x.ErrorMessage) : new List<string>()).ToList();

        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "validations failed."));
   }; 
});
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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