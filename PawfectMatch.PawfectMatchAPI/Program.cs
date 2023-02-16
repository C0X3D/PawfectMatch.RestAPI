using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PawfectMatch.DatabaseContextManager;
using PawfectMatch.DatabaseRepositoryManager;
using PawfectMatch.DatabaseRepositoryManager.Interface;
using PawfectMatch.ExceptionHandling.Middleware;
using PawfectMatch.JwtIssuer;
using PawfectMatch.JwtIssuer.Interface;
using System.Net;
using System.Text;

internal class Program
{
   
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<ExceptionMiddleware, ExceptionMiddleware>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "JWTToken_Auth_API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
        });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
              b => b.MigrationsAssembly("PawfectMatch.DatabaseContextManager")),ServiceLifetime.Singleton);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["JwtIssuerOptions:Issuer"],
                ValidAudience = builder.Configuration["JwtIssuerOptions:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["JwtIssuerOptions:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });

        IJwtConfiguration jwtConfiguration = new JwtConfiguration(
            builder.Configuration["JwtIssuerOptions:Issuer"], 
            builder.Configuration["JwtIssuerOptions:Audience"],
            builder.Configuration["JwtIssuerOptions:Key"]);

        IJwtIssuerManager jwtIssuerManager = new JwtIssuerManager(jwtConfiguration);

        builder.Services.AddSingleton<IJwtIssuerManager>(jwtIssuerManager);
        builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddMvc();

        

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseExceptionHandler(
            builder =>
            {
                builder.Run(
                  async context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                      context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                      
                      var error = context.Features.Get<IExceptionHandlerFeature>();                      
                      if (error != null)
                      {
                          await context.Response.WriteAsync("cacat:" + error.Error.Message).ConfigureAwait(false);
                      }
                  });
            });

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseCors(cpb =>
        {
            cpb.AllowAnyHeader();
            cpb.AllowAnyMethod();
            cpb.AllowAnyOrigin();
        });
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.Run();
    }
}