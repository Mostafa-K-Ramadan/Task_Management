using API.Extensions;
using API.Services;
using Application.Task;
using MediatR;
using Infrastructure.Security;
using Application.Interfaces;
using Application.Core;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
            .AddFluentValidation( _config => 
                {
                    _config.RegisterValidatorsFromAssemblyContaining<TaskValidator>();
                }
            )
            .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
            // CORS Policy
builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(typeof(GetAll).Assembly);

builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);

// Add all database services to our program
builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddScoped<IToken, AccountToken>();

builder.Services.AddScoped<IUserAccessor, UserAccessor>();

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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers(); 

app.Run();
