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

    public IActionResult Comenzar(string username, int dificultad, int categoria)
    {
        Juego.CargarPartida(username, dificultad, categoria);
        return RedirectToAction("Jugar");
    }
    public IActionResult Jugar(string Username, int dificultad, int categoria)
{
    // Carga la partida con los parámetros proporcionados
    Juego.CargarPartida(Username, dificultad, categoria);
    
    // Obtiene la próxima pregunta sin argumentos
    Preguntas PreguntaElegida = Juego.ObtenerProximaPregunta();
    
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
        return View("Juego");
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

    public IActionResult ConfigurarJuego()
    {
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.dificultad = BD.ObtenerDificultades();
        return View("ConfigurarJuego");
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
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
