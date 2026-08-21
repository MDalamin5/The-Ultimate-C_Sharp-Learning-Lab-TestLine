var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

// Build the API
app.MapGet("/", () => {
   return  "Welcome to The MapGet APi.";
});

app.Run();

