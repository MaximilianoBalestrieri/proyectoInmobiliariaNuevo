using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using proyectoInmobiliariaNuevo.Models;

namespace proyectoInmobiliariaNuevo.Controllers
{
   
   

    public class ContratosController : Controller
    {
        ConexionDB db = new ConexionDB();

        public ActionResult Index()
        {
            var contratos = db.ObtenerTodosLosContratos();
            return View(contratos);
        }

        // GET: Contratos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            if (!db.VerificarDisponibilidad(contrato))
            {
                ModelState.AddModelError("", "El inmueble ya tiene un contrato en esas fechas.");
            }

            if (ModelState.IsValid)
            {
                db.AgregarContrato(contrato); // Asegurate de tener este método en ConexionDB
                return RedirectToAction("Index");
            }

            return View(contrato);
        }




        public ActionResult BuscarPropietarios(string termino)
        {
            var propietarios = db.BuscarPropietarios(termino);  // Método para buscar propietarios
            return PartialView("_ListaPropietarios", propietarios); // Vista parcial que muestra los resultados
        }

        // Acción para buscar inquilinos
        public ActionResult BuscarInquilinos(string termino)
        {
            var inquilinos = db.BuscarInquilinos(termino);  // Método para buscar inquilinos
            return PartialView("_ListaInquilinos", inquilinos); // Vista parcial que muestra los resultados
        }

        [HttpGet]
        public JsonResult BuscarInmueblesPorDni(string dniPropietario)
        {
            var conexion = new ConexionDB();
            var lista = conexion.ObtenerInmuebles()
                .Where(i => i.DniPropietario == dniPropietario)
                .ToList();
            return Json(lista);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var contrato = db.ObtenerContratoPorId(id);
            if (contrato == null)
            {
               return NotFound();

            }

            var pagos = db.ObtenerPagosPorContrato(id); // asegurate de tener este método en ConexionDB
            ViewBag.Pagos = pagos;

            return View(contrato);
        }



        [HttpPost]
        public ActionResult Edit(Contrato contrato)
        {
            if (ModelState.IsValid)
            {
                var exito = db.EditarContrato(
                    contrato.IdContrato,
                    contrato.FechaInicio,
                    contrato.FechaFinal,
                    contrato.Monto
                );

                if (exito)
                {
                    return RedirectToAction("Index");
                }
            }

            // Si hay error, se vuelve a mostrar el formulario con los datos ingresados
            return View(contrato);
        }

        public ActionResult Delete(int id)
        {
            var contrato = db.ObtenerContratoPorId(id);
            if (contrato == null)
                return NotFound();


            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.EliminarContrato(id);
            return RedirectToAction("Index");
        }


        public JsonResult AgregarPago(Pago pago)
        {
            bool exito = false;

            try
            {
                exito = db.AgregarPago(pago); // Tu método para guardar
                return Json(new { success = exito });
            }
            catch (Exception ex)
            {
                // Enviar el mensaje de error completo
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        public JsonResult AnularPago(int idPago)
        {
            try
            {
                var db = new ConexionDB();
                db.AnularPago(idPago); // Aquí llamamos al método AnularPago
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ActualizarDetalle(int idPago, string detalle)
        {
            try
            {
                ConexionDB db = new ConexionDB();
                db.ActualizarDetalle(idPago, detalle);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult BuscarContratos(DateTime desde, DateTime hasta)
        {
            try
            {
                if (hasta < desde)
                {
                 return Json(new { error = "La fecha 'hasta' no puede ser menor que la fecha 'desde'." });
                }

                ConexionDB conexionDB = new ConexionDB();
                var contratos = conexionDB.BuscarContratos(desde, hasta);

                foreach (var contrato in contratos)
                {
                    var propietario = conexionDB.ObtenerPropietarioPorDni(contrato.DniPropietario);
                    var inquilino = conexionDB.ObtenerInquilinoPorDni(contrato.DniInquilino);

                    contrato.NombrePropietario = propietario?.NombrePropietario + " " + propietario?.ApellidoPropietario;
                    contrato.NombreInquilino = inquilino?.NombreInquilino + " " + inquilino?.ApellidoInquilino;
                }

                return Json(contratos);

            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });

            }
        }


        [HttpPost]
        public ActionResult Renovar(Contrato nuevoContrato)
        {
            try
            {
                ConexionDB db = new ConexionDB();
                nuevoContrato.Vigente = true;
                db.AgregarContrato(nuevoContrato); // Asumiendo que tenés un método así
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
               return StatusCode(500, ex.Message);

            }
        }


        [HttpPost]
        public ActionResult GuardarRenovacion(NuevaRenovacionModel datos)
        {
            var nuevo = new Contrato
            {
                DniPropietario = datos.NuevoContrato.DniPropietario,
                NombrePropietario = datos.NuevoContrato.NombrePropietario,
                DniInquilino = datos.NuevoContrato.DniInquilino,
                NombreInquilino = datos.NuevoContrato.NombreInquilino,
                FechaInicio = datos.NuevoContrato.FechaInicio,
                FechaFinal = datos.NuevoContrato.FechaFinal,
                Monto = datos.NuevoContrato.Monto,
                IdInmueble = datos.NuevoContrato.IdInmueble,
                Direccion = datos.NuevoContrato.Direccion,
                Vigente = true
            };

            var db = new ConexionDB();
            db.AgregarContrato(nuevo);
            db.MarcarComoNoVigente(datos.ContratoAnteriorId); // Método que deberías tener para actualizar

            return Ok();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearRenovacion(RenovacionContratoViewModel data)
        {
            try
            {
                int idAnterior = data.IdContratoAnterior;
                Contrato nuevoContrato = new Contrato
                {
                    DniPropietario = data.DniPropietario,
                    NombrePropietario = data.NombrePropietario,
                    DniInquilino = data.DniInquilino,
                    NombreInquilino = data.NombreInquilino,
                    FechaInicio = data.FechaInicio,
                    FechaFinal = data.FechaFinal,
                    Monto = data.Monto,
                    IdInmueble = data.IdInmueble,
                    Direccion = data.Direccion,
                    Vigente = true
                };

                db.MarcarContratoComoNoVigente(idAnterior);
                db.AgregarContrato(nuevoContrato);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
               return StatusCode(500, "Error al renovar: " + ex.Message);

            }
        }



    }
}