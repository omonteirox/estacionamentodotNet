using estacionamento.Data;
using estacionamento.Models.Enums;
using estacionamento.Services;
using estacionamento.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.MapType<DateTime>(() => new OpenApiSchema
    {
        Type = "string",
        Example = new OpenApiString("30/04/2024-12:00:00.000") 
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    var key = Encoding.UTF8.GetBytes("50ed68fa66ba4bb6a56684ae508e2347");
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "omonteirox",
        ValidAudience = "AudienciaVenenosa",
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("AdminOnly", policy =>
             policy.RequireClaim("TypeUser", TypeUserEnum.ADMIN.ToString()));

    opt.AddPolicy("ManagerOnly", policy =>
        policy.RequireClaim("TypeUser", TypeUserEnum.MANAGER.ToString()));

    opt.AddPolicy("UserOnly", policy =>
        policy.RequireClaim("TypeUser", TypeUserEnum.USER.ToString()));
});

builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
           });
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<EstablishmentService>();
builder.Services.AddScoped<InOutEstablishmentService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(@"Host=localhost;Port=5432;Database=estacionamento;Username=postgres;Password=admin123;");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
