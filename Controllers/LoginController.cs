using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;

namespace proyectoInmobiliariaNuevo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // GET: Login/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string usuario, string contraseña)
        {
            // Simulación: validamos usuario desde BD (esto lo conectas con tu clase que consulta MySQL)
            if (usuario == "admin" && contraseña == "123")
            {
                // Guardar la información del usuario en la sesión
                HttpContext.Session.SetString("Usuario", usuario);
                HttpContext.Session.SetString("Rol", "administrador");
                return RedirectToAction("Index", "Home");
            }
            else if (usuario == "empleado" && contraseña == "123")
            {
                HttpContext.Session.SetString("Usuario", usuario);
                HttpContext.Session.SetString("Rol", "empleado");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View("Index");
            }
        }

        // POST: Login/Create
        [HttpPost]
        public IActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public IActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
