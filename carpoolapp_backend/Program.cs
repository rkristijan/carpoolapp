using carpoolapp_backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddCors(options => {
//     options.AddDefaultPolicy(policyBuilder => {
//         policyBuilder.AllowAnyMethod()
//             .AllowAnyHeader().AllowCredentials().WithOrigins("https://localhost:3000", "http://localhost:3000");
//
//     });
// });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

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

app.UseCors(policyBuilder => {
    policyBuilder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowCredentials().AllowAnyHeader();
});

app.Run();
