using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;

public class LoginController : Controller
{
    private ConexionDB conexionDB = new ConexionDB();

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string usuario, string contraseña)
    {
        var user = conexionDB.BuscarUsuario(usuario, contraseña);

        if (user != null)
        {
            HttpContext.Session.SetString("Usuario", user.UsuarioNombre);
            HttpContext.Session.SetString("Rol", user.Rol);
           return RedirectToAction("Index", "Home");

        }
        else
        {
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            ViewBag.Usuario = usuario; // <-- guarda lo que escribió para volver a mostrarlo
            return View("Index");
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

[HttpGet]
public IActionResult Index()
{
    return View();
}


}


    
