
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using QuizApp.Repository.Generic;
using QuizApp.Repository.ModelRepository;
using QuizApp.Services.JwtServices;
using QuizLib.DatabaseContext;
using QuizLib.Model.User;
using System.Text.Json.Serialization;
using System.Text;
using QuizLib.Configurator;
using QuizApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;

namespace QiuzAPI;

public class Startup
{
    private IConfigurationManager _configurationManager;

    public Startup(IConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public void Build(IServiceCollection Services)
    {
        Services.AddEndpointsApiExplorer();
        Services.AddSwaggerGen();
        Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));


        Services.AddCors(ops => ops.AddPolicy("AllowAnyOrigins",
         builder => builder
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithExposedHeaders("Content-Disposition")
            ));


        //Services.AddHttpClient("GoogleAPIClient", client =>
        //{
        //    client.BaseAddress = new Uri("https://www.googleapis.com/");
        //    client.DefaultRequestHeaders.Add("Authorization", $"Bearer ");
        //});

        Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuers = new List<string>() { _configurationManager["JWT:Issuer"], "accounts.google.com" },
                ValidAudiences = new List<string>() { _configurationManager["JWT:Audience"], _configurationManager["Google:ClientId"] },
                IssuerSigningKeys =  SecretsKey().Result,
            };
        });

        async Task<IEnumerable<SecurityKey>> GetGoogleSigningKeys()
        {
            using var httpClient = new HttpClient();
            var keysUrl = "https://www.googleapis.com/oauth2/v3/certs";
            var keysResponse = await httpClient.GetStringAsync(keysUrl);
            var keysData = JObject.Parse(keysResponse);
            var keys = keysData["keys"];

            List<SecurityKey> securityKeys = new List<SecurityKey>();

            foreach (var key in keys)
            {
                var jsonWebKey = new JsonWebKey(key.ToString());
                securityKeys.Add(jsonWebKey);
            }

            return securityKeys;
        }


        async Task<IEnumerable<SecurityKey>> SecretsKey()
        {
            List<SecurityKey> secretKeys = new List<SecurityKey>();
            var myKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationManager["JWT:IssuerKey"]));
            var googleKey = GetGoogleSigningKeys(); // Ваш метод получения ключей Google
            secretKeys.Add(myKey);
            secretKeys.AddRange(await googleKey); // Добавляем ключи Google
            return secretKeys;
        }


        Services.AddSwaggerGen(ops =>
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


        Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        Services.AddScoped<IMongoDatabaseContext, MongoQuizDbContext>();

        Services.AddScoped<UserManager>();
        Services.AddScoped<QuizRepository>();
        Services.AddScoped<UserRepository>();
        Services.AddScoped<JwtCreate>();

        Services.AddSingleton(provider =>
        {
            return new AppUserConfigurator
            {
                DefaultLockoutTimeSpan = 10,
                MinPasswordLength = 6,
                UniqueUsername = false
            };
        });
        Services.AddSingleton<IConfiguration, ConfigurationManager>();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
