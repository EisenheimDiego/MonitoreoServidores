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
    public class Procedimientos2Controller : ApiController
    {

        private MonitoreoEntities entities = new MonitoreoEntities();
        public Procedimientos2Controller()
        {
            entities.Configuration.LazyLoadingEnabled = true;
            entities.Configuration.ProxyCreationEnabled = true;
        }

        [HttpPost]
        [Route("api/Procedimientos2Controller/EnvioCorreo/objeto")]
        public IHttpActionResult EnvioCorreo([FromBody] CorreoObject objeto)
        {
            try
            {
                Correo correo = new Correo();
                correo.Enviar(objeto.NombreServidor, objeto.Encargados);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Procedimientos2Controller/PorcComp/{servidor}/{componente}")]
        public async Task<IHttpActionResult> PorcComp(string servidor, int componente)
        {
            try
            {
                var query = from c in entities.UmbralComponente
                            where c.Codigo.ToString() == servidor && c.CodigoC == componente
                            select c;
                List<UmbralComponente> umbralComponentes = await query.ToListAsync();
                return Ok(umbralComponentes);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("api/Procedimientos2Controller/ServicioEnc/{nombre}")]
        public async Task<IHttpActionResult> ServicioEnc(string nombre)
        {
            var query = from c in entities.Servicio
                        where c.Nombre == nombre
                        select c.CodigoServicio;
            List<int> servicios = await query.ToListAsync();
            return Ok(servicios);
            //Servicio temp = servicios.ElementAt(0);
            //int codigo = temp.CodigoServicio;

            //var query2 = from s in entities.EncargadoServicio
            //             where s.CodigoServicio == codigo
            //             select s;

            //List<EncargadoServicio> encargados = query2.ToList();
            //if (encargados != null)
            //{
            //    return Ok(encargados);
            //}
            //else
            //{
            //    return NotFound();
            //}
        }

        [HttpPost]
        [Route("api/Procedimientos2Controller/Monitorear/objetoM")]
        public async Task<IHttpActionResult> Monitorear([FromBody] Monitoreo monitoreo)
        {
            try
            {
                if (monitoreo.Fecha == null || monitoreo.ServidorId == 0
                    || monitoreo.UsoDeCpu == 0 || monitoreo.UsoDeCpu > 100
                    || monitoreo.UsoDeDisco == 0 || monitoreo.UsoDeDisco > 100
                    || monitoreo.UsoDeMemoria == 0 || monitoreo.UsoDeMemoria > 100)
                {
                    return StatusCode(System.Net.HttpStatusCode.BadRequest);
                }
                if (MonitoreoExists((int)monitoreo.MonitoreoId))
                {
                    return StatusCode(System.Net.HttpStatusCode.Conflict);
                }
                if (!ServidorExists((int)monitoreo.ServidorId))
                {
                    return NotFound();
                }
                foreach (ServicioM x in monitoreo.Servicios)
                {
                    if (!ServicioExists((int)x.ServicioId))
                    {
                        return NotFound();
                    }
                }
                MonitoreoServidor monServ = new MonitoreoServidor()
                {
                    Codigo = (int)monitoreo.ServidorId,
                    Fecha = monitoreo.Fecha,
                    CPU = monitoreo.UsoDeCpu,
                    Disco = monitoreo.UsoDeDisco,
                    Memoria = monitoreo.UsoDeMemoria
                };
                entities.MonitoreoServidor.Add(monServ);
                await entities.SaveChangesAsync();
                foreach (ServicioM s in monitoreo.Servicios)
                {
                    string estado;
                    if (s.CodigoEstado == 1)
                        estado = "Disponible";
                    else estado = "No disponible";
                    MonitoreoServicio monServi = new MonitoreoServicio()
                    {
                        CodigoServicio = (int)s.ServicioId,
                        Disponibilidad = estado,
                        Fecha = monitoreo.Fecha,
                        Timeout = 0
                    };
                    entities.MonitoreoServicio.Add(monServi);
                    await entities.SaveChangesAsync();
                }
                return CreatedAtRoute("DefaultApi", new { id = monitoreo.MonitoreoId }, monitoreo);
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
        private bool MonitoreoExists(int id)
        {
            return entities.MonitoreoServidor.Count(e => e.MonitoreoID == id) > 0;
        }

    }
}
