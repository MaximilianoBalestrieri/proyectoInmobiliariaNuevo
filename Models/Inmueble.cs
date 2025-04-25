using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace proyectoInmobiliariaNuevo.Models
{
    public class Inmueble
    {
        public int IdInmueble { get; set; }

        [Required(ErrorMessage = "El DNI del propietario es obligatorio")]
        public string DniPropietario { get; set; }

        [Required(ErrorMessage = "La calle es obligatoria")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El número es obligatorio")]
        public int Nro { get; set; }

        public int? Piso { get; set; }
        public string Dpto { get; set; }

        [Required(ErrorMessage = "La localidad es obligatoria")]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "La provincia es obligatoria")]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "El uso es obligatorio")]
        public string Uso { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Tipo { get; set; }

        public int Ambientes { get; set; }
        public bool Pileta { get; set; }
        public bool Parrilla { get; set; }
        public bool Garage { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        public double Precio { get; set; }
        
        public bool Vigente { get; set; }
        
        public string ImagenPortada { get; set; }

        // Si querés mantener estas listas:
        public List<string> FotosCarruselLista { get; set; }
        public List<string> ImagenesCarrusel { get; set; }

        // Si querés guardar una cadena de rutas para insertarlas luego
        public string FotosCarrusel { get; set; } // <-- AGREGAR ESTO si lo vas a usar como string con ;

        


    }



    public class InmuebleFotoCarrusel
    {
        public int Id { get; set; }
        public int IdInmueble { get; set; }
        public string RutaFoto { get; set; }
    }

    public class ImagenCarrusel
    {
        public int Id { get; set; }
        public string RutaFoto { get; set; }
    }



    public class InmuebleDTO
    {
        public int IdInmueble { get; set; }
        public string DniPropietario { get; set; }
        public string Calle { get; set; }
        public int Nro { get; set; }
        public int Piso { get; set; }
        public string Dpto { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public double Precio { get; set; }
        public bool Vigente { get; set; }
    }

}
