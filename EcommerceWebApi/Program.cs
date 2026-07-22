using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
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

// Create Category api: 
app.MapPost("api/v1/categories", ([FromBody] Category categoryData) =>
{
    var newCategory = new Category
    {
      CategoryId =  Guid.NewGuid(),
      Name = categoryData.Name,
      Description = categoryData.Description,
      CreatedAt = DateTime.UtcNow,
    };

    categories.Add(newCategory);

    return Results.Created($"/api/categories/{newCategory.CategoryId}",newCategory);


    
});

//READ all category: GET api
app.MapGet("/api/v1/categories", () =>
{
    return Results.Ok(categories);
    
});

// READ a specific one category
app.MapGet("api/v1/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

    if (foundCategory != null)
    {
        return Results.Ok(foundCategory);
    }
    else
        return Results.NotFound("Data not Found");
});

// Delete a product category
app.MapDelete("api/v1/categories/{categoryId:guid}", (Guid categoryId) =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

    if (foundCategory != null)
    {
        categories.Remove(foundCategory);
        return Results.NoContent();
    }
    else
        return Results.NotFound("Data not Found");
});


// Update a product category
app.MapPut("api/v1/categories/{categoryId:guid}", (Guid categoryId, [FromBody] Category categoryData) =>
{
    var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

    if (foundCategory != null)
    {
        foundCategory.Name = categoryData.Name;
        foundCategory.Description = categoryData.Description;

        return Results.NoContent();
    }
    else
        return Results.NotFound("Data not Found");
});

// GET: api/v1/categories
app.MapGet("/api/v2/categories", ([FromQuery] string searchValue) =>
{
    if (!string.IsNullOrEmpty(searchValue))
    {
        var searchCategories = categories.Where(c => c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

        return Results.Ok(searchCategories);
    }
    else
        return Results.Ok(categories);
});

app.Run();
public record Product(string Name, decimal Price);

// CRUD api via product category
// CREATE: uri-> POST: api/v1/category
// READ: uri -> GET: api/v1/category/{id}
// READ-1: uri -> GET: api/v1/category/{id}
// PUT: uri -> PUT: api/v1/category/{id}
// DELETE: uri -> DELETE api/v1/category/{id}

public record Category
{
    public Guid CategoryId {get; set;}
    public string? Name {get; set;}
    public string? Description {get; set;}
    public DateTime CreatedAt {get; set;}
}