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
    public class ServidoresController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/Servidores
        public IQueryable<Servidor> GetServidor()
        {
            return db.Servidor;
        }

        // GET: api/Servidores/5
        [ResponseType(typeof(Servidor))]
        public async Task<IHttpActionResult> GetServidor(int id)
        {
            Servidor servidor = await db.Servidor.FindAsync(id);
            if (servidor == null)
            {
                return NotFound();
            }

            return Ok(servidor);
        }

        // PUT: api/Servidores/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutServidor(int id, Servidor servidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servidor.Codigo)
            {
                return BadRequest();
            }

            db.Entry(servidor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServidorExists(id))
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

        // POST: api/Servidores
        [ResponseType(typeof(Servidor))]
        public async Task<IHttpActionResult> PostServidor(Servidor servidor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Servidor.Add(servidor);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServidorExists(servidor.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = servidor.Codigo }, servidor);
        }

        // DELETE: api/Servidores/5
        [ResponseType(typeof(Servidor))]
        public async Task<IHttpActionResult> DeleteServidor(int id)
        {
            Servidor servidor = await db.Servidor.FindAsync(id);
            if (servidor == null)
            {
                return NotFound();
            }

            db.Servidor.Remove(servidor);
            await db.SaveChangesAsync();

            return Ok(servidor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServidorExists(int id)
        {
            return db.Servidor.Count(e => e.Codigo == id) > 0;
        }
    }
}