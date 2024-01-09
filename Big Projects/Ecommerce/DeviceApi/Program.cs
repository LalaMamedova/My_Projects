using DeviceApi;

var builder = WebApplication.CreateBuilder(args);

Startup startup = new(builder.Configuration);
startup.AddServices(builder.Services);

var app = builder.Build();
startup.StartupApp(app);


app.Run();

