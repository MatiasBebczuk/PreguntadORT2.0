using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PreguntadORT.Models;

namespace PreguntadORT.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        Juego.InicializarJuego();
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
        return View();
    }

    public IActionResult Comenzar(string Usuario, int dificultad, int categoria)
    {
        Juego.CargarPartida(Usuario, dificultad, categoria);
        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar()
    {    
    Preguntas? PreguntaElegida = Juego.ObtenerProximaPregunta();

    ViewBag.Username = Juego.Username;
    
    if (PreguntaElegida == null)
    {
        return View("Fin");
    }
    else
    {
        ViewBag.PreguntaElegida = PreguntaElegida;
        ViewBag.ListaRespuestas = Juego.ObtenerProximasRespuestas(PreguntaElegida.IdPregunta);
        ViewBag.Usuario = Juego.Username; 
        ViewBag.PuntajeActual = Juego.puntajeActual;
        return View("Jugar");
    }
}


    [HttpPost] 

     public IActionResult VerificarRespuesta(int idRespuesta){
        
        if(Juego.VerificarRespuesta(idRespuesta)){
            ViewBag.Mensaje="CORRECTO";
        }
        else{
            ViewBag.Mensaje="INCORRECTO";
        }
        return View("Respuesta");
     }


        public IActionResult InicializarJuego()
    {
        ViewBag.dificultad = BD.ObtenerDificultades();
        ViewBag.Categorias = BD.ObtenerCategorias();
        return View("InicializarJuego");
        
    }
    public IActionResult Tutorial()
    {
        return View();
    }
    public IActionResult Creditos()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
