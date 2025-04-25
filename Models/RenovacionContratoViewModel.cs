using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoInmobiliariaNuevo.Models
{
	public class RenovacionContratoViewModel
	{
        public int IdContratoAnterior { get; set; }
        public string DniPropietario { get; set; }
        public string NombrePropietario { get; set; }
        public string DniInquilino { get; set; }
        public string NombreInquilino { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Monto { get; set; }
        public int IdInmueble { get; set; }
        public string Direccion { get; set; }
    }
}