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
        // Aquí puedes obtener los inmuebles desde la base de datos, por ejemplo:
        var inmuebles = conexionDB.ObtenerInmuebles();  // Suponiendo que tienes este método en ConexionDB

        return View(inmuebles);  // Devuelves la lista de inmuebles a la vista
    }

  // GET: Inmuebles/Create
  public ActionResult Create()
  {
      return View();
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
        ViewBag.Mensaje = "🏡 entro al edit de inmueble.";
        if (ModelState.IsValid)
        {
            try
            {
                string rutaOriginalPortada = model.Inmueble.ImagenPortada;

                // ✅ Procesar imagen de portada
                if (FotoPortada != null && FotoPortada.Length > 0)
                {
                    if (!string.IsNullOrEmpty(model.Inmueble.ImagenPortada))
                    {
                        string rutaAnterior = Path.Combine(_env.WebRootPath, model.Inmueble.ImagenPortada.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                        if (System.IO.File.Exists(rutaAnterior))
                        {
                            System.IO.File.Delete(rutaAnterior);
                        }
                    }

                    string extension = Path.GetExtension(FotoPortada.FileName);
                    string nombreArchivo = Path.GetFileNameWithoutExtension(FotoPortada.FileName);
                    string nombreUnico = $"{nombreArchivo}_{Guid.NewGuid()}{extension}";
                    string rutaRelativa = Path.Combine("Imagenes", "Inmuebles", nombreUnico);
                    string rutaFisica = Path.Combine(_env.WebRootPath, rutaRelativa);

                    Directory.CreateDirectory(Path.GetDirectoryName(rutaFisica));

                    using (var stream = new FileStream(rutaFisica, FileMode.Create))
                    {
                        await FotoPortada.CopyToAsync(stream);
                    }

                    model.Inmueble.ImagenPortada = "/" + rutaRelativa.Replace("\\", "/");
                }
                else
                {
                    model.Inmueble.ImagenPortada = rutaOriginalPortada;
                }

                // ✅ Guardar imágenes del carrusel
                if (FotosCarrusel != null)
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


      public int AgregarInmueble(Inmueble inmueble)
{
    int idInmueble = 0;

    using (MySqlConnection conexion = new MySqlConnection(ConexionDB))
    {
        string query = @"
        INSERT INTO Inmueble 
            (DniPropietario, Calle, Nro, Piso, Dpto, Localidad, Provincia, Uso, Tipo, Ambientes, Precio, Latitud, Longitud, Pileta, Parrilla, Garage, ImagenPortada, vigente)
        VALUES 
            (@DniPropietario, @Calle, @Nro, @Piso, @Dpto, @Localidad, @Provincia, @Uso, @Tipo, @Ambientes, @Precio, @Latitud, @Longitud, @Pileta, @Parrilla, @Garage, @ImagenPortada, @vigente);
        SELECT LAST_INSERT_ID();";

        MySqlCommand cmd = new MySqlCommand(query, conexion);

        cmd.Parameters.AddWithValue("@DniPropietario", inmueble.DniPropietario);
        cmd.Parameters.AddWithValue("@Calle", inmueble.Calle);
        cmd.Parameters.AddWithValue("@Nro", inmueble.Nro);
        cmd.Parameters.AddWithValue("@Piso", inmueble.Piso);
        cmd.Parameters.AddWithValue("@Dpto", inmueble.Dpto);
        cmd.Parameters.AddWithValue("@Localidad", inmueble.Localidad);
        cmd.Parameters.AddWithValue("@Provincia", inmueble.Provincia);
        cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
        cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
        cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
        cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
        cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
        cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
        cmd.Parameters.AddWithValue("@Pileta", inmueble.Pileta);
        cmd.Parameters.AddWithValue("@Parrilla", inmueble.Parrilla);
        cmd.Parameters.AddWithValue("@Garage", inmueble.Garage);
        cmd.Parameters.AddWithValue("@ImagenPortada", inmueble.ImagenPortada);
        cmd.Parameters.AddWithValue("@vigente", inmueble.Vigente);

        conexion.Open();
        idInmueble = Convert.ToInt32(cmd.ExecuteScalar());

        // Guardar fotos del carrusel si hay
        if (!string.IsNullOrEmpty(inmueble.FotosCarrusel))
        {
            var rutas = inmueble.FotosCarrusel.Split(';');

            foreach (var ruta in rutas)
            {
                if (!string.IsNullOrWhiteSpace(ruta))
                {
                    var cmdCarrusel = new MySqlCommand(
                        "INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@IdInmueble, @RutaFoto)",
                        conexion);

                    cmdCarrusel.Parameters.AddWithValue("@IdInmueble", idInmueble);
                    cmdCarrusel.Parameters.AddWithValue("@RutaFoto", ruta);
                    cmdCarrusel.ExecuteNonQuery();
                }
            }
        }
    }

    return idInmueble;
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
