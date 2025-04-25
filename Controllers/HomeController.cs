using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;
using Microsoft.AspNetCore.Http;  // Necesario para usar HttpContext.Session

namespace proyectoInmobiliariaNuevo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("Usuario") == null)
            {
                // Si no hay nadie logueado, lo mandamos al login
                return RedirectToAction("Index", "Login");
            }

            // Si hay alguien logueado, pasamos su info a la vista
            ViewBag.Usuario = HttpContext.Session.GetString("Usuario");
            ViewBag.Rol = HttpContext.Session.GetString("Rol");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descripción del Software.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "CONTACTO";
            return View();
        }

        public ActionResult Inquilinos()
        {
            return View();
        }

        public ActionResult Propietarios()
        {
            return View();
        }

        public ActionResult Inmuebles()
        {
            return View();
        }

        public ActionResult Contratos()
        {
            return View();
        }
    }
}
