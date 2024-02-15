using QiuzAPI;

var builder = WebApplication.CreateBuilder(args);
Startup start = new(builder.Configuration);
start.Build(builder.Services);

var app = builder.Build();
start.Start(app);
