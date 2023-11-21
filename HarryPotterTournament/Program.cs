using Entities;
using Entities.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using RepositoryContracts;
using ServiceContracts.Interfaces;
using ServiceContracts.Interfaces.DuelInterfaces;
using ServiceContracts.Interfaces.TournamentInterfaces;
using ServiceContracts.Interfaces.UserInterfaces;
using Services;
using Services.DuelServices;
using Services.TournamentServices;
using Services.UserServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IDuelRepository , DuelRepository>();
builder.Services.AddScoped<IDuelGetterService, DuelGetterService>();
builder.Services.AddScoped<IDuelAdderService, DuelAdderService>();
builder.Services.AddScoped<IDuelDeleterService, DuelDeleterService>();
builder.Services.AddScoped<IDuelUpdaterService, DuelUpdaterService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGetterService, UserGetterService>();
builder.Services.AddScoped<IUserUpdaterService, UserUpdaterService>();

builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITournamentGetterService, TournamentGetterService>();
builder.Services.AddScoped<ITournamentAdderService, TournamentAdderService>();
builder.Services.AddScoped<ITournamentUpdaterService, TournamentUpdaterService>();
builder.Services.AddScoped<ITournamentDeleterService, TournamentDeleterService>();


builder.Services.AddIdentity<User, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]);

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateAudience = false,
    ValidAudience = builder.Configuration["JwtSettings:Audience"],
    ValidateIssuer = false,
    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
    RequireExpirationTime = true, //update when add refresh token
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key)
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = tokenValidationParameters;
});

builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
