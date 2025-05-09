using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyectoInmobiliariaNuevo.Models;

public class UsuariosController : Controller
{
    ConexionDB db = new ConexionDB();


private readonly IWebHostEnvironment _env;

public UsuariosController(IWebHostEnvironment env)
{
    _env = env;
}

 public ActionResult Index(string searchString, string sortOrder)
{
    var nombreUsuario = HttpContext.Session.GetString("Usuario");

    if (string.IsNullOrEmpty(nombreUsuario))
        return RedirectToAction("Index", "Login");

    var usuario = db.ObtenerUsuarioPorNombre(nombreUsuario);
    if (usuario == null)
        return RedirectToAction("Index", "Login");

    if (string.IsNullOrEmpty(usuario.FotoPerfil))
        usuario.FotoPerfil = "/imagenes/usuarios/default.png";

    ViewBag.Usuario = usuario.UsuarioNombre;
    ViewBag.FotoPerfil = usuario.FotoPerfil;
    ViewBag.Apellido = usuario.NombreyApellido;
    ViewBag.Rol = usuario.Rol;

    // Para las vistas
    ViewBag.CurrentSort = sortOrder;
    ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
    ViewBag.RolSortParm = sortOrder == "Rol" ? "rol_desc" : "Rol";
    ViewBag.SearchString = searchString;

    // Obtener la lista completa de usuarios
    var lista = db.ObtenerUsuarios();

    // Asegurarse de que todas las fotos por defecto están asignadas solo cuando es necesario
    foreach (var u in lista)
    {
        if (string.IsNullOrEmpty(u.FotoPerfil))
        {
            u.FotoPerfil = "/imagenes/usuarios/default.png";
        }
        else
        {
            Console.WriteLine($"Foto de {u.NombreyApellido}: {u.FotoPerfil}"); // Debug: Ver fotos asignadas
        }
    }

    // Filtrar por nombre y apellido si searchString tiene valor
    if (!string.IsNullOrEmpty(searchString))
    {
        lista = lista.Where(u => u.NombreyApellido != null &&
                                 u.NombreyApellido.ToLower().Contains(searchString.ToLower())).ToList();
        Console.WriteLine($"Buscando por: {searchString}"); // Debug: Ver qué estamos buscando
    }

    // Ordenar por el parámetro seleccionado
    switch (sortOrder)
    {
        case "name_desc":
            lista = lista.OrderByDescending(u => u.NombreyApellido).ToList();
            break;
        case "Rol":
            lista = lista.OrderBy(u => u.Rol).ToList();
            break;
        case "rol_desc":
            lista = lista.OrderByDescending(u => u.Rol).ToList();
            break;
        default:
            lista = lista.OrderBy(u => u.NombreyApellido).ToList();
            break;
    }

    // Debug: Ver la lista resultante
    Console.WriteLine($"Usuarios encontrados: {lista.Count}"); // Debug: Cuántos usuarios quedan después de la búsqueda

    return View(lista);
}



public IActionResult Create()
{
    return View();
}

   [HttpPost]
public async Task<IActionResult> Create(Usuario usuario, IFormFile FotoPerfil)
{
    if (!ModelState.IsValid)
        return View(usuario);

    if (FotoPerfil != null && FotoPerfil.Length > 0)
    {
        var rutaFotos = Path.Combine(_env.WebRootPath, "imagenes", "usuarios");
        Directory.CreateDirectory(rutaFotos); // por si no existe

        string nombreArchivo = Guid.NewGuid() + Path.GetExtension(FotoPerfil.FileName);
        string rutaCompleta = Path.Combine(rutaFotos, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await FotoPerfil.CopyToAsync(stream);
        }

        usuario.FotoPerfil = "/imagenes/usuarios/" + nombreArchivo;
    }

    db.AgregarUsuario(usuario);
    return RedirectToAction("Index");
}

    

   public IActionResult Edit(int id)
{
    var usuario = db.ObtenerUsuarioPorId(id);
    if (usuario == null)
    {
        return NotFound();
    }
    return View(usuario);
}


[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(Usuario usuario, IFormFile? FotoNueva)
{
    if (!ModelState.IsValid)
    {
        return View(usuario);
    }

    // Obtener el usuario original desde la base de datos
    var usuarioOriginal = db.ObtenerUsuarioPorId(usuario.IdUsuario);
    if (usuarioOriginal == null)
    {
        return NotFound();
    }

    // Manejo de la foto nueva
    if (FotoNueva != null && FotoNueva.Length > 0)
    {
        string rutaCarpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes", "usuarios");
        if (!Directory.Exists(rutaCarpeta))
        {
            Directory.CreateDirectory(rutaCarpeta);
        }

        string nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(FotoNueva.FileName);
        string rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            FotoNueva.CopyTo(stream);
        }

        // Asignamos la nueva foto
        usuarioOriginal.FotoPerfil = "/imagenes/usuarios/" + nombreArchivo;
    }

    // Si no hay nueva foto, NO tocamos FotoPerfil (queda la que ya tenía)

    // Actualizamos los demás campos del usuario original
    usuarioOriginal.UsuarioNombre = usuario.UsuarioNombre;
    usuarioOriginal.NombreyApellido = usuario.NombreyApellido;
    usuarioOriginal.Contraseña = usuario.Contraseña;
    usuarioOriginal.Rol = usuario.Rol;

    db.ActualizarUsuario(usuarioOriginal); // Método que hace el UPDATE real

    // Mostramos temporalmente los datos enviados
   // TempData["DebugData"] = $"Usuario: {usuarioOriginal.UsuarioNombre}, Nombre: {usuarioOriginal.NombreyApellido}, Contraseña: {usuarioOriginal.Contraseña}, Rol: {usuarioOriginal.Rol}, Foto: {usuarioOriginal.FotoPerfil}";

    return RedirectToAction("Index");
}

[HttpGet]
public JsonResult BuscarUsuarios(string filtro)
{
    var lista = db.ObtenerUsuarios();

    if (!string.IsNullOrEmpty(filtro))
    {
        lista = lista.Where(u => u.NombreyApellido != null &&
                                 u.NombreyApellido.ToLower().Contains(filtro.ToLower())).ToList();
    }

    var resultado = lista.Select(u => new {
        u.IdUsuario,
        u.UsuarioNombre,
        u.NombreyApellido,
        u.Rol
        // podés agregar más campos si querés
    });

    return Json(resultado);
}


// GET: Usuarios/Delete/5
public ActionResult Delete(int id)
{
    var usuario = db.ObtenerUsuarioPorId(id); // Función que obtiene al usuario
    if (usuario == null)
    {
        // Si el usuario no se encuentra, mostramos un error claro
        TempData["MensajeError"] = "El usuario que intentas eliminar no existe.";
        return RedirectToAction("Index"); // Redirigimos al listado de usuarios
    }
    return View(usuario); // Si existe, cargamos la vista con el usuario
}


// POST: Usuarios/Delete/5
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Delete(Usuario u)
{
    try
    {
        db.EliminarUsuario(u.IdUsuario);
        TempData["Mensaje"] = "Usuario eliminado exitosamente.";
        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        ViewBag.Error = "No se pudo eliminar el usuario: " + ex.Message;
        return View(u);
    }
}




// POST: Usuarios/DeleteConfirmed
[HttpPost]
public ActionResult DeleteConfirmed(int idUsuario)
{
    db.EliminarUsuario(idUsuario);
    return RedirectToAction("Index");
}

[HttpPost]
public async Task<IActionResult> ActualizarPerfil(IFormFile FotoNueva, string Usuario, string NuevaContrasena, string accion)
{
    var rutaFotos = Path.Combine(_env.WebRootPath, "imagenes", "usuarios");
    var usuario = db.ObtenerUsuarioPorNombre(Usuario);

    if (accion == "cargar")
    {
        if (FotoNueva != null && FotoNueva.Length > 0)
        {
            string nombreArchivo = Guid.NewGuid() + Path.GetExtension(FotoNueva.FileName);
            string rutaCompleta = Path.Combine(rutaFotos, nombreArchivo);

            using (var stream = new FileStream(rutaCompleta, FileMode.Create))
            {
                await FotoNueva.CopyToAsync(stream);
            }

            usuario.FotoPerfil = "/imagenes/usuarios/" + nombreArchivo;
            HttpContext.Session.SetString("FotoPerfil", usuario.FotoPerfil);

            db.ActualizarRutaFoto(usuario);

            return Json(new { ok = true, nuevaRuta = usuario.FotoPerfil });
        }

        return Json(new { ok = false, error = "Archivo vacío o nulo" });
    }

    if (accion == "eliminar")
    {
        if (!string.IsNullOrEmpty(usuario.FotoPerfil))
        {
            string rutaAnterior = Path.Combine(_env.WebRootPath, usuario.FotoPerfil.TrimStart('/'));
            if (System.IO.File.Exists(rutaAnterior))
                System.IO.File.Delete(rutaAnterior);
        }

        usuario.FotoPerfil = null;
        db.ActualizarRutaFoto(usuario);

        return Json(new { ok = true });
    }

    // Guardar contraseña normalmente
    if (accion == "guardarPerfil" && !string.IsNullOrEmpty(NuevaContrasena))
    {
        usuario.Contraseña = NuevaContrasena;
        db.ActualizarClave(usuario);
    }

    return RedirectToAction("Index", "Home");
}



}
