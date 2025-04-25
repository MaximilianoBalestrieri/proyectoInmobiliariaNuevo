using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoInmobiliariaNuevo.Models
{
	
        public class Pago
        {
            public int IdPago { get; set; }
            public int IdContrato { get; set; }
            public int NroPago { get; set; }
            public DateTime FechaPago { get; set; }
            public decimal Importe { get; set; }
            public string Detalle { get; set; }
            public string Estado { get; set; }

        }

    
}