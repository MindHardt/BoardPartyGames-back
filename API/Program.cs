using System.Text.Encodings.Web;
using API;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRequestHandlers();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

builder.Services.AddErrorHandling();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandling();

app.UseHttpsRedirection();

app.MapControllers()
    .WithOpenApi();

app.Run();