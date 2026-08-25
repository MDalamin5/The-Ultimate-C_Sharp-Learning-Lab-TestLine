using Microsoft.AspNetCore.Mvc;
using TEcommerceWebApi.Controllers;
using TEcommerceWebApi.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//controller services registrations
builder.Services.AddControllers();
builder.Services.AddSingleton<CategoryService>();

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

