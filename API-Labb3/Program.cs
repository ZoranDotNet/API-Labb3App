using API_Labb3.Data;
using API_Labb3.Endpoints;
using API_Labb3.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Labb 3 API",
        Description = "Random people with hobbies",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Zoran Matovic, NET23",
            Email = "zoran.matovic.net23@edu.edugrade.se"
        }
    });
});
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddOutputCache();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IHobbiesRepository, HobbiesRepository>();
builder.Services.AddScoped<ILinksRepository, LinksRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseOutputCache();


app.MapGroup("/api/people").MapPeople();
app.MapGroup("/api/person/{personId:int}/hobbies").MapHobbies();
app.MapGroup("/api/hobby/{hobbyId:int}/links").MapLinks();
app.MapGroup("/api/hobbies").MapHobbySearch();

app.Run();

