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
    public class ParametrosController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/Parametros
        public IQueryable<Parametro> GetParametro()
        {
            return db.Parametro;
        }

        // GET: api/Parametros/5
        [ResponseType(typeof(Parametro))]
        public async Task<IHttpActionResult> GetParametro(int id)
        {
            Parametro parametro = await db.Parametro.FindAsync(id);
            if (parametro == null)
            {
                return NotFound();
            }

            return Ok(parametro);
        }

        // PUT: api/Parametros/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParametro(int id, Parametro parametro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametro.CodigoP)
            {
                return BadRequest();
            }

            db.Entry(parametro).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametroExists(id))
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

        // POST: api/Parametros
        [ResponseType(typeof(Parametro))]
        public async Task<IHttpActionResult> PostParametro(Parametro parametro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parametro.Add(parametro);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParametroExists(parametro.CodigoP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parametro.CodigoP }, parametro);
        }

        // DELETE: api/Parametros/5
        [ResponseType(typeof(Parametro))]
        public async Task<IHttpActionResult> DeleteParametro(int id)
        {
            Parametro parametro = await db.Parametro.FindAsync(id);
            if (parametro == null)
            {
                return NotFound();
            }

            db.Parametro.Remove(parametro);
            await db.SaveChangesAsync();

            return Ok(parametro);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParametroExists(int id)
        {
            return db.Parametro.Count(e => e.CodigoP == id) > 0;
        }
    }
}