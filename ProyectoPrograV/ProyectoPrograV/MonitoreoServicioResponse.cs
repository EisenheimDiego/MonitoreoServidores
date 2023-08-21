using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoPrograV.DataAccess;

namespace ProyectoPrograV
{
    public class MonitoreoServicioResponse
    {
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public List<string> Encargados {get; set;}
    }
}