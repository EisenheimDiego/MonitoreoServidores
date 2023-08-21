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
    public class EncargadoServidoresController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/EncargadoServidores
        public IQueryable<EncargadoServidor> GetEncargadoServidor()
        {
            return db.EncargadoServidor;
        }

        // GET: api/EncargadoServidores/5
        [ResponseType(typeof(EncargadoServidor))]
        public async Task<IHttpActionResult> GetEncargadoServidor(string id)
        {
            EncargadoServidor encargadoServidor = await db.EncargadoServidor.FindAsync(id);
            if (encargadoServidor == null)
            {
                return NotFound();
            }

            return Ok(encargadoServidor);
        }

        // PUT: api/EncargadoServidores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEncargadoServidor(string id, EncargadoServidor encargadoServidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encargadoServidor.Usuario)
            {
                return BadRequest();
            }

            db.Entry(encargadoServidor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncargadoServidorExists(id))
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

        // POST: api/EncargadoServidores
        [ResponseType(typeof(EncargadoServidor))]
        public async Task<IHttpActionResult> PostEncargadoServidor(EncargadoServidor encargadoServidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EncargadoServidor.Add(encargadoServidor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EncargadoServidorExists(encargadoServidor.Usuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = encargadoServidor.Usuario }, encargadoServidor);
        }

        // DELETE: api/EncargadoServidores/5
        [ResponseType(typeof(EncargadoServidor))]
        public async Task<IHttpActionResult> DeleteEncargadoServidor(string id)
        {
            EncargadoServidor encargadoServidor = await db.EncargadoServidor.FindAsync(id);
            if (encargadoServidor == null)
            {
                return NotFound();
            }

            db.EncargadoServidor.Remove(encargadoServidor);
            await db.SaveChangesAsync();

            return Ok(encargadoServidor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncargadoServidorExists(string id)
        {
            return db.EncargadoServidor.Count(e => e.Usuario == id) > 0;
        }
    }
}