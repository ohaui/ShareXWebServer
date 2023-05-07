using Microsoft.EntityFrameworkCore;
using ShareXWebClient.Data;
using ShareXWebClient.Interfaces.Service;
using ShareXWebClient.Middleware;
using ShareXWebClient.Utils;
using ShareXWebClient.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ILinkService, LinkService>();
builder.Services.AddTransient<ITextService, TextService>();
builder.Services.AddTransient<IContentService, ContentService>();

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();

var connection = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

var app = builder.Build();

app.UseMiddleware<RedirectMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();