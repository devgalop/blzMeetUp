using System.Text;
using MeetUpBack.Data;
using MeetUpBack.Data.Repositories;
using MeetUpBack.Helpers;
using MeetUpBack.Models.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<JwtConfigModel>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<SmtpConfigModel>(builder.Configuration.GetSection("SmtpConfig"));
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBMEETUP"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:SecretKey"]);
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = true
    };
});
builder.Services.AddScoped<ILocationRepository, LocationRepository>()
                .AddScoped<IMeetUpRepository, MeetUpRepository>()
                .AddScoped<IAuthRepository, AuthRepository>()
                .AddScoped<IMappingHelper, MappingHelper>()
                .AddScoped<ITokenFactoryHelper,TokenFactoryHelper>()
                .AddScoped<IPasswordManagerHelper,PasswordManagerHelper>()
                .AddScoped<IMailHelper,MailHelper>();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
