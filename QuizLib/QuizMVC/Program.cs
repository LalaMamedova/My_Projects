using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using QuizApp.Repository.Generic;
using QuizApp.Repository.ModelRepository;
using QuizLib.DatabaseContext;
using QuizLib.Model.User;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using QuizApp.Services.JwtServices;
using Microsoft.Extensions.Configuration;
using QuizApp.Services.ModelServices;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var mongoIdentityConfig = new MongoDbIdentityConfiguration()
{
    MongoDbSettings = new MongoDbSettings()
    {
        ConnectionString = builder.Configuration["Mongo:Connection"],
        DatabaseName = builder.Configuration["Mongo:DataBaseName"],
    },
    IdentityOptionsAction = option =>
    {
        option.Password.RequireDigit = false;
        option.Password.RequireUppercase = false;
        option.Password.RequiredLength = 5;

        option.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(10);
        option.Lockout.MaxFailedAccessAttempts = 6;
        option.User.RequireUniqueEmail = true; 
    } 
};
builder.Services.ConfigureMongoDbIdentity<AppUser, AppUserRole, Guid>(mongoIdentityConfig)
    .AddUserManager<UserManager<AppUser>>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddRoleManager<RoleManager<AppUserRole>>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(y =>
{

    y.RequireHttpsMetadata = true;
    y.SaveToken = true;
    y.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:IssuerKey"])),
        ClockSkew = TimeSpan.Zero,
    };
});

builder.Services.AddSingleton<IConfiguration,ConfigurationManager>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericMongoRepository<>));
builder.Services.AddScoped<IMongoDbContext,MongoQuizDbContext>();
builder.Services.AddScoped<QuizRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<QuizService>();
builder.Services.AddScoped<JwtCreate>();

builder.Services.AddCors(ops => ops.AddPolicy("AllowAnyOrigins",
         builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithExposedHeaders("Content-Disposition")
));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("AllowAnyOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
