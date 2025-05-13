using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using proyectoInmobiliariaNuevo.Models;
using System;
using System.Linq;

namespace proyectoInmobiliariaNuevo.Controllers
{
    [Route("Contratos")]
    public class ContratosController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ConexionDB db;

        public ContratosController(IConfiguration configuration)
        {
            _configuration = configuration;
            db = new ConexionDB();
        }

        // Listar contratos
        [HttpGet("")]
        public ActionResult Index()
        {
            var rol = HttpContext.Session.GetString("Rol");
            ViewBag.Rol = rol ?? "Usuario";
            ViewBag.NombreyApellido = HttpContext.Session.GetString("NombreyApellido");
            var contratos = db.ObtenerTodosLosContratos();
            return View(contratos);
        }


//---------------------------- ANULAR PAGO ------------------------

[HttpPost("/Contratos/AnularPago")]
public JsonResult AnularPago([FromBody] PagoRequest request)
{
    Console.WriteLine($"Se aNUló el pago ID {request.IdPago} por {request.PagadoPor}");
    try
    {
        ConexionDB conexion = new ConexionDB();
        bool resultado = conexion.AnularPago(request.IdPago, request.PagadoPor);
        return Json(new { success = resultado });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}





        // Crear contrato - GET
        [HttpGet("Crear")]
        public ActionResult Create()
        {
            ViewBag.NombreyApellido = HttpContext.Session.GetString("NombreyApellido");
            
            return View();
        }

        // Crear contrato - POST
        [HttpPost("Crear")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato contrato)
        {
            Console.WriteLine("CONTRATO Realizado por: " + contrato.RealizadoPor);

            if (string.IsNullOrEmpty(contrato.RealizadoPor))
    {
        // Opcionalmente forzás acá el valor también:
        contrato.RealizadoPor = ViewBag.NombreyApellido as string;
    }
            if (!db.VerificarDisponibilidad(contrato))
                ModelState.AddModelError("", "El inmueble ya tiene un contrato en esas fechas.");

            if (ModelState.IsValid)
            {
                db.AgregarContrato(contrato);
                return RedirectToAction("Index");
            }

            return View(contrato);
        }

        // Editar contrato - GET
        [HttpGet("Editar/{id}")]
        public ActionResult Edit(int id)
        {
            var contrato = db.ObtenerContratoPorId(id);
            if (contrato == null)
                return NotFound();
            ViewBag.Rol = HttpContext.Session.GetString("Rol");
            ViewBag.NombreyApellido = HttpContext.Session.GetString("NombreyApellido");
            ViewBag.Pagos = db.ObtenerPagosPorContrato(id);
            return View(contrato);
        }

        // Editar contrato - POST
        [HttpPost("Editar/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Contrato contrato)
        {
             ViewBag.NombreyApellido = HttpContext.Session.GetString("NombreyApellido");
             ViewBag.Rol = HttpContext.Session.GetString("Rol");
            if (ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
    {
        Console.WriteLine(error.ErrorMessage);
    }
                if (db.EditarContrato(contrato.IdContrato, contrato.FechaInicio, contrato.FechaFinal, contrato.Monto))
                    return RedirectToAction("Index");
            }
            return View(contrato);
        }

        // Eliminar contrato - GET
        [HttpGet("Eliminar/{id}")]
        public ActionResult Delete(int id)
        {
            var contrato = db.ObtenerContratoPorId(id);
            if (contrato == null)
                return NotFound();

            return View(contrato);
        }

        // Eliminar contrato - POST
        [HttpPost("Eliminar/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.EliminarContrato(id);
            return RedirectToAction("Index");
        }

        // Buscar propietarios
        [HttpGet("BuscarPropietarios")]
        public ActionResult BuscarPropietarios(string termino)
        {
            var propietarios = db.BuscarPropietarios(termino);
            return PartialView("_ListaPropietarios", propietarios);
        }

        // Buscar inquilinos
        [HttpGet("BuscarInquilinos")]
        public ActionResult BuscarInquilinos(string termino)
        {
            var inquilinos = db.BuscarInquilinos(termino);
            return PartialView("_ListaInquilinos", inquilinos);
        }

        // Buscar inmuebles por DNI de propietario
        [HttpGet("BuscarInmueblesPorDni")]
        public JsonResult BuscarInmueblesPorDni(string dniPropietario)
        {
            var lista = db.ObtenerInmuebles()
                          .Where(i => i.DniPropietario == dniPropietario)
                          .ToList();
            return Json(lista);
        }
[HttpPost("AgregarPago")]
public JsonResult AgregarPago([FromBody] Pago pago)
{
    try
    {
        Console.WriteLine("Datos recibidos para agregar pago:");
        Console.WriteLine($"IdContrato: {pago.IdContrato}");
        Console.WriteLine($"NroPago: {pago.NroPago}");
        Console.WriteLine($"FechaPago: {pago.FechaPago}");
        Console.WriteLine($"Importe: {pago.Importe}");
        Console.WriteLine($"Detalle: {pago.Detalle}");
        Console.WriteLine($"Estado: {pago.Estado}");

        bool exito = db.AgregarPago(pago);

        if (exito)
        {
            return Json(new { success = true, message = "Pago agregado correctamente." });
        }
        else
        {
            return Json(new { success = false, message = "No se pudo agregar el pago." });
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error en AgregarPago: " + ex.Message);
        return Json(new { success = false, message = "Error interno: " + ex.Message });
    }
}


[HttpPost("/Contratos/ActualizarPago")]
public JsonResult ActualizarPago(Pago pago)
{
    try
    {
        // Actualizar pago en la base de datos
        ConexionDB conexion = new ConexionDB();
        conexion.ActualizarPago(pago);

        // Obtener el pago actualizado desde la base de datos (opcional, pero te da los datos completos)
        var pagoActualizado = conexion.ObtenerPagoPorId(pago.IdPago);

        // Devolver la respuesta con los nuevos valores
        return Json(new { success = true, pago = pagoActualizado });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, message = ex.Message });
    }
}




     [HttpPost ("ActualizarDetalle")]
public JsonResult ActualizarDetalle(int idPago, string detalle)
{
    Console.WriteLine($"ID Pago recibido: {idPago}");  
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



        // Buscar contratos entre fechas
        [HttpGet("BuscarContratos")]
        public JsonResult BuscarContratos(DateTime desde, DateTime hasta)
        {
            try
            {
                if (hasta < desde)
                    return Json(new { error = "La fecha 'hasta' no puede ser menor que la fecha 'desde'." });

                var contratos = db.BuscarContratos(desde, hasta);

                foreach (var contrato in contratos)
                {
                    var propietario = db.ObtenerPropietarioPorDni(contrato.DniPropietario);
                    var inquilino = db.ObtenerInquilinoPorDni(contrato.DniInquilino);

                    contrato.NombrePropietario = $"{propietario?.NombrePropietario} {propietario?.ApellidoPropietario}";
                    contrato.NombreInquilino = $"{inquilino?.NombreInquilino} {inquilino?.ApellidoInquilino}";
                }

                return Json(contratos);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // Renovar contrato (simple)
        [HttpPost("Renovar")]
        public ActionResult Renovar(Contrato nuevoContrato)
        {
            try
            {
                nuevoContrato.Vigente = true;
                db.AgregarContrato(nuevoContrato);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Guardar renovación
        [HttpPost("GuardarRenovacion")]
        public ActionResult GuardarRenovacion(NuevaRenovacionModel datos)
        {
            try
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

                db.MarcarComoNoVigente(datos.ContratoAnteriorId);
                db.AgregarContrato(nuevo);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // Crear renovación desde vista
        [HttpPost("CrearRenovacion")]
        [ValidateAntiForgeryToken]
        public ActionResult CrearRenovacion(RenovacionContratoViewModel data)
        {
            try
            {
                db.MarcarContratoComoNoVigente(data.IdContratoAnterior);

                var nuevoContrato = new Contrato
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

                db.AgregarContrato(nuevoContrato);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al renovar: " + ex.Message);
            }
        }


[HttpGet]
    [Route("Contratos/Index/{idContrato}")]
    public IActionResult Index(int idContrato)
    {
        var contratos = db.ObtenerContratos(); // Ejemplo de llamada a tu base de datos
        return View(contratos);
    }



        // Obtener pagos por contrato
 [HttpGet("GetByContrato/{idContrato}")]
public IActionResult GetByContrato(int idContrato)
{
    // Aquí obtienes los pagos desde la base de datos
    var pagos = db.ObtenerPagosPorContrato(idContrato);
    
    if (pagos == null || pagos.Count == 0)
    {
        return Json(new { success = false, message = "No se encontraron pagos." });
    }
    
    return Json(new { success = true, pagos });
}



    }
}
