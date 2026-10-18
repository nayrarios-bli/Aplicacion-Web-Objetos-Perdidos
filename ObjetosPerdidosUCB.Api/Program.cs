


using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Mappings;
using ObjetosPerdidosUCB.Infrastructure.Repositories;

namespace ObjetosPerdidosUCB.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<IReclamoRepository, ReclamoRepository>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(typeof(ReclamoProfile).Assembly);

            //RegistrarValidators de FluentValidator
            builder.Services.AddScoped<ReclamoDtoValidator>();
            builder.Services.AddScoped<CrearReclamoDtoValidator>();
            builder.Services.AddScoped<ActualizarReclamoDtoValidator>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
