using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace MonitoreoGUI
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioLinea.InnerText = Session["usuario"].ToString();
            if (!IsPostBack)
            {
                try
                {
                    CargarServidores();
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        private void CargarServidores()
        {
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.
                    GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/Servidores");
                });

                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var task2 = Task<string>.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });

                    string resultado = task2.Result;
                    List<Servidor> servidores = Servidor.FromJson(resultado);
                    List<string> temp = new List<string>();

                    foreach (Servidor x in servidores)
                    {
                        temp.Add(x.Codigo.ToString());
                    }

                    ddlServidores.DataSource = temp;
                    ddlServidores.DataBind();

                    if (temp.Count > 0)
                    {
                        CargarMonitoreos(temp);
                    }
                }
            }
        }

        private void CargarMonitoreos(List<string> servidores)
        {
            DataSet dataSet = new DataSet();
            DataTable tabla = EstructuraDashboard();
            foreach (string ser in servidores)
            {
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.
                        GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/ProcedimientosController/Dashboard/" + ser);
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
                        Session["dashb"] = dash;
                        DataRow fila = tabla.NewRow();
                        fila[0] = dash.Codigo;
                        fila[1] = dash.Nombre;
                        fila[2] = dash.Fecha;
                        fila[3] = dash.Estado;
                        fila[4] = dash.Cpu;
                        fila[5] = dash.Memoria;
                        fila[6] = dash.Disco;
                        tabla.Rows.Add(fila);
                    }
                }
            }
            dataSet.Tables.Add(tabla);
            gridMonitoreos.DataSource = dataSet;
            gridMonitoreos.DataBind();
            CheckErrores();
        }

        private void CargarEncargados(string server)
        {
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.
                    GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                "/api/ProcedimientosController/Dashboard/" + server);
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
                    if (dash.Encargados != null)
                    {
                        gridEncargados.DataSource = dash.Encargados;
                        gridEncargados.DataBind();
                        //gridEncargados.Columns[0].HeaderText = "Nombre";
                    }
                    else
                    {
                        lblError.Text = "El servidor no presenta problemas";
                    }
                }
                else
                {
                    lblError.Text = "El servidor no presenta monitoreos";
                }
            }
        }

        private DataTable EstructuraDashboard()
        {
            DataTable table = new DataTable("Dashboard");

            DataColumn columna = 
                new DataColumn("Servidor", System.Type.GetType("System.Int32"));
            columna.Caption = "Servidor";
            table.Columns.Add(columna);

            columna =
                new DataColumn("Nombre", System.Type.GetType("System.String"));
            columna.Caption = "Nombre";
            table.Columns.Add(columna);

            columna =
                new DataColumn("Último_monitoreo", System.Type.GetType("System.String"));
            columna.Caption = "Último monitoreo";
            table.Columns.Add(columna);

            columna =
                new DataColumn("Estado", System.Type.GetType("System.String"));
            columna.Caption = "Estado";
            table.Columns.Add(columna);

            columna =
                new DataColumn("CPU", System.Type.GetType("System.Double"));
            columna.Caption = "CPU";
            table.Columns.Add(columna);

            columna =
                new DataColumn("Memoria", System.Type.GetType("System.Double"));
            columna.Caption = "Memoria";
            table.Columns.Add(columna);

            columna =
                new DataColumn("Disco", System.Type.GetType("System.Double"));
            columna.Caption = "Disco";
            table.Columns.Add(columna);

            return table;
        }

        protected void btnEncargados_Click(object sender, EventArgs e)
        {
            CargarEncargados(ddlServidores.SelectedValue);
        }

        private void CheckErrores()
        {
            foreach (GridViewRow row in gridMonitoreos.Rows)
            {
                if (row.Cells[3].Text.Equals("Error"))
                {
                    row.Cells[3].ForeColor = System.Drawing.Color.Red;
                    row.Cells[3].Font.Bold = true;
                }
            } 
        }

        protected void btnServicio_Click(object sender, EventArgs e)
        {
            Session["servidorDetalle"] = ddlServidores.SelectedValue;
            foreach (GridViewRow row in gridMonitoreos.Rows)
            {
                if (row.Cells[0].Text.Equals(ddlServidores.SelectedValue))
                {
                    Session["servidorCPU"] = row.Cells[4].Text;
                    Session["servidorMem"] = row.Cells[5].Text;
                    Session["servidorDisco"] = row.Cells[6].Text;
                    Session["servidorEstado"] = row.Cells[3].Text;
                    break;
                }
            }
            Response.Redirect("DashboardDetallado.aspx");
        }

        protected void btnNotOn_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlServidores.Items.Count > 0)
            {
                string serNot = ddlServidores.SelectedValue;
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.
                        PutAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/ProcedimientosController/Notificaciones/" +
                    1 + '/' + Session["usuario"].ToString() + '/' +
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
            if (ddlServidores.Items.Count > 0)
            {
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.
                        PutAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/ProcedimientosController/Notificaciones/" +
                    1 + '/' + Session["usuario"].ToString() + '/' +
                    ddlServidores.SelectedValue + '/' + 0, null);
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

        protected void btnMail_Click(object sender, ImageClickEventArgs e)
        {
            string serverMail = ddlServidores.SelectedValue;
            List<string> correos = null;
            string nombre = "";
            try
            {
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.
                        PutAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/ProcedimientosController/Correo/" + 1 +'/'+ serverMail, null);
                    });
                    HttpResponseMessage message = task.Result;
                    if (message.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var task2 = Task<string>.Run(async () =>
                        {
                            return await message.Content.ReadAsStringAsync();
                        });
                        string resultado = task2.Result;
                        correos = Correos.FromJson(resultado);
                    }
                    var task3 = Task.Run(async () =>
                    {
                        return await client.
                        GetAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                    "/api/Servidores");
                    });
                    HttpResponseMessage message2 = task3.Result;
                    if (message2.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var task2 = Task<string>.Run(async () =>
                        {
                            return await message2.Content.ReadAsStringAsync();
                        });
                        string resultado = task2.Result;
                        List<Servidor> todos = Servidor.FromJson(resultado);
                        foreach (Servidor s in todos)
                        {
                            if (s.Codigo == Int32.Parse(serverMail))
                            {
                                nombre = s.Nombre;
                                break;
                            }
                        }
                        Objeto ob = new Objeto()
                        {
                            NombreServidor = nombre,
                            Encargados = correos
                        };
                        string json = ob.ToJson();
                        var task4 = Task.Run(async () =>
                        {
                            return await client.
                            PostAsync("https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                        "/api/Procedimientos2Controller/EnvioCorreo",
                        new StringContent(json, Encoding.UTF8, "application/json"));
                        });
                        HttpResponseMessage message3 = task4.Result;
                        if (message3.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            lblError.Text = "Correos enviados";
                        }
                        else
                        {
                            lblError.Text = message3.StatusCode.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}