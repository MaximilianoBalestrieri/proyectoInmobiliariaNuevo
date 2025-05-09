using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
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
                           .OrderBy(p => p.NroPago) // Ordenamos por número de pago
                           .ToList();

            ViewBag.idContrato = idContrato; // Asegúrate de que idContrato tenga un valor válido
            
            return View(pagos);  // Pasamos la lista de pagos a la vista
        }

        // Acción para anular un pago
       [HttpPost]
  


    
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
