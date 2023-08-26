using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema.BLL.Servicios.Contrato;
using Sistema.BLL.Servicios.Implementacion;
using Sistema.DAL.DBContext;
using Sistema.DAL.Repositorios.Contrato;
using Sistema.DAL.Repositorios.Implementacion;
using Sistema.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbcitasmedicasContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"));
            });

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICitaRepository, CitaRepository>();

            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IEspecialidadService, EspecialidadService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IDoctorService, DoctorService>();
        }
    }
}
