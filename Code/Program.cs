using CodeAPI.Models;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add ICodeContentFactory dependency injection
builder.Services.AddScoped<ICodeContentFactory, CodeContentFactory>();

// Register the custom serializer for JsonElement
BsonSerializer.RegisterSerializer(new JsonElementSerializer());

// Adding Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors();
app.UseRouting();
app.Run();