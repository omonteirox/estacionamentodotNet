using estacionamento.Data;
using estacionamento.Services;
using estacionamento.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

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
builder.Services.AddControllers()
           .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
           });
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<EstablishmentService>();
builder.Services.AddScoped<InOutEstablishmentService>();
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
app.MapControllers();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
