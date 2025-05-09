using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Http;

namespace proyectoInmobiliariaNuevo.Controllers
{
    public class InquilinosController : Controller
    {
        // Usar Dependency Injection para acceder a la base de datos
        private readonly ConexionDB _db;

        // Constructor que inyecta ConexionDB
        public InquilinosController(ConexionDB db)
        {
            _db = db; // Asignamos la instancia de ConexionDB
        }

        // Acción que obtiene los inquilinos y los pasa a la vista
        public IActionResult Index()
        {
            var rol = HttpContext.Session.GetString("Rol");
            ViewBag.Rol = rol ?? "Usuario";
            List<Inquilino> inquilinos = _db.ObtenerInquilinos();  // Obtén los inquilinos de la base de datos

            // Depuración: Verifica los datos
            foreach (var inquilino in inquilinos)
            {
                Console.WriteLine($"Inquilino ID: {inquilino.IdInquilino}, Nombre: {inquilino.NombreInquilino}, Apellido: {inquilino.ApellidoInquilino}");
            }

            return View(inquilinos);  // Pasa la lista de inquilinos a la vista
        }

        // GET: Inquilinos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                _db.AgregarInquilino(inquilino);  // Asegúrate de implementar el método AgregarInquilino en tu clase de conexión a la base de datos
                ViewBag.Mensaje = "Inquilino agregado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Edit/5
        public IActionResult Edit(int id)
        {
            Inquilino inquilino = _db.ObtenerInquilinoPorId(id);  // Implementa este método para obtener un inquilino por su ID
            if (inquilino == null)
            {
                return NotFound();  // Cambié HttpNotFound() por NotFound() en ASP.NET Core
            }
            return View(inquilino);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                _db.ActualizarInquilino(inquilino);  // Implementa este método para actualizar un inquilino en la base de datos
                ViewBag.Mensaje = "Inquilino actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Delete/5
        public IActionResult Delete(int id)
        {
            Inquilino inquilino = _db.ObtenerInquilinoPorId(id);  // Implementa este método para obtener un inquilino por su ID
            if (inquilino == null)
            {
                return NotFound();  // Cambié HttpNotFound() por NotFound() en ASP.NET Core
            }
            return View(inquilino);
        }

        // POST: Inquilinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _db.EliminarInquilino(id);  // Implementa este método para eliminar un inquilino de la base de datos
            ViewBag.Mensaje = "Inquilino eliminado exitosamente.";
            return RedirectToAction("Index");
        }

        // Buscar inquilinos por término
        [HttpGet]
        public JsonResult Buscar(string termino)
        {
            var inquilinos = _db.BuscarInquilinos(termino)
                .Select(p => new {
                    Dni = p.DniInquilino,
                    Nombre = p.NombreInquilino,
                    Apellido = p.ApellidoInquilino
                })
                .ToList();

            return Json(inquilinos);  // Devuelve los resultados en formato JSON
        }
    }
}
