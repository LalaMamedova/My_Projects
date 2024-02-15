using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using QuizLib.Configurator;
using QuizLib.DatabaseContext;
using QuizLib.Model.User;
using System.Linq.Expressions;
using System.Xml;


namespace QuizApp.Services;

public class UserManager: IMongoDatabaseContext
{
    private MongoClient _client;
    private AppUserConfigurator _userConfigurator;

    public IMongoDatabase MongoDatabase { get; set; }
    public IMongoCollection<AppUser> Users { get; set; }
    public UserManager(AppUserConfigurator userConfigurator)
    {
        _userConfigurator = userConfigurator;

        var config = new ConfigurationBuilder()
           .AddUserSecrets<MongoQuizDbContext>()
           .Build();

        var connectionString = config["Mongo:Connection"];
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(settings);

        MongoDatabase = _client.GetDatabase(config["Mongo:DataBaseName"]);
        Users = MongoDatabase.GetCollection<AppUser>(config["Mongo:UserCollectionName"]);

        var keys = Builders<AppUser>.IndexKeys.Ascending(x => x.Email);
        var indexOptions = new CreateIndexOptions { Unique = true };
        var indexModel = new CreateIndexModel<AppUser>(keys, indexOptions);
        Users.Indexes.CreateOne(indexModel);
    }

    public async Task CreateAsync(AppUser user,string role)
    {
        var existUser = await Users.FindAsync(x=>x.Id == user.Id);
        if (await existUser.FirstOrDefaultAsync() != null)
        {
            throw new Exception("User alredy exist");
        }

        if (_userConfigurator.UniqueUsername)
        {
            var userNameCheck = await FirstAsync(x => x.UserName, user.UserName);

            if (userNameCheck != null)
            {
                throw new InvalidOperationException($"{user.UserName} alredy taken");
            }
        }

        var emailCheck = await FirstAsync(x=>x.Email, user.Email);
        if (emailCheck != null)
        {
            throw new InvalidOperationException($"{user.Email}  alredy taken");
        }

        if(user.Password.Length < _userConfigurator.MinPasswordLength)
        {
            throw new InvalidOperationException($"Password  must has at least {_userConfigurator.MinPasswordLength} characters ");
        }

        string hashedPassword = HashPassword(user);

        user.Password = hashedPassword;
        user.Id = Guid.NewGuid();
        user.Roles = new List<string>([role]);
        user.UserQuizes = new();

        await Users.InsertOneAsync(user);
    }

    public async Task<AppUser> FirstAsync<T>(Expression<Func<AppUser,T>> expression, T value) 
    {
        var filter = Builders<AppUser>.Filter.Eq(expression, value);
        var result = await Users.Find(filter).FirstOrDefaultAsync();
        return result;

    }
    public async Task<AppUser?> FindByEmailAsync(string email)
    {
        var user = await FirstAsync(x=>x.Email,email);

        if (user == null)
        {
            throw new InvalidOperationException("User with the specified email does not exist.");
        }
        return user;
    }

    public async Task<AppUser?> FindByIdAsync(string id)
    {
        var user = await FirstAsync(x => x.Id.ToString(), id);

        if (user == null)
        {
            throw new InvalidOperationException("User don't exist.");
        }
        return user;
    }

    public string HashPassword(AppUser user)
    {
        var passwordHasher = new PasswordHasher<AppUser>();
        
        string hashedPassword = passwordHasher.HashPassword(user, user.Password);
        return hashedPassword;
    }
    public async Task AddRoleAsync(AppUser user,string role)
    {
        user.Roles.Add(role);
        await Users.ReplaceOneAsync(x => x.Id == user.Id,user);
    }
    public async Task<ICollection<string>> GetRoleAsync(Guid id)
    {
        var existUsers = await Users.FindAsync(x => x.Id == id);
        var user = await existUsers.FirstAsync();
        return user.Roles;
    }

    public async Task<AppUser> UpdateAsync(AppUser user)
    {
        await Users.ReplaceOneAsync(x => x.Id == user.Id, user);
        return user;
    }
}
