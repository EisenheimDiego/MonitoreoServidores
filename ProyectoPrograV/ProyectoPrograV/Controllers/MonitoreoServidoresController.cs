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
    public class MonitoreoServidoresController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/MonitoreoServidores
        public IQueryable<MonitoreoServidor> GetMonitoreoServidor()
        {
            return db.MonitoreoServidor;
        }

        // GET: api/MonitoreoServidores/5
        [ResponseType(typeof(MonitoreoServidor))]
        public async Task<IHttpActionResult> GetMonitoreoServidor(int id)
        {
            MonitoreoServidor monitoreoServidor = await db.MonitoreoServidor.FindAsync(id);
            if (monitoreoServidor == null)
            {
                return NotFound();
            }

            return Ok(monitoreoServidor);
        }

        // PUT: api/MonitoreoServidores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMonitoreoServidor(int id, MonitoreoServidor monitoreoServidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != monitoreoServidor.MonitoreoID)
            {
                return BadRequest();
            }

            db.Entry(monitoreoServidor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MonitoreoServidorExists(id))
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

        // POST: api/MonitoreoServidores
        [ResponseType(typeof(MonitoreoServidor))]
        public async Task<IHttpActionResult> PostMonitoreoServidor(MonitoreoServidor monitoreoServidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MonitoreoServidor.Add(monitoreoServidor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = monitoreoServidor.MonitoreoID }, monitoreoServidor);
        }

        // DELETE: api/MonitoreoServidores/5
        [ResponseType(typeof(MonitoreoServidor))]
        public async Task<IHttpActionResult> DeleteMonitoreoServidor(int id)
        {
            MonitoreoServidor monitoreoServidor = await db.MonitoreoServidor.FindAsync(id);
            if (monitoreoServidor == null)
            {
                return NotFound();
            }

            db.MonitoreoServidor.Remove(monitoreoServidor);
            await db.SaveChangesAsync();

            return Ok(monitoreoServidor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MonitoreoServidorExists(int id)
        {
            return db.MonitoreoServidor.Count(e => e.MonitoreoID == id) > 0;
        }
    }
}