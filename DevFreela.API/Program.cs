using DevFreela.API.Filters;
using DevFreela.API.Model;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Services.Implementations;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.Validators;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/configuration/?view=aspnetcore-7.0
builder.Services.Configure<OpeningTimeOption>
    (builder.Configuration.GetSection(OpeningTimeOption.OpeningTime));

//var jwtOption = builder.Services.Configure<JwtOption>
//    (builder.Configuration.GetSection(JwtOption.JWT));

var jwtOption = builder.Configuration.GetSection(JwtOption.JWT).Get<JwtOption>();

var connectionString = builder.Configuration.GetConnectionString("DevFreelaCs");

builder.Services.AddDbContext<DevFreelaDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();



//https://github.com/FluentValidation/FluentValidation/issues/1965
builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));
//    .AddFluentValidation( fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


//https://stackoverflow.com/questions/72261822/how-to-add-mediatr-in-net-6
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DevFreela.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Autorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header usando o esquema Bearer."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id ="Bearer"
                }

            },
            new string[]{}
        }

    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,

           ValidIssuer = jwtOption.Issuer,
           ValidAudience = jwtOption.Audience,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.Key))
       };
   });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DevFreela.API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
