using System.Text;
using DentalWebApi.Models;
using DentalWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// *Adds basic JWT authentication scheme
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.SaveToken = true;
    // * Switch this to false if under development
    opts.RequireHttpsMetadata = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        // Todo Have Jwt settings key in a secrets management system (EX: Azure key vaults)
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<IAuthService, AuthService>();

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
    // Todo uncomment below once a set direction for JWTs
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
