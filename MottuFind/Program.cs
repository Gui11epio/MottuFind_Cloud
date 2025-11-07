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
                // Tenta ler primeiro a variável usada no Azure (CUSTOMCONNSTR_)
                var connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DEFAULT_CONNECTION");

                // Se não achar, tenta a variável local (DEFAULT_CONNECTION)
                if (string.IsNullOrWhiteSpace(connectionString))
                    connectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION");

                // E por fim, tenta o appsettings.json local
                if (string.IsNullOrWhiteSpace(connectionString))
                    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                // Se ainda estiver vazio, lança uma exceção clara
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new Exception("❌ Nenhuma connection string foi encontrada. Verifique as variáveis de ambiente ou o appsettings.json.");

                // Usa o SQL Server com a connection string encontrada
                options.UseSqlServer(connectionString);
            });

            // Registro dos serviços
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

            // Habilita Swagger sempre, mesmo em Production (somente para testes)
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MottuFind API V1");
            });

            // Remova o HTTPS redirection para rodar no ACI via HTTP
            // app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
