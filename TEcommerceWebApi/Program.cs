using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//controller services registrations
builder.Services.AddControllers();
//validation services
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
                .Where(e => e.Value != null && e.Value.Errors.Count > 0)
                .Select(e => new
                {
                    Field = e.Key,
                    Errors =  e.Value!.Errors.Select(x => x.ErrorMessage).ToArray()
                }).ToList();

        
        var errorString = string.Join("; ", errors.Select(e => $"{e.Field}: {string.Join(", ", e.Errors)}"));

                return new BadRequestObjectResult(new
                {
                    Message = "Validations Failed Errors.",
                    Errors = errorString
                });
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

