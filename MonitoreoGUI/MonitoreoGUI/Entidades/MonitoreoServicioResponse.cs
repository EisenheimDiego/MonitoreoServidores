using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoreoGUI
{
    public class MonitoreoServicioResponse
    {
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public List<string> Encargados {get; set;}
    }
}