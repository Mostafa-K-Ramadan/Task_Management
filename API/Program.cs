using API.Extensions;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
            // CORS Policy
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

// Add all database services to our program
builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddScoped<IToken, AccountToken>();

// Add all identity services to our program
builder.Services.AddIdentityServices(builder.Configuration);
                            

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); 

app.Run();
