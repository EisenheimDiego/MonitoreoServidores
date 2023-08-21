using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonitoreoGUI
{
    public partial class DashboardDetallado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioLinea.InnerText = Session["usuario"].ToString();
            codServ.InnerText = "Codigo: " + Session["servidorDetalle"].ToString();
            porCPU.InnerText = Session["servidorCPU"].ToString() + "%";
            porMem.InnerText = Session["servidorMem"].ToString() + "%";
            porDisco.InnerText = Session["servidorDisco"].ToString() + "%";
            estadoServ.InnerText = Session["servidorEstado"].ToString();
            if (estadoServ.InnerText.Equals("Error"))
            {
                estadoServ.Attributes["class"] = "card rojo";
            }
            else if (estadoServ.InnerText.Equals("Advertencia")){
                estadoServ.Attributes["class"] = "card amarillo";
            }
            else if (estadoServ.InnerText.Equals("Normal")){
                estadoServ.Attributes["class"] = "card verde";
            }

            if(!IsPostBack)
                Servicios();
            SetBoxes();
        }

        private void Servicios()
        {
            DataSet dataSet = new DataSet();
            DataTable tabla = EstructuraServicios();

            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.
                    GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                "/api/ProcedimientosController/Dashboard/" + 
                Session["servidorDetalle"].ToString());
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultado = task2.Result;
                    DashboardResponse dash = DashboardResponse.FromJson(resultado);
                    DataRow fila;
                    List<string> servs = new List<string>();
                    foreach (var x in dash.MonitoreoServicios)
                    {
                        fila = tabla.NewRow();
                        fila[0] = x.Nombre;
                        fila[1] = x.Estado;
                        tabla.Rows.Add(fila);
                        servs.Add(x.Nombre);
                    }
                    dataSet.Tables.Add(tabla);
                    servicios.DataSource = dataSet;
                    servicios.DataBind();
                    ddlServicios.DataSource = servs;
                    ddlServicios.DataBind();
                    CheckServicios();
                }
            }
        }

        private void CheckServicios()
        {
            foreach (GridViewRow row in servicios.Rows)
            {
                if (!row.Cells[1].Text.Equals("Disponible"))
                {
                    row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    row.Cells[1].Font.Bold = true;
                }
            }
        }

        private DataTable EstructuraServicios()
        {
            DataTable tabla = new DataTable();

            DataColumn columna =
                new DataColumn("Servicio", System.Type.GetType("System.String"));
            columna.Caption = "Servicio";
            tabla.Columns.Add(columna);

            columna =
                new DataColumn("Estado", System.Type.GetType("System.String"));
            columna.Caption = "Estado";
            tabla.Columns.Add(columna);

            return tabla;
        }

        protected void btnNotOn_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlServicios.Items.Count > 0)
            {
                string serNot = ObtenerCodigo().ToString();
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.
                        PutAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/ProcedimientosController/Notificaciones/" +
                    2 + '/' + Session["usuario"].ToString() + '/' +
                    serNot + '/' + 1, null);
                    });
                    HttpResponseMessage message = task.Result;
                    if (message.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        lblError.Text = "Se activaron las notificaciones";
                    }
                    else
                    {
                        lblError.Text = "Error con la API";
                    }
                }
            }
        }

        protected void btnNotOff_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlServicios.Items.Count > 0)
            {
                string serNot = ObtenerCodigo().ToString();
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.
                        PutAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/ProcedimientosController/Notificaciones/" +
                    2 + '/' + Session["usuario"].ToString() + '/' +
                    serNot + '/' + 0, null);
                    });
                    HttpResponseMessage message = task.Result;
                    if (message.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        lblError.Text = "Se desactivaron las notificaciones";
                    }
                    else
                    {
                        lblError.Text = "Error con la API";
                    }
                }
            }
        }

        private int ObtenerCodigo()
        {
            string buscar = ddlServicios.SelectedValue;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.
                    GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                "/api/Servicios");
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultado = task2.Result;
                    List<Servicio> todos = Servicio.FromJson(resultado);
                    foreach (Servicio s in todos)
                    {
                        if (s.Nombre.Equals(buscar))
                        {
                            return (int)s.CodigoServicio;
                        }
                    }
                }
            }
            return -1;
        }

        private void SetBoxes()
        {
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.
                    GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                "/api/UmbralComponentes");
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string resultado = task2.Result;
                    List<UmbralComponente> compos = UmbralComponente.FromJson(resultado);
                    foreach (UmbralComponente item in compos)
                    {
                        if(item.Codigo == Int32.Parse(Session["servidorDetalle"].ToString()))
                        {
                            if (item.CodigoC == 1) //CPU
                            {
                                if (item.CodigoUmbral == 1) //ADVERTENCIA
                                {
                                    if (item.Porcentaje < Double.Parse(Session["servidorCPU"].ToString()))
                                    {
                                        CPU.Attributes["class"] = "yellow";
                                    }
                                    else
                                    {
                                        CPU.Attributes["class"] = "green";
                                    }
                                }
                                if (item.CodigoUmbral == 2) //ERROR
                                {
                                    if (item.Porcentaje < Double.Parse(Session["servidorCPU"].ToString()))
                                    {
                                        CPU.Attributes["class"] = "red";
                                    }
                                }
                            }
                            if (item.CodigoC == 2) //MEMORIA
                            {
                                if (item.CodigoUmbral == 1) //ADVERTENCIA
                                {
                                    if (item.Porcentaje < Double.Parse(Session["servidorMem"].ToString()))
                                    {
                                        Memoria.Attributes["class"] = "yellow";
                                    }
                                    else
                                    {
                                        Memoria.Attributes["class"] = "green";
                                    }
                                }
                                if (item.CodigoUmbral == 2) //ERROR
                                {
                                    if (item.Porcentaje < Double.Parse(Session["servidorMem"].ToString()))
                                    {
                                        Memoria.Attributes["class"] = "red";
                                    }
                                }
                            }
                            if (item.CodigoC == 3) //DISCO
                            {
                                if (item.CodigoUmbral == 1) //ADVERTENCIA
                                {
                                    if (item.Porcentaje < Double.Parse(Session["servidorDisco"].ToString()))
                                    {
                                        disco.Attributes["class"] = "yellow";
                                    }
                                    else
                                    {
                                        disco.Attributes["class"] = "green";
                                    }
                                }
                                if (item.CodigoUmbral == 2) //Error
                                {
                                    if (item.Porcentaje < Double.Parse(Session["servidorDisco"].ToString()))
                                    {
                                        disco.Attributes["class"] = "red";
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}