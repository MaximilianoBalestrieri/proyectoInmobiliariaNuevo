using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using static proyectoInmobiliariaNuevo.Models.ConexionDB;
using proyectoInmobiliariaNuevo.Models;
using System.Linq;
using MySql.Data.MySqlClient;


namespace proyectoInmobiliariaNuevo.Controllers
{
    public class InmueblesController(IWebHostEnvironment env) : Controller
    {
      private readonly IWebHostEnvironment _env = env;
    private readonly ConexionDB conexionDB = new ConexionDB();

        public IActionResult Index()
    {
         var rol = HttpContext.Session.GetString("Rol");
    ViewBag.Rol = rol ?? "Usuario"; 
        // Aquí puedes obtener los inmuebles desde la base de datos, por ejemplo:
        var inmuebles = conexionDB.ObtenerInmuebles();  // Suponiendo que tienes este método en ConexionDB

        return View(inmuebles);  // Devuelves la lista de inmuebles a la vista
    }

  // GET: Inmuebles/Create
  public ActionResult Create()
  {
      return View();
  }

        // POST: Inmuebles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

public async Task<IActionResult> Create(Inmueble inmueble, IFormFile FotoPortada, List<IFormFile> fotosCarrusel)
{
    if (ModelState.IsValid)
    {
        // Guardar la imagen de portada
        if (FotoPortada != null && FotoPortada.Length > 0)
        {
            // Obtiene la ruta del directorio para las imágenes
            var pathPortada = Path.Combine(_env.WebRootPath, "Uploads", "Portadas", FotoPortada.FileName);

            // Guarda el archivo de la portada
            using (var stream = new FileStream(pathPortada, FileMode.Create))
            {
                await FotoPortada.CopyToAsync(stream);
            }

            // Asigna la ruta relativa para almacenar en la base de datos
            inmueble.ImagenPortada = "/Uploads/Portadas/" + FotoPortada.FileName;
        }

        // Guardar imágenes del carrusel
        List<string> rutasCarrusel = new List<string>();
        if (fotosCarrusel != null && fotosCarrusel.Count > 0)
        {
            foreach (var foto in fotosCarrusel)
            {
                if (foto != null && foto.Length > 0)
                {
                    // Obtiene la ruta para cada imagen del carrusel
                    var pathCarrusel = Path.Combine(_env.WebRootPath, "Uploads", "Carrusel", foto.FileName);

                    // Guarda el archivo de la imagen del carrusel
                    using (var stream = new FileStream(pathCarrusel, FileMode.Create))
                    {
                        await foto.CopyToAsync(stream);
                    }

                    // Agrega la ruta de la imagen al listado
                    rutasCarrusel.Add("/Uploads/Carrusel/" + foto.FileName);
                }
            }

            // Asigna las rutas del carrusel (si es necesario)
            // inmueble.FotosCarrusel = string.Join(";", rutasCarrusel);  // Descomentar si lo usas
        }

        // Inserta el inmueble y obtiene el ID generado
        int idGenerado = conexionDB.AgregarInmueble(inmueble);

        return RedirectToAction("Index");
    }

    // Si el modelo no es válido, regresa a la vista con los errores
    return View(inmueble);
}


  public ActionResult Edit(int id)
  {
      var inmueble = conexionDB.ObtenerInmueblePorId(id);
      var fotos = conexionDB.ObtenerFotosCarruselPorInmueble(id); // Este debe devolver una lista de objetos InmuebleFotoCarrusel.

      var viewModel = new InmuebleEditViewModel
      {
          Inmueble = inmueble,
          RutasCarrusel = fotos // Aquí pasas la lista de objetos InmuebleFotoCarrusel.
      };

      return View(viewModel);
  }

   [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(InmuebleEditViewModel model, IFormFile FotoPortada, List<IFormFile> FotosCarrusel)
{
    // Eliminamos validación automática de FotoPortada y RutasCarrusel porque los manejamos aparte
    ModelState.Remove("FotoPortada");
    ModelState.Remove("RutasCarrusel");

    foreach (var error in ModelState)
    {
        foreach (var subError in error.Value.Errors)
        {
            Console.WriteLine($"Error en el campo '{error.Key}': {subError.ErrorMessage}");
        }
    }

    if (ModelState.IsValid)
    {
        ViewBag.Mensaje = "🏡 entro al edit de inmueble.";
        try
        {
            string rutaOriginalPortada = model.Inmueble.ImagenPortada;

            // ✅ Procesar imagen de portada
            if (FotoPortada != null && FotoPortada.Length > 0)
            {
                // Eliminar imagen anterior si existe
                if (!string.IsNullOrEmpty(rutaOriginalPortada))
                {
                    string rutaAnterior = Path.Combine(_env.WebRootPath, rutaOriginalPortada.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                    if (System.IO.File.Exists(rutaAnterior))
                    {
                        System.IO.File.Delete(rutaAnterior);
                    }
                }

                string extension = Path.GetExtension(FotoPortada.FileName);
                string nombreArchivo = Path.GetFileNameWithoutExtension(FotoPortada.FileName);
                string nombreUnico = $"{nombreArchivo}_{Guid.NewGuid()}{extension}";
                string rutaRelativa = Path.Combine("imagenes", "inmuebles", nombreUnico);
                model.Inmueble.ImagenPortada = "/" + rutaRelativa.Replace("\\", "/");
                string rutaFisica = Path.Combine(_env.WebRootPath, rutaRelativa);

                Directory.CreateDirectory(Path.GetDirectoryName(rutaFisica));

                using (var stream = new FileStream(rutaFisica, FileMode.Create))
                {
                    await FotoPortada.CopyToAsync(stream);
                }
            }
            else
            {
                model.Inmueble.ImagenPortada = rutaOriginalPortada;
            }

            // ✅ Guardar imágenes del carrusel
            if (FotosCarrusel != null && FotosCarrusel.Any())
            {
                foreach (var foto in FotosCarrusel)
                {
                    if (foto != null && foto.Length > 0)
                    {
                        string extension = Path.GetExtension(foto.FileName);
                        string nombreArchivo = Path.GetFileNameWithoutExtension(foto.FileName);
                        string nombreUnico = $"{nombreArchivo}_{Guid.NewGuid()}{extension}";
                        string rutaRelativa = Path.Combine("Imagenes", "Carrusel", nombreUnico);
                        string rutaFisica = Path.Combine(_env.WebRootPath, rutaRelativa);

                        Directory.CreateDirectory(Path.GetDirectoryName(rutaFisica));

                        using (var stream = new FileStream(rutaFisica, FileMode.Create))
                        {
                            await foto.CopyToAsync(stream);
                        }

                        conexionDB.InsertarFotoCarrusel(model.Inmueble.IdInmueble, "/" + rutaRelativa.Replace("\\", "/"));
                    }
                }
            }

            // ✅ Actualizar el inmueble
            conexionDB.ActualizarInmueble(model.Inmueble);

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error al guardar imagen: " + ex.Message);
            ModelState.AddModelError("", "Ocurrió un error al guardar las imágenes.");
            return View(model);
        }
    }

    return View(model);
}




[HttpPost]
public JsonResult EliminarFotoCarrusel(int idFoto)
{
    System.Diagnostics.Debug.WriteLine("ID RECIBIDO: " + idFoto);

    var foto = conexionDB.ObtenerFotoCarruselPorId(idFoto);

    if (foto != null)
    {
        // Ruta relativa sin el "~", reemplazada por "/"
        var rutaRelativa = foto.RutaFoto.StartsWith("~") ? foto.RutaFoto.Substring(1) : foto.RutaFoto;
        var rutaFisica = Path.Combine(_env.WebRootPath, rutaRelativa.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (System.IO.File.Exists(rutaFisica))
        {
            System.IO.File.Delete(rutaFisica);
        }

        conexionDB.EliminarFotoCarrusel(idFoto);

        return Json(new { success = true });
    }

    return Json(new { success = false, mensaje = "No se encontró la foto." });
}






// GET: Inmuebles/Delete/5
public IActionResult Delete(int id)
{
    var inmueble = conexionDB.ObtenerInmueblePorId(id);
    if (inmueble == null)
    {
        return NotFound();
    }
    return View(inmueble);
}

// POST: Inmuebles/Delete/5
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id) 
{
    conexionDB.EliminarInmueble(id);
    return RedirectToAction("Index");
}


         [HttpGet]
        public JsonResult BuscarPorDni(string dni)
        {
            var inmuebles = conexionDB.ObtenerInmuebles()
                .Where(i => i.DniPropietario == dni)
                .ToList();

            return Json(inmuebles);
        }

       [HttpGet]
public ActionResult ObtenerDireccionesPorDni(string dni)
{
    var inmuebles = conexionDB.ObtenerInmueblesPorDni(dni);

   var direcciones = inmuebles.Select(i => new
{
    id = i.IdInmueble,
    direccion = $"{i.Calle ?? ""} {i.Nro} " +
                $"{(i.Piso != 0 ? $"Piso {i.Piso} " : "")}" +
                $"{(!string.IsNullOrEmpty(i.Dpto) ? $"Dto. {i.Dpto} " : "")}" +
                $", {i.Localidad ?? ""}"
}).ToList();

    return Json(direcciones);  // No se necesita JsonRequestBehavior en ASP.NET Core
}

[HttpGet]
public JsonResult Ocupados(DateTime desde, DateTime hasta)
{
    try
    {
        var lista = conexionDB.ObtenerInmueblesOcupados(desde, hasta);
                    
        return Json(lista ?? new List<Models.Inmueble>());
    }
    catch (Exception ex)
    {
        return Json(new { error = ex.Message });
    }
}


    }

    public class HttpPostedFileBase
    {
    }
}
