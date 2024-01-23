using EcommerceLib.Context;
using EcommerceLib.Models.UserModel.JWT;
using EcommerceLib.Models.UserModel.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using UserApi.Middleware;
using UserApi.Services;
using UserApi.Services.JWTServices.Classes;
using UserApi.Services.JWTServices.Interfaces;


namespace UserApi;

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
       

    }


    public void AddServices(IServiceCollection services)
    {
        services.AddAuthorization(ops =>
        {
            ops.AddPolicy(UserRoles.Admin, policy => policy.RequireClaim(UserRoles.Admin, "true"));
            ops.AddPolicy(UserRoles.User, policy => policy.RequireClaim(UserRoles.User, "true"));
            ops.AddPolicy(UserRoles.SuperAdmin, policy => policy.RequireClaim(UserRoles.SuperAdmin, "true"));
        });
    
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        BigServices(services);
        services.AddControllers();

        services.AddDbContext<UserDbContext>(options =>
        {
            options.UseSqlServer(_configuration["DbConnection:DeviceDbConnection"]);
        });

       
        services.AddScoped<ICreateTokenService, CreateTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<ITokenManager, TokenManager>();
        services.AddScoped<ILoginService, LoginService>();
        //services.AddScoped<IMailServices, EmailSenderService>();

    }

    public void Start(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAnyOrigins");

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();


    }
}
