using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Api.Filters;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using ObjetosPerdidosUCB.Infrastructure.Mappings;
using ObjetosPerdidosUCB.Infrastructure.Repositories;
using ObjetosPerdidosUCB.Services.Interfaces;
using ObjetosPerdidosUCB.Services.Service;
using ObjetosPerdidosUCB.Services.Validators.Objetos;
using ObjetosPerdidosUCB.Services.Validators.Reclamos;
using ObjetosPerdidosUCB.Services.Validators.Usuarios;

namespace ObjetosPerdidosUCB.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configurar la BD MySql
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<ObjetosPerdidosContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion

            // Repositorios
            builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddTransient<ILugarAlmacenamientoRepository, LugarAlmacenamientoRepository>();
            builder.Services.AddTransient<IObjetoRepository, ObjetoRepository>();
            builder.Services.AddTransient<IReclamoRepository, ReclamoRepository>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Una sola llamada registra todos los Profile del ensamblado Infrastructure
            builder.Services.AddAutoMapper(typeof(ObjetoProfile).Assembly);

            // Validadores de FluentValidation
            builder.Services.AddScoped<CrearObjetoDtoValidator>(); 
            builder.Services.AddScoped<ActualizarObjetoDtoValidator>();
            builder.Services.AddScoped<ObjetoDtoValidator>();
            builder.Services.AddScoped<CrearReclamoDtoValidator>();
            builder.Services.AddScoped<CrearUsuarioDtoValidator>();
            builder.Services.AddScoped<ActualizarUsuarioDtoValidator>();
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IObjetoRepository, ObjetoRepository>();
            builder.Services.AddScoped<IObjetoService, ObjetoService>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IReclamoService, ReclamoService>();
            builder.Services.AddScoped<IReclamoRepository, ReclamoRepository>();
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<ILugarAlmacenamientoRepository, LugarAlmacenamientoRepository>();
            builder.Services.AddScoped<ILugarAlmacenamientoService, LugarAlmacenadoService>();


            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

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
