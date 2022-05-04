using MeetUpBack.Data;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBMEETUP"));
});
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IMeetUpRepository, MeetUpRepository>();
builder.Services.AddScoped<IMappingHelper, MappingHelper>();
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
