using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;
using Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var originsToAllow = "_originsToAllow";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();
builder.Services.Configure<DatabaseConnectionModel>(configuration.GetSection("DatabaseSetting"));
builder.Services.Configure<JWTConfigModel>(configuration.GetSection("JWT"));

builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>();

builder.Services.AddSingleton<IUserRepositories, UserRepository>();

builder.Services.AddSingleton<IUserLoginRepository, UserLoginRepository>();

builder.Services.AddSingleton<IUserServices, UserService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: originsToAllow, policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


builder.Services.AddCors(options => { options.AddPolicy(name: originsToAllow, policy => { policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); }); });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
   options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters()
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value)),
           ClockSkew = TimeSpan.Zero

       };
   });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(originsToAllow);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
