
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Sprint1_C_.Application.Services;
using Sprint1_C_.Infrastructure.Data;
using Sprint1_C_.Mappings;

namespace Sprint1_C_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception("A vari�vel de ambiente DEFAULT_CONNECTION n�o est� definida.");

                options.UseOracle(connectionString);
            });




            builder.Services.AddScoped<MotoService>();
            builder.Services.AddScoped<FilialService>();
            builder.Services.AddScoped<PatioService>();
            builder.Services.AddScoped<UsuarioService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers()
                .AddJsonOptions(opt => {
                                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

            app.Run();
        }
    }
}
