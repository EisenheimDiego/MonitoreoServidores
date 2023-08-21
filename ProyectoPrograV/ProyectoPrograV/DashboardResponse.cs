using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProyectoPrograV.DataAccess;

namespace ProyectoPrograV
{
    public class DashboardResponse
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public double Cpu { get; set; }
        public double Disco { get; set; }
        public double Memoria { get; set; }
        public List<MonitoreoServicioResponse> MonitoreoServicios { get; set; }
        public List<string> Encargados { get; set; }
    }
}