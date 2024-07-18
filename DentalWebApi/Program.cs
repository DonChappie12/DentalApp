using DentalWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Connects to DB
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DentalContext>(options => options.UseSqlServer(dbConnectionString));
// builder.Services.AddDbContext<DentalContext>(options => options.UseSqlServer(dbConnectionString));

// Identity and Authorization
builder.Services.AddDefaultIdentity<User>()
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<DentalContext>();

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DentalContext>();
    context.Database.Migrate();
    // requires using Microsoft.Extensions.Configuration;
    // Set password with the Secret Manager tool.
    // dotnet user-secrets set SeedUserPW <pw>

    // var testUserPw = builder.Configuration.GetValue<string>("SeedUserPW");
    var testUserPw = "testPassword";

    // await DataSeeder.Initialize(services, testUserPw);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
