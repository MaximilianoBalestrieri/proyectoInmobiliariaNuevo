using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;
using System;
using System.Linq;

namespace proyectoInmobiliariaNuevo.Controllers
{
    public class PagosController : Controller
    {
        private readonly ConexionDB _db;

        public PagosController()
        {
            _db = new ConexionDB(); // Idealmente deberías inyectarlo por DI
        }

        public IActionResult Index(int idContrato)
        {
            var pagos = _db.ObtenerPagosPorContrato(idContrato)
                           .OrderBy(p => p.NroPago)
                           .ToList();

            ViewBag.IdContrato = idContrato;
            return View(pagos);
        }

        [HttpPost]
        public JsonResult AnularPago(int idPago)
        {
            try
            {
                _db.AnularPago(idPago);
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
                _db.ActualizarDetalle(idPago, detalle);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
