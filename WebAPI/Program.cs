using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ServiceCenter.Application.Interfaces;
using ServiceCenter.Application.Services;
using ServiceCenter.Domain.Interfaces;
using ServiceCenter.Infrascructure.DataAccess;
using ServiceCenter.Infrascructure.DataAccess.Repositories;
using ServiceCenter.Infrascructure.DataAccess.Specifications;

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
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowBlazorClient");

app.MapControllers();

app.Run();
