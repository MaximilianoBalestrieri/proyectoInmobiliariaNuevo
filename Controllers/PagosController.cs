using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;
using System;
using System.Linq;

namespace proyectoInmobiliariaNuevo.Controllers
{
    [Route("Pagos")]
    //-----------------PAGOS CONTROLLER -----------------
    public class PagosController : Controller
    {
       
        private readonly ConexionDB _db;

        // Constructor con inyección de dependencias
        public PagosController(ConexionDB db)
        {
            _db = db;  // Aquí se inyecta la instancia de ConexionDB
        }

        // Acción para mostrar los pagos de un contrato
        public IActionResult Index(int idContrato)
        {
            var pagos = _db.ObtenerPagosPorContrato(idContrato)
                           .OrderBy(p => p.NroPago) // ordenamos por número de pago
                           .ToList();

            ViewBag.idContrato = idContrato; // Asegúrate de que idContrato tenga un valor válido
    return View();
        }

        // Acción para anular un pago
        [HttpPost]
        public JsonResult AnularPago(int idPago)
        {
            try
            {
                _db.AnularPago(idPago);  // Método en tu clase ConexionDB
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // Acción para actualizar el detalle de un pago
        
        [HttpPost("ActualizarDetalle")]
public JsonResult ActualizarDetalle([FromBody] PagoUpdateRequest request)
{
    Console.WriteLine($"ID Pago recibido: {request.idPago}");
    try
    {
        _db.ActualizarDetalle(request.idPago, request.detalle);
        return Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, error = ex.Message });
    }
}



        [HttpGet("GetByContrato/{idContrato}")]
public IActionResult GetByContrato(int idContrato)
{
    var pagos = _db.ObtenerPagosPorContrato(idContrato);
    return Json(new { success = true, pagos });
}

    }
}
