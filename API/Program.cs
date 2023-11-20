using System.Text.Encodings.Web;
using API;
using Application;
using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments($"{builder.Environment.ContentRootPath}/API.xml");
    options.IncludeXmlComments($"{builder.Environment.ContentRootPath}/Application.xml");
});

builder.Services.AddRequestHandlers();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping);

builder.Services.AddErrorHandling();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    await scope.ServiceProvider.GetRequiredService<DataContext>().Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}

app.UseStaticFiles();

app.UseErrorHandling();

app.UseHttpsRedirection();

app.MapControllers()
    .WithOpenApi();

app.Run();