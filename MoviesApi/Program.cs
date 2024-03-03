using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MoviesApi.Models;

namespace MoviesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {


                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors();
            builder.Services.AddSwaggerGen(options =>
            {


            options.AddSecurityDefinition("Bearer",new  OpenApiSecurityScheme
                {
                Name="Autorization",
                Type=SecuritySchemeType.ApiKey,
                Scheme="Bearer",
                BearerFormat="JWT",
                In=ParameterLocation.Header,
                Description="Enter your jwt key"
                });




                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference= new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"

                            },
                             Name= "Bearer",
                             In=ParameterLocation.Header

                        },
                        new List<string>()

                    }

                });


            });

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(c=>c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}