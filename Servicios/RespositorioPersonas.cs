using Entidades;
using HolaHangfire.Repositorio;

namespace Servicios
{
    public interface IRespositorioPersonas
    {
        Task AgregarPersonaAsync(string nombre);
        Task AgregarPersonaAsync(string nombre, Guid guid);
    }

    public class RespositorioPersonas: IRespositorioPersonas
    {
        private AppDbContext _appDbContext;
        private ILogger<RespositorioPersonas> _logger;

        public RespositorioPersonas(AppDbContext appDbContext, ILogger<RespositorioPersonas> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task AgregarPersonaAsync(string nombre)
        {
            _logger.LogInformation("Agregando persona");
            var persona = new Persona { Nombre = nombre };
            await Task.Delay(5000);
            _appDbContext.Add(persona);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Persona agregada: " + nombre);
        }

        public async Task AgregarPersonaAsync(string nombre, Guid guid)
        {
            _logger.LogInformation("Agregando persona");
            var persona = new Persona { Nombre = nombre };
            await Task.Delay(5000);
            _appDbContext.Add(persona);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Persona agregada: " + nombre);
        }
    }

}