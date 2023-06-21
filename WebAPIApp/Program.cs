using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using WebAPIBusiness.Implementacion;
using WebAPIBusiness.Interface;
using WebAPIDataLayer.Implementacion;
using WebAPIDataLayer.Interfaces;
using WebApiEntities.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
}
);
//Se agrega el dbcontext, en este caso a una base SqLite
builder.Services.AddDbContext<DbUsersContext>(optionsBuilder => optionsBuilder.UseSqlite("Data Source=.\\DB\\DbUsers.db"));
//se inyectan las clases de lógica y repositorio con sus respectivas interfaces
builder.Services.AddTransient<IUserBL, UserBL>();
builder.Services.AddTransient<IUserDAL, UserDAL>();

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
