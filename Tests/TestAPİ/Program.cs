using Microsoft.Extensions.DependencyInjection;
using TestsApp.Repository.Generic;
using TestsApp.Repository.İnterfaces;
using TestsApp.Repository.ModelReposotry;
using TestsApp.Services.HttpRequests;
using TestsLib.DbContexts;
using TestsApp.Services.Interfaces;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<TestDbContext>(options => 
{ 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL:Connection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(ops => ops.AddPolicy("AllowAnyOrigins",
         builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithExposedHeaders("Content-Disposition")
));

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<TestRepository>();
builder.Services.AddScoped<UserRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAnyOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
