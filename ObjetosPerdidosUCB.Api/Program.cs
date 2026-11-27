using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ObjetosPerdidosUCB.Api.Filters;
using ObjetosPerdidosUCB.Core.DTOs;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using ObjetosPerdidosUCB.Infrastructure.Mappings;
using ObjetosPerdidosUCB.Infrastructure.Repositories;
using ObjetosPerdidosUCB.Services.Interface;
using ObjetosPerdidosUCB.Services.Interfaces;
using ObjetosPerdidosUCB.Services.Service;
using ObjetosPerdidosUCB.Services.Validators.Objetos;
using ObjetosPerdidosUCB.Services.Validators.Reclamos;
using ObjetosPerdidosUCB.Services.Validators.Usuarios;
using SocialMedia.Core.Interfaces;

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

            // Repositorios y services
            builder.Services.AddTransient<IObjetoRepository, ObjetoRepository>();
            builder.Services.AddTransient<IObjetoService, ObjetoService>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<IUsuarioService, UsuarioService>();
            builder.Services.AddTransient<IReclamoService, ReclamoService>();
            builder.Services.AddTransient<IReclamoRepository, ReclamoRepository>();
            builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            builder.Services.AddTransient<ICategoriaService, CategoriaService>();
            builder.Services.AddTransient<ILugarAlmacenamientoRepository, LugarAlmacenamientoRepository>();
            builder.Services.AddTransient<ILugarAlmacenamientoService, LugarAlmacenadoService>();
            builder.Services.AddTransient<ISecurityService, SecurityService>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
            builder.Services.AddScoped<IDapperContext, DapperContext>();

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            builder.Services.AddOpenApi();


            //Configurar JWT
            builder.Services.AddAuthentication(options =>
            {
                /*Esquema por defecto para autenticar (identificar quién es el usuario).
                 * DefaultAuthenticateScheme → Esquema por defecto para autenticar (identificar quién es el usuario).
                 * JwtBearerDefaults.AuthenticationScheme → Equivale al string "Bearer".
                 * Significado: "Cuando llegue una petición, usa JWT Bearer para identificar al usuario"
                 * */
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;
                /*DefaultChallengeScheme → Esquema por defecto cuando se desafía al usuario (por ejemplo, 
                 * si intenta acceder a algo sin estar autenticado).
                 * Si no hay token o es inválido, el sistema responderá con un desafío típico de JWT (401 Unauthorized)               
                 * */
                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    //TokenValidationParameters → Objeto que contiene todas las reglas de validación para un token JWT.
                    new TokenValidationParameters
                    {
                        //Valida el emisor (iss) → Verifica que el token haya sido emitido por un servidor de confianza. Evita que alguien use tokens creados por otro sistema.
                        ValidateIssuer = true,
                        //Verifica que el token esté dirigido a esta API. Evita que un token para otro servicio(ej: un frontend) sea aceptado aquí.
                        ValidateAudience = true,
                        //Comprueba que el token no haya expirado y que su "no válido antes de" sea correcto. Es la razón por la que los tokens dejan de funcionar automáticamente después de X tiempo.
                        ValidateLifetime = true,
                        //Verifica que el token no haya sido modificado. Usa la clave secreta para asegurar que el token es genuino y no manipulado.
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Authentication:Issuer"],
                        ValidAudience = builder.Configuration["Authentication:Audience"],
                        /*SymmetricSecurityKey → Clave simétrica (misma clave para firmar y verificar).
                         * Encoding.UTF8.GetBytes(...) → Convierte la clave(string) en bytes.
                         * builder.Configuration["Authentication:SecretKey"] → Lee la clave desde appsettings.json(ej: "miSuperSecretoLargo123!").
                        */
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(
                                builder.Configuration["Authentication:SecretKey"]
                            )
                        )
                    };
            });

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

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
