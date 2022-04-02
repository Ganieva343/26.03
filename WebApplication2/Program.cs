var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var users = new List<User>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/register", (User user) =>
{
    users.Add(user);
    return "Hello";
});

app.MapPost("/login", (Loginbody body) =>
{
    var user = users.Find(u => u.login == body.login);
    if (user == null)
        return "Пользователь не найден";
    if (user.password == body.password)
        return "Вход выполнен";
    else
        return "Пароль неверный";
        
});

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


class User
{
    public int id { get; set; }
    public string login { get; set; }
    public string password { get; set; }

}

class Employee : User
{
    public int salary { get; set; }
}

class Client : User
{
    public int discount { get; set; }
}
class Loginbody
{
    public string login { get; set; }
    public string password { get; set; }
}
public enum role
{
    admin, user, privilegesuser, moderator
}