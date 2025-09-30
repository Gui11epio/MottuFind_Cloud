using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MottuFind_C_.Application.Services;
using MottuFind_C_.Domain.Repositories;
using MottuFind_C_.Infrastructure.Repositories;
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

            

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception("A variável de ambiente DEFAULT_CONNECTION não está definida.");

                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });



            builder.Services.AddScoped<IMotoRepository, MotoRepository>();
            builder.Services.AddScoped<MotoService>();

            builder.Services.AddScoped<IFilialRepository, FilialRepository>();
            builder.Services.AddScoped<FilialService>();

            builder.Services.AddScoped<IPatioRepository, PatioRepository>();
            builder.Services.AddScoped<PatioService>();

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<UsuarioService>();

            builder.Services.AddScoped<ILeitorRfidRepository, LeitorRfidRepository>();
            builder.Services.AddScoped<LeitorRfidService>();

            builder.Services.AddScoped<ILeituraRfidRepository, LeituraRfidRepository>();
            builder.Services.AddScoped<LeituraRfidService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers()
                .AddJsonOptions(opt => {
                                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });



            var app = builder.Build();


            
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
