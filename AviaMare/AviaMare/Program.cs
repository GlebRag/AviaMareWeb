using AviaMare.Data;
using AviaMare.Data.Interface.Repositories;
using AviaMare.Data.Repositories;
using AviaMare.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Логирование в консоль
    .WriteTo.File("logs/latest.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 1) // Логирование в файл
    .MinimumLevel.Information() // Уровень логирования
    .CreateLogger();

// Встраиваем Serilog в ASP.NET Core
builder.Host.UseSerilog();

builder.Services
    .AddAuthentication(AuthService.AUTH_TYPE_KEY)
    .AddCookie(AuthService.AUTH_TYPE_KEY, config =>
    {
        config.LoginPath = "/Auth/Login";
        config.AccessDeniedPath = "/Home/Forbidden";
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDbContext>(x => x.UseSqlServer(WebDbContext.CONNECTION_STRING));

builder.Services.AddScoped<IUserRepositryReal, UserRepository>();
builder.Services.AddScoped<ITicketRepositoryReal, TicketRepository>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<EnumHelper>();
builder.Services.AddScoped<UserService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var seed = new Seed();
seed.Fill(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Who Am I?
app.UseAuthorization(); // May I?

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Log.Information("Application starting...");
try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application crashed!");
}
finally
{
    Log.CloseAndFlush(); // Завершаем логирование корректно
}
