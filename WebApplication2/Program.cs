using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var users = new List<User>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/register", (Client user) =>
{
// В этой точке непосредственно начинается работа с Entity Framework

    // Создать объект контекста
    SampleContext context = new SampleContext();

    // Вставить данные в таблицу Customers с помощью LINQ
    context.Clients.Add(user);

    // Сохранить изменения в БД
    context.SaveChanges();
    return "Hello";
});
app.MapPost("/admin", (Employee employee)=>
{// В этой точке непосредственно начинается работа с Entity Framework

    // Создать объект контекста
    SampleContext context = new SampleContext();

    // Вставить данные в таблицу Customers с помощью LINQ
    context.Employees.Add(employee);

    // Сохранить изменения в БД
    context.SaveChanges();
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


public class User
{
    public int id { get; set; }
    public string login { get; set; }
    public string password { get; set; }

}
public class SampleContext : DbContext
{
    // Имя будущей базы данных можно указать через
    // вызов конструктора базового класса
    public SampleContext() : base("MyOrder")
    { }

    // Отражение таблиц базы данных на свойства с типом DbSet
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Client> Clients { get; set; }
}

public class Employee : User
{
    public int salary { get; set; }
}

public class Client : User
{
    public int discount { get; set; }
}
public class Loginbody
{
    public string login { get; set; }
    public string password { get; set; }
}
public enum role
{
    admin, user, privilegesuser, moderator
}