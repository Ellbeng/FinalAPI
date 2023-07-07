using APIFinal.DBContext;
using APIFinal.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IUserService, UserService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
