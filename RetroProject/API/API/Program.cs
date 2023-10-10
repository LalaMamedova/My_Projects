using API.Classes;
using API.Classes.TempObj;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});



var tempTechnologies = new TempTechs();
var technologies = tempTechnologies.Technologies; 
var tempUsers = new TempUsers();
var users = tempUsers.users;

app.UseHttpsRedirection();

app.MapGet("/technologies", () =>
{
    return Results.Ok(technologies);
});
app.MapGet("/technologies/{id}", (int id) =>
{
    var techs = technologies.Where(x => x.id == id).First();
   if(techs == null)
        return Results.NotFound("Sorry but we can't found it");
    
    return Results.Ok(techs);
});


app.MapPost("/technologies", (Technology tech) =>
{
    if (!string.IsNullOrEmpty(tech.name) && !string.IsNullOrEmpty(tech.description) && !string.IsNullOrEmpty(tech.images[0]))
    {
        technologies.Add(tech);
        return Results.Ok(tech);
    }
    return Results.NotFound("Please fill all field");

});

app.MapDelete("/technologies/{id}", (int id) =>
{
    var tech = technologies.Find(x => x.id== id);
    if (tech!=null)
    {
        technologies.Remove(tech);
        return Results.Ok(tech);
    }
    return Results.NotFound("Can't find this tech");

});

app.MapPut("/technologies/{id}", (int id,Technology editTech) =>
{
    var tech = technologies.Find(x => x.id == id);
    if (tech != null)
    {
        tech.name = editTech.name;
        tech.year = editTech.year;
        tech.description = editTech.description;
        tech.charname = editTech.charname;
        tech.charvalue = editTech.charvalue;
        tech.interestingfacts = editTech.interestingfacts;
        tech.images = editTech.images;
        tech.type = editTech.type;

        return Results.Ok(tech);
    }
    return Results.NotFound("Can't find this tech");
});


app.MapGet("/users", () =>
{ return Results.Ok(users); });


app.MapGet("/users/{email}", (string email) =>
{
    var user = users.First(x => x.email == email);
    if(user != null)
    {
        return Results.Ok(user);
    }
    return Results.NotFound("User not founded");
});


app.MapPut("/users/{id}", (int id, Users updateUser) =>
{
    var user = users.First(x => x.Id == id);
    if (user != null)
    {
        user.likedTechnology = updateUser.likedTechnology;
        return Results.Ok(user);
    }
    return Results.NotFound("User not founded");
});
app.MapPost("/users", (Users user) =>
{
    var checkUser = users.Where(x => x.email == user.email).FirstOrDefault();
    if (!string.IsNullOrEmpty(user.password) && !string.IsNullOrEmpty(user.email) && !string.IsNullOrEmpty(user.username) && checkUser==null)
    {
        Console.WriteLine(user.username);
        user.Id = users.Last().Id;
        user.Id++;
        users.Add(user);
        return Results.Ok(user);

    }
    else if (checkUser != null) {
        return Results.BadRequest($"{user.email} is alredy registred");
    }
    return Results.NotFound("Please fill all field");

});


app.Run();

