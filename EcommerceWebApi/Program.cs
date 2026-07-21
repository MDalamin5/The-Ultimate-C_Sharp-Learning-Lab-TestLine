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


// get product
var products = new List<Product>()
{
    new Product("Laptop", 13433.01m),
    new Product("Mobile", 43000.33m)
};

app.MapGet("/getProduct", () =>
{
    return Results.Ok(products);
});

app.Run();
public record Product(string Name, decimal Price);