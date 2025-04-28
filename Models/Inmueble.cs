using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace proyectoInmobiliariaNuevo.Models
{
    public class Inmueble
{
    public int IdInmueble { get; set; }
    public string Calle { get; set; }
    public int Nro { get; set; }
    public int Piso { get; set; }
    public string Dpto { get; set; }
    public string Localidad { get; set; }
    public string Provincia { get; set; }
    public string Uso { get; set; }
    public string Tipo { get; set; }
    public int Ambientes { get; set; }
    public bool Pileta { get; set; }
    public bool Parrilla { get; set; }
    public bool Garage { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public double Precio { get; set; }
    public string DniPropietario { get; set; }

     public bool Vigente { get; set; }

    [ValidateNever]
    public string ImagenPortada { get; set; }

    [ValidateNever]
    public List<string> FotosCarruselLista { get; set; }

    [ValidateNever]
    public List<string> ImagenesCarrusel { get; set; }

    [ValidateNever]
    public string FotosCarrusel { get; set; }

    [ValidateNever]
    public string Nombre { get; set; }

       
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
