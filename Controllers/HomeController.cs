using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;
using Microsoft.AspNetCore.Http;
using System;  // Necesario para usar HttpContext.Session

namespace proyectoInmobiliariaNuevo.Controllers
{
    public class HomeController : Controller
    {
          ConexionDB db = new ConexionDB();
    public IActionResult Index()
{
    var usuario = HttpContext.Session.GetString("Usuario");
    var apellido = HttpContext.Session.GetString("NombreyApellido");
    var rol = HttpContext.Session.GetString("Rol");
    var fotoPeril=HttpContext.Session.GetString("FotoPerfil");
    ViewBag.Usuario = usuario;
    ViewBag.Rol = rol;
    ViewBag.Apellido = apellido;
    ViewBag.FotoPerfil= fotoPeril;
  

    if (usuario == null)
    {
        return RedirectToAction("Index", "Login");
    }

    return View();
}

public IActionResult Perfil()
{
    var usuario = db.ObtenerUsuarioPorNombre(User.Identity.Name); // o el nombre que uses
    ViewBag.FotoPerfil = usuario.FotoPerfil;
    ViewBag.Usuario = usuario.UsuarioNombre;
    ViewBag.Apellido = usuario.NombreyApellido;
    ViewBag.Rol = usuario.Rol;

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
