using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoInmobiliariaNuevo.Models
{
	public class Contrato
	{
        public int IdContrato { get; set; }
        public string DniPropietario { get; set; }
        public string NombrePropietario { get; set; }
        public string DniInquilino { get; set; }
        public string NombreInquilino { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Monto { get; set; }
        public int IdInmueble { get; set; }
        public string Direccion { get; set; }
        public bool Vigente { get; set; }
        public string? DireccionSeleccionada { get; set; }


    }

    public class NuevaRenovacionModel
    {
        public Contrato NuevoContrato { get; set; }
        public int ContratoAnteriorId { get; set; }
    }



}