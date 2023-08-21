using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProyectoPrograV.DataAccess;

namespace ProyectoPrograV.Controllers
{
    public class ProcedimientosController : ApiController
    {

        private MonitoreoEntities entities = new MonitoreoEntities();
        public ProcedimientosController()
        {
            entities.Configuration.LazyLoadingEnabled = true;
            entities.Configuration.ProxyCreationEnabled = true;
        }

        [HttpPost]
        [Route("api/ProcedimientosController/Login")]
        public IHttpActionResult InicioSesion([FromBody] Usuarios usuario)
        {
            byte[] encryptionKeyBytes = Encriptar.CreateKey(usuario.Contrasena);
            usuario.Contrasena = System.Text.Encoding.Default.GetString(encryptionKeyBytes);
            ObjectResult<int?> resultado = entities.InicioSesion(usuario.Usuario, usuario.Contrasena);
            if (resultado.ElementAt(0).Value == 1)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/ProcedimientosController/Dashboard/{servidor}")]
        public async Task<IHttpActionResult> Dashboard(int servidor)
        {
            if (!servidor.ToString().All(Char.IsDigit) || servidor == 0)
            {
                return BadRequest();
            }

            ObjectResult<Dashboard_Result> result = entities.Dashboard(servidor);
            DashboardResponse response;
            if (result != null) //SI EXISTE UN MONITOREO
            {
                Dashboard_Result resultado = result.ElementAt(0);
                response = new DashboardResponse() //LO LEO
                {
                    Codigo = resultado.Codigo,
                    Nombre = resultado.Nombre,
                    Fecha = resultado.Ultima_fecha,
                    Estado = resultado.Estado,
                    Cpu = resultado.Uso_de_CPU,
                    Disco = resultado.Uso_de_Disco,
                    Memoria = resultado.Uso_de_Memoria
                };
                if (response.Estado.Equals("Error")) //SI EL SERVIDOR TRAE ERROR
                {
                    var query = from c in entities.EncargadoServidor
                                where c.Codigo == response.Codigo
                                select c.Usuario;
                    //RECUPERO LOS ENCARGADOS DEL SERVIDOR
                    response.Encargados = await query.ToListAsync();
                }
                var query2 = from c in entities.Servicio
                             where c.Codigo == response.Codigo
                             select c;
                List<Servicio> temp = await query2.ToListAsync(); //CONSULTO LOS SERVICIOS

                if (temp != null) //SI EXISTEN SERVICIOS
                {
                    //LISTA TEMPORAL PARA ALMACENAR MONITOREOS DE CADA SERVICIO
                    List<MonitoreoServicio> monitoreoServicios = new List<MonitoreoServicio>();

                    response.MonitoreoServicios = new List<MonitoreoServicioResponse>();

                    foreach (Servicio s in temp) //PARA CADA SERVICIO, LEO MONITOREOS
                    {
                        var query3 = from c in entities.MonitoreoServicio
                                     where c.CodigoServicio == s.CodigoServicio
                                     && c.Fecha == response.Fecha
                                     select c;
                        monitoreoServicios = await query3.ToListAsync();

                        if (monitoreoServicios.Count > 0) //SI TIENE MONITOREOS EL SERVICIO
                        {
                            MonitoreoServicio act = monitoreoServicios.ElementAt(0);
                            //OBTENGO EL NOMBRE DEL SERVICIO
                            var nombreServicio = from c in entities.Servicio
                                                 where c.CodigoServicio == s.CodigoServicio
                                                 select c.Nombre;
                            List<string> nombre = await nombreServicio.ToListAsync();
                            MonitoreoServicioResponse mon = new MonitoreoServicioResponse()
                            {
                                Estado = act.Disponibilidad,
                                Nombre = nombre.ElementAt(0)
                            };
                            if (!mon.Estado.Equals("Disponible"))
                            {
                                var query4 = from c in entities.EncargadoServicio
                                            where c.CodigoServicio == act.CodigoServicio
                                            select c.Usuario;
                                //RECUPERO LOS ENCARGADOS DEL SERVIDOR
                                mon.Encargados = await query4.ToListAsync();
                            }
                            response.MonitoreoServicios.Add(mon);
                        }
                    }
                    return Ok(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("api/ProcedimientosController/ServidorNombre/{nombre}")]
        public IHttpActionResult ServidorNombre(string nombre)
        {
            if (nombre.All(Char.IsDigit) || nombre == null || nombre.Trim().Equals(""))
            {
                return BadRequest();
            }
            ObjectResult<ServidorNombre_Result> result = entities.ServidorNombre(nombre);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("api/ProcedimientosController/ServicioServidor/{servidor}")]
        public IHttpActionResult ServicioServidor(int servidor)
        {
            if (!servidor.ToString().All(Char.IsDigit) || servidor == 0)
            {
                return BadRequest();
            }

            ObjectResult<ServicioServidor_Result> result = entities.ServicioServidor(servidor);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpPut]
        [Route("api/ProcedimientosController/Notificaciones/{tipo}/" +
            "{usuario}/{codigo}/{valor}")]
        public IHttpActionResult Notificaciones(int tipo, string usuario, int codigo, int valor)
        {
            if (!tipo.ToString().All(Char.IsDigit)
                || usuario == null || usuario.Trim().Equals("") ||
                !codigo.ToString().All(Char.IsDigit) || codigo == 0
                || !valor.ToString().All(Char.IsDigit) || valor > 1 || valor < 0)
            {
                return BadRequest();
            }

            if (tipo == 1)
            {
                if (!ServidorExists(codigo))
                {
                    return NotFound();
                }
            }
            else if (!ServicioExists(codigo))
            {
                return NotFound();
            }
            else if (!UsuarioExists(usuario))
            {
                return NotFound();
            }
            entities.Notificaciones(tipo, usuario, codigo, valor);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("api/ProcedimientosController/Correo/{valor}/{codigo}")]
        public IHttpActionResult Correo(int valor, int codigo)
        {
            try
            {
                ObjectResult<string> result = entities.Correos(valor, codigo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        private bool ServicioExists(int id)
        {
            return entities.Servicio.Count(e => e.CodigoServicio == id) > 0;
        }
        private bool ServidorExists(int id)
        {
            return entities.Servidor.Count(e => e.Codigo == id) > 0;
        }

        private bool UsuarioExists(string usuario)
        {
            return entities.Usuarios.Count(e => e.Usuario.Equals(usuario)) > 0;
        }
    }
}
