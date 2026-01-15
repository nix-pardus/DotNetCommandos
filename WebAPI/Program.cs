using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Services;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Domain.ValueObjects.Enums;
using ServiceCenter.Infrascructure.DataAccess;
using ServiceCenter.Infrascructure.DataAccess.Repositories;
using ServiceCenter.Infrascructure.DataAccess.Specifications;
using System.Data;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy => policy
        .WithOrigins("https://localhost:7131", "http://localhost:5278")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        //Убрал преобразование enum из числа в строку, чтоб в UI не делать обратно из строки в число
        //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IAssignmentRepository, EFAssignmentRepository>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();

builder.Services.AddScoped<IClientRepository, EFClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IScheduleRepository, EFScheduleRepository>();
builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddScoped<IScheduleExceptionRepository, EFScheduleExceptionRepository>();
builder.Services.AddScoped<IScheduleExceptionService, ScheduleExceptionService>();

builder.Services.AddScoped(typeof(IFilterBuilder<>), typeof(FilterBuilder<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };
        
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                {
                    context.Token = token.Substring(7);
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    // тут политика для мастера
    options.AddPolicy("Master", policy =>
    policy.RequireRole(RoleType.Master.ToString(), RoleType.Administrator.ToString()));

    // для оператора
    options.AddPolicy("Operator", policy =>
        policy.RequireRole(RoleType.Operator.ToString(), RoleType.Administrator.ToString()));
    
    // для всех
    options.AddPolicy("All", policy =>
        policy.RequireRole(RoleType.Operator.ToString(), RoleType.Master.ToString(), RoleType.Administrator.ToString()));
    
    // для администратора
    options.AddPolicy("Administrator", policy =>
        policy.RequireRole(RoleType.Administrator.ToString()));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt=> 
{
    opt.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString(DateTime.Today.ToString("yyyy-MM-dd"))
    });
    opt.MapType<TimeOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "time",
        Example = new OpenApiString(DateTime.Today.ToString("HH-mm-ss.ff"))
    });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API v1");

        options.OAuthClientId("swagger-ui");
        options.OAuthAppName("Swagger UI");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowBlazorClient");

app.MapControllers();

app.Run();
