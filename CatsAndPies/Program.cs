
using CatsAndPies.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CatsAndPies
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<AppDbContext>(
                options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConntection"));
                });

            var key = Encoding.ASCII.GetBytes("ThisIsMySuperSecretKeyForJwtToken1234567890");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            // ���������� CORS � �������
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500") // ��������� ������ URL ���������
                            .WithOrigins("http://localhost:3000")
                            .WithOrigins("https://geckonda.github.io")
                            .WithOrigins("https://maximbri.github.io")
                          .AllowAnyHeader()                   // ��������� ����� ���������
                          .AllowAnyMethod()                   // ��������� ����� HTTP ������
                          .AllowCredentials();                // ��������� �������� ������� ������
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // ��������� swagger ����� ����� ���� ���������� ������ � [Authorize]
            builder.Services.AddSwaggerGen(options =>
            {
                //��������� ��������� ��� �������� �������
                options.EnableAnnotations();
                // ��������� ��������� JWT-�������
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "������� JWT ����� ��������� �������: Bearer {��� �����}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.InitialiseRepositories();
            builder.Services.InitialiseServices();
            builder.Services.InitialiseMappers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
           

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication();
            app.UseAuthorization();



            if (!app.Environment.IsDevelopment())
            {
                app.UseWhen(context => context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
                {
                    appBuilder.Use(async (context, next) =>
                    {
                        var authToken = context.Request.Cookies["authToken"];
                        if (string.IsNullOrEmpty(authToken))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }

                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(authToken);
                        if (jwtToken.ValidTo < DateTime.UtcNow)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized; // ����� �����
                            return;
                        }
                        var roles = jwtToken.Claims.Where(claim => claim.Type == "role").Select(claim => claim.Value).ToList();
                        //if (!context.User.Identity.IsAuthenticated || !context.User.IsInRole("Admin"))
                        if (!roles.Contains("Admin"))
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return;
                        }

                        await next.Invoke();
                    });
                });

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CatsAndPies V1");

                    // ��������� Swagger UI �� ��������� �����������
                    c.RoutePrefix = "swagger"; // ���� ���������� �������� ���� � Swagger
                });
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.MapControllers();
            app.UseStaticFiles();


            app.Run();
        }
    }
}
