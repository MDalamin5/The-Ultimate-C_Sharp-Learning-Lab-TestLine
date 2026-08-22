var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
   return  response;
});

app.Run();

