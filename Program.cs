using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "test-ai API";
        options.Theme = ScalarTheme.Purple;
    });
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
