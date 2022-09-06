using Entidades;
using Hangfire;
using HolaHangfire.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace HolaHagfire.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonasController : ControllerBase
{
    private AppDbContext _appDbContext;
    private IBackgroundJobClient _backgroundJobClient;

    public PersonasController(AppDbContext appDbContext, IBackgroundJobClient backgroundJobClient)
    {
        _appDbContext = appDbContext;
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("crear")]
    //public  async Task<ActionResult> Crear(string nombre)
    public ActionResult Crear(string nombre)
    {
        ////_backgroundJobClient.Enqueue(() => Console.WriteLine(nombre));
        //_backgroundJobClient.Enqueue(() => Console.WriteLine(nombre));
        //await AgregarPersonaAsync(nombre);

        //_backgroundJobClient.Enqueue(()=> AgregarPersonaAsync(nombre));
        Guid guid;

        guid = Guid.NewGuid();
        _backgroundJobClient.Enqueue<IRespositorioPersonas>(repositorioPersonas =>
            repositorioPersonas.AgregarPersonaAsync(nombre, guid)
        );

        return Ok(new { Guid = guid });
    }

    [HttpPost("Schedule")]
    public ActionResult Schedule(string nombreDeLaPersona)
    {
        _backgroundJobClient.Schedule(() => Console.WriteLine("El nombre es " + nombreDeLaPersona), TimeSpan.FromSeconds(15));

        return Ok();
    }
}
