using Microsoft.EntityFrameworkCore;
using UserApi;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new(builder.Configuration);
startup.AddServices(builder.Services);


var app = builder.Build();
startup.Start(app);

app.Run();

