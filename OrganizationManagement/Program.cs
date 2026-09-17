using Microsoft.EntityFrameworkCore;
using OrganizationManagement.Data;
using OrganizationManagement.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options=>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapGet("/", () =>
{
    var sayAlive = new
    {
        message = "I'm Alive",
        status = "Okay"
    };
    return Results.Ok(sayAlive);
});
app.MapControllers();

app.Run();

