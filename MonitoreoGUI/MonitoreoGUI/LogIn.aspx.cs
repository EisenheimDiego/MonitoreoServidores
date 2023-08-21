using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MonitoreoGUI
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btnLog_ServerClick(object sender, EventArgs e)
        {
            if (username.Value == null || password.Value == null ||
                username.Value.ToLower().Contains("select") ||
                username.Value.ToLower().Contains("insert")
                || username.Value.ToLower().Contains("delete")
                || password.Value.All(Char.IsDigit)
                || password.Value.ToLower().Contains("select")
                || password.Value.ToLower().Contains("insert")
                || password.Value.ToLower().Contains("delete")
                || password.Value.ToLower().Contains("<script")
                || username.Value.ToLower().Contains("<script")
                || password.Value.Trim() == ""
                || username.Value.Trim() == "")
            {
                lblError.InnerText = "Datos erróneos o faltantes";
                lblError.Visible = true;
            }
            else
            {
                Usuarios usuario = new Usuarios()
                {
                    Usuario = username.Value,
                    Contrasena = password.Value
                };
                if (Validar(usuario))
                {
                    Session["usuario"] = usuario.Usuario;
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    lblError.InnerText = "Usuario y/o contraseña incorrectos";
                    lblError.Visible = true;
                }
            }
        }

        private bool Validar(Usuarios u)
        {
            string json = u.ToJsonString();
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.PostAsync(
                        "https://tiusr5pl.cuc-carrera-ti.ac.cr/APIProyecto" +
                        "/api/ProcedimientosController/LogIn",
                        new StringContent(json, Encoding.UTF8, "application/json"));
                });
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}