using DentalWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Connects to DB
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DentalContext>(options => options.UseSqlServer(dbConnectionString));
// builder.Services.AddDbContext<DentalContext>(options => options.UseSqlServer(dbConnectionString));

// Identity and Authorization
// builder.Services.AddDefaultIdentity<User>(options =>
// *As of .Net 8 we can set AddIdentityApiEndpoints to secure api endpoints with configuration
// *Below config will only work if trying to create new user but requirements are not met in _userManager
builder.Services.AddIdentityApiEndpoints<User>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;
    })
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<DentalContext>();

// * .Net 7 or below configuration if not using AddIdentityApiEndpoints
// builder.Services.Configure<IdentityOptions>(options =>
// {
//     // Default Password settings.
//     options.Password.RequireDigit = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = true;
//     options.Password.RequireUppercase = true;
//     options.Password.RequiredLength = 6;
//     options.Password.RequiredUniqueChars = 1;
// });

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Basic Title of Dental App",
        Description = "Basic Description of Dental App",
    });
});

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

// app.MapIdentityApi<User>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
