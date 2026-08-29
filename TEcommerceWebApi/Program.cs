using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEcommerceWebApi.Controllers;
using TEcommerceWebApi.data;
using TEcommerceWebApi.Interfaces;
using TEcommerceWebApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//controller services registrations
builder.Services.AddControllers();
// add the repository Pattern Services and Map the Interfaces with the Services file.
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ProductService>();
// add auto-mapper
builder.Services.AddAutoMapper(typeof(Program));

//Centralized api responses
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value != null && e.Value.Errors.Count>0).
        SelectMany(e=>e.Value?.Errors != null ? e.Value.Errors.Select(x=>x.ErrorMessage): new List<string>()).ToList();

        return  new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(errors, 400, "Validations failed."));
    };
});

builder.Services.AddDbContext<AppDbContext>(options=> 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Build the API
app.MapGet("/", () => {
    // return a json object
    var response = new
    {
        message = "Welcome to Our site",
        status = "ok"
    };
   return  Results.Ok(response);
});


app.MapControllers();
app.Run();

