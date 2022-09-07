using Microsoft.EntityFrameworkCore;
using CommandAPI.Data;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<ICommandAPIRepo, SqlCommandAPIRepo>();
builder.Services.AddDbContext<CommandContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")));
var app = builder.Build();

app.MapControllers();
app.Run();
