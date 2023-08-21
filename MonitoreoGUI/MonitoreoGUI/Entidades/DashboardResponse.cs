using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MonitoreoGUI
{
    public partial class DashboardResponse
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
    public partial class DashboardResponse
    {
        public static DashboardResponse FromJson(string json) => JsonConvert.DeserializeObject<DashboardResponse>(json, MonitoreoGUI.Converter.Settings);
    }

    public static class SerializeDashboardResponse
    {
        public static string ToJson(this List<Servidor> self) => JsonConvert.SerializeObject(self, MonitoreoGUI.Converter.Settings);
    }

    internal static class ConverterDashboardResponse
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}