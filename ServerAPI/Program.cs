using ServerAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("SqlConnection");
builder.Services.AddDbContext<NewChatSedContext>(option => option.UseSqlServer(connection));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
