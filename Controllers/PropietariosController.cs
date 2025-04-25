using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace proyectoInmobiliariaNuevo.Controllers
{
    public class PropietariosController : Controller
    {
        private readonly ConexionDB _db;

        public PropietariosController()
        {
            _db = new ConexionDB();
        }

        // Acción que obtiene los propietarios y los pasa a la vista
        public ActionResult Index()
        {
            List<Propietario> propietarios = _db.ObtenerPropietarios(); // Obtén los propietarios de la base de datos
            return View(propietarios); // Pasa la lista de propietarios a la vista
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                _db.AgregarPropietario(propietario); // Agregar propietario
                ViewBag.Mensaje = "Propietario agregado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(propietario);
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
            Propietario propietario = _db.ObtenerPropietarioPorId(id); // Obtener propietario por ID
            if (propietario == null)
            {
                return NotFound(); // En ASP.NET Core usamos NotFound() para indicar que no se encontró el recurso
            }
            return View(propietario);
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Propietario propietario)
        {
            if (ModelState.IsValid)
            {
                _db.ActualizarPropietario(propietario); // Actualizar propietario
                ViewBag.Mensaje = "Propietario actualizado exitosamente.";
                return RedirectToAction("Index");
            }
            return View(propietario);
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            Propietario propietario = _db.ObtenerPropietarioPorId(id); // Obtener propietario por ID
            if (propietario == null)
            {
                return NotFound(); // En caso de que no lo encuentre, devolvemos NotFound
            }
            return View(propietario);
        }

        // POST: Propietarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _db.EliminarPropietario(id); // Eliminar propietario
            ViewBag.Mensaje = "Propietario eliminado exitosamente.";
            return RedirectToAction("Index");
        }

        [HttpGet]
public JsonResult ObtenerPorDni(string dni)
{
    try
    {
        if (string.IsNullOrWhiteSpace(dni))
        {
            return Json(new { error = "El DNI no puede estar vacío o ser nulo." });
        }

        int dniInt;
        if (!int.TryParse(dni, out dniInt))
        {
            return Json(new { error = "El DNI proporcionado no es válido. Debe ser un número." });
        }

        var propietario = _db.ObtenerPropietarioPorDni(dniInt); // Obtener propietario por DNI

        if (propietario != null)
        {
            return Json(new
            {
                dniPropietario = propietario.DniPropietario,
                apellidoPropietario = propietario.ApellidoPropietario,
                nombrePropietario = propietario.NombrePropietario,
                contactoPropietario = propietario.ContactoPropietario
            });
        }

        return Json(new { error = "No se encontró un propietario con ese DNI." }); // Si no se encuentra el propietario, devolvemos un error
    }
    catch (Exception ex)
    {
        // Aquí se captura cualquier error inesperado y se devuelve el mensaje
        return Json(new { error = "Ocurrió un error al obtener el propietario: " + ex.Message });
    }
}


        // Buscar propietarios
        [HttpGet]
        public JsonResult Buscar(string termino)
        {
            var propietarios = _db.BuscarPropietarios(termino)
                .Where(p => p.DniPropietario.Contains(termino) || p.NombrePropietario.Contains(termino) || p.ApellidoPropietario.Contains(termino))
                .Select(p => new { p.DniPropietario, p.NombrePropietario, p.ApellidoPropietario })
                .ToList();

            return Json(propietarios);
        }
    }
}
