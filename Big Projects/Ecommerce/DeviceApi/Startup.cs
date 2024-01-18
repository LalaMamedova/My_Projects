using DeviceApp.Cache;
using DeviceApp.Repo.Classes;
using DeviceApp.Repo.General;
using DeviceApp.Repo.Interface;
using EcommerceLib.Context;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceServer.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using UserApi.Middleware;
using UserApi.Services.JWTServices.Classes;
using UserApi.Services.JWTServices.Interfaces;

namespace DeviceApi;

public class Startup
{
    public IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private void BigServices(IServiceCollection services)
    {
        services.AddCors(ops => ops.AddPolicy("AllowAnyOrigins",
           builder => builder
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithExposedHeaders("Content-Disposition")
         ));

        services
            .AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 7;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
            })
           .AddEntityFrameworkStores<UserDbContext>()
           .AddDefaultTokenProviders();


        services.AddAuthentication(
            options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _configuration["JWT:Issuer"],
                ValidAudience = _configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:TokenKey"])),
            };
        });

        services.AddSwaggerGen(ops =>
        {
            ops.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
            });

            ops.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public void AddServices(IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<AutoMapperConfig>();
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

     
        services.AddStackExchangeRedisCache(option =>
        {
            option.Configuration = _configuration["Redis"];
        });


        BigServices(services);

        services.Configure<RouteOptions>(options =>{options.ConstraintMap.Add("id", typeof(GuidRouteConstraint));});
        services.AddDbContext<DeviceDbContext>(options =>{options.UseSqlServer(_configuration["DbConnection:DeviceDbConnection"]);});
        services.AddDbContext<UserDbContext>(options =>{options.UseSqlServer(_configuration["DbConnection:DeviceDbConnection"]);});

        services.AddScoped(typeof(IGenericRepository<,>), typeof(Repository<,>));

        services.AddScoped<ProductRepository>();
        services.AddScoped<BrandRepository>();
        services.AddScoped<CategoryRepository>();
        services.AddScoped<SubCategoryRepository>();
        services.AddScoped<ReviewRepository>();
        services.AddScoped<LikedProductRepository>();
        services.AddScoped<PurchasedProductRepository>();
        services.AddScoped<CharacteristicRepository>();


        services.AddScoped<ICreateTokenService, CreateTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ITokenManager, TokenManager>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddSingleton<ICacheService, CacheService>();  
    }

    public void StartupApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAnyOrigins");
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<RefreshTokenMiddleware>();

        app.MapControllers();


    }
}
