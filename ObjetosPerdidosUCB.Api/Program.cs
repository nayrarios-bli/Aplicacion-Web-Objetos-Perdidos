
using Microsoft.EntityFrameworkCore;
using ObjetosPerdidosUCB.Core.Interfaces;
using ObjetosPerdidosUCB.Infrastructure.Data;
using ObjetosPerdidosUCB.Infrastructure.Mapping;
using ObjetosPerdidosUCB.Infrastructure.Repositories;

namespace ObjetosPerdidosUCB.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region COnfigura MySQL
            var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
            builder.Services.AddDbContext<ObjetoPerdidosContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            #endregion
            builder.Services.AddTransient<IObjetoRepository, ObjetoRepository>();
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddAutoMapper(typeof(ObjetoProfile).Assembly);

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
