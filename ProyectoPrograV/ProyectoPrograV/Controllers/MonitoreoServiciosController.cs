using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ProyectoPrograV.DataAccess;

namespace ProyectoPrograV.Controllers
{
    public class MonitoreoServiciosController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/MonitoreoServicios
        public IQueryable<MonitoreoServicio> GetMonitoreoServicio()
        {
            return db.MonitoreoServicio;
        }

        // GET: api/MonitoreoServicios/5
        [ResponseType(typeof(MonitoreoServicio))]
        public async Task<IHttpActionResult> GetMonitoreoServicio(int id)
        {
            MonitoreoServicio monitoreoServicio = await db.MonitoreoServicio.FindAsync(id);
            if (monitoreoServicio == null)
            {
                return NotFound();
            }

            return Ok(monitoreoServicio);
        }

        // PUT: api/MonitoreoServicios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMonitoreoServicio(int id, MonitoreoServicio monitoreoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != monitoreoServicio.MonitoreoID)
            {
                return BadRequest();
            }

            db.Entry(monitoreoServicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonitoreoServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MonitoreoServicios
        [ResponseType(typeof(MonitoreoServicio))]
        public async Task<IHttpActionResult> PostMonitoreoServicio(MonitoreoServicio monitoreoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MonitoreoServicio.Add(monitoreoServicio);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = monitoreoServicio.MonitoreoID }, monitoreoServicio);
        }

        // DELETE: api/MonitoreoServicios/5
        [ResponseType(typeof(MonitoreoServicio))]
        public async Task<IHttpActionResult> DeleteMonitoreoServicio(int id)
        {
            MonitoreoServicio monitoreoServicio = await db.MonitoreoServicio.FindAsync(id);
            if (monitoreoServicio == null)
            {
                return NotFound();
            }

            db.MonitoreoServicio.Remove(monitoreoServicio);
            await db.SaveChangesAsync();

            return Ok(monitoreoServicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonitoreoServicioExists(int id)
        {
            return db.MonitoreoServicio.Count(e => e.MonitoreoID == id) > 0;
        }
    }
}