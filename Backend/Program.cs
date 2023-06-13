using Backend.Data;
using Backend.Data.Seed;
using Backend.Repository.BaseRepository;
using Backend.Repository.ScheduleRepository;
using Backend.Service.ScheduleService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var services = builder.Services;
services.AddCors(o =>
    o.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()));

var connectionString = builder
    .Configuration.GetConnectionString("MyDB");
services.AddDbContext<DataContext>
(option =>
{
    option.UseSqlServer(connectionString);
}, ServiceLifetime.Transient);


services.AddScoped<IScheduleRepository, ScheduleRepository>();
services.AddScoped<IScheduleService, ScheduleService>();
services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]))
        };
    });
builder.Services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger Mock Project 2",
        Version = "v1",
        Description = "An ASP.NET Core Web API for Mock Project 2",
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Authorization header using the Bearer scheme. 
                Enter 'Bearer' [space] and then your token in the text input below.
                Example: 'Bearer 12345abcdef",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
                {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
                },
                In = ParameterLocation.Header,
            },
            new List<string>()
            }
        });
});

var app = builder.Build();

using var scope = app.Services.CreateScope();
var servicesProvider = scope.ServiceProvider;
try
{
    var context = servicesProvider.GetRequiredService<DataContext>();
    context.Database.Migrate();
    SeedData.SeedUsers(context);
}
catch (Exception e)
{
    var logger = servicesProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(e, "Migration failed");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
