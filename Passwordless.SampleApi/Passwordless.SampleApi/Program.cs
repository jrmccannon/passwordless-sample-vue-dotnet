using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Passwordless.SampleApi.Auth;
using Passwordless.SampleApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(r =>
{
    r.AddPolicy("wildcard", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth:Audience"],
            ValidIssuer = builder.Configuration["Auth:Issuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Auth:Secret"]))
        };
    });

builder.Services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));

builder.Services.AddTransient<IPenguinRepository, PenguinRepository>();
builder.Services.AddTransient<ITokenManager, TokenManager>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();