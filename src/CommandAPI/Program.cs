using Microsoft.EntityFrameworkCore;
using CommandAPI.Data;
using Npgsql;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

NpgsqlConnectionStringBuilder connectionStringBuilder = new NpgsqlConnectionStringBuilder();
connectionStringBuilder.ConnectionString = builder.Configuration.GetConnectionString("PostgreSqlConnection");
connectionStringBuilder.Username = builder.Configuration["UserID"];
connectionStringBuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommandContext>(option => option.UseNpgsql(connectionStringBuilder.ConnectionString));
var app = builder.Build();

app.MapControllers();
app.Run();
