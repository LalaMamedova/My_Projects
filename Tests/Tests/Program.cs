using TestsApp.Repository.Generic;
using TestsApp.Repository.İnterfaces;
using TestsApp.Repository.ModelReposotry;
using TestsApp.Services.HttpRequests;
using TestsApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<TestRequests>();
builder.Services.AddHttpClient<UserRequest>();

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddLogging();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(500); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorComponents();
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddScoped<TestRepository>();
builder.Services.AddScoped<UserRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
