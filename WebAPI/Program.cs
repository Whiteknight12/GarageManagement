using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Authorization;
using WebAPI.Data;
using WebAPI.Middlewares;
using WebAPI.Service;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// allow android emulator to access the API
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5142);
    serverOptions.ListenLocalhost(7228, listenOptions =>
    {
        listenOptions.UseHttps(); 
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtsecurityscheme = new OpenApiSecurityScheme
    {
        BearerFormat="JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type=SecuritySchemeType.Http,
        Scheme=JwtBearerDefaults.AuthenticationScheme,
        Description="Enter your JWT Access Token",
        Reference=new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition("Bearer", jwtsecurityscheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtsecurityscheme, Array.Empty<string>()  }
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience=builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Key"])),
        ValidateIssuer=true,
        ValidateAudience= true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtService>();
//builder.Services.AddSingleton<IAuthorizationPolicyProvider, DynamicAuthorizationPolicyProvider>();
//builder.Services.AddScoped<IAuthorizationHandler, DynamicPermissionHandler>();
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.SetMinimumLevel(LogLevel.Information);
});
//quartz implementation
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Job Key
    var jobKey = new JobKey("MonthlyJob");

    // Đăng ký job
    q.AddJob<MonthlyJobService>(opts => opts.WithIdentity(jobKey));

    // Trigger 1: Đầu tháng lúc 00:00 ngày 1
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("FirstDayTrigger")
        .WithCronSchedule("0 0 0 1 * ?")); // 00:00 ngày 1 hàng tháng

    // Trigger 2: Cuối tháng lúc 23:59
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("LastDayTrigger")
        .WithCronSchedule("0 59 23 L * ?")); // 23:59 ngày cuối tháng
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Should be used in production environment, for testing purposes, UseHttpsRedirection is commented out
//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UserJwtLogging();
app.MapControllers();
app.Run();
//wewew