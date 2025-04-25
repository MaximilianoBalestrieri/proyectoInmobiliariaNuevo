using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoInmobiliariaNuevo.Models
{
    public class InmuebleEditViewModel
    {
        public Inmueble Inmueble { get; set; }
        public List<InmuebleFotoCarrusel> RutasCarrusel { get; set; } // Lista de fotos del carrusel
        public bool Vigente { get; set; }
    }


    public class FotoCarrusel
    {
        public int Id { get; set; }
        public int IdInmueble { get; set; }
        public string RutaFoto { get; set; }
    }
}