using Microsoft.AspNetCore.Authentication.JwtBearer;
using Passwordless.SampleApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddCors(r =>
{
    r.AddPolicy("wildcard", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddTransient<IPenguinRepository, PenguinRepository>();

builder.Services.AddPasswordlessSdk(options =>
{
    options.ApiSecret = builder.Configuration["Passwordless:ApiSecret"];
    options.ApiUrl = builder.Configuration["Passwordless:ServerUrl"];
});

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
app.UseCors("wildcard");

app.MapControllers();

app.Run();