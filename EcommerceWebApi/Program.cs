using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.SignalR;

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

/*
    ======================================
    || Product Category CRUD Operations ||
    ======================================
*/

List<Category> categories = new List<Category>();

app.MapPost("api/v1/categories", () =>
{
    var newCategory = new Category
    {
      CategoryId =  Guid.NewGuid(),
      Name = "Laptop",
      Description = "This is Laptop Descriptions",
      CreatedAt = DateTime.UtcNow,
    };

    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}",newCategory);
});

app.Run();
public record Product(string Name, decimal Price);

// CRUD api via product category
// CREATE: uri-> POST: api/v1/category
// READ: uri -> GET: api/v1/category/{id}
// PUT: uri -> PUT: api/v1/category/{id}
// DELETE: uri -> DELETE api/v1/category/{id}

public record Category
{
    public Guid CategoryId {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime CreatedAt {get; set;}
}