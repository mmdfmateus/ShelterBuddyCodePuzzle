using ShelterBuddy.CodePuzzle.Api.Models.Validations;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRepository<Animal, Guid>, AnimalRepository>();
builder.Services.AddScoped<AnimalModelValidator>();

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