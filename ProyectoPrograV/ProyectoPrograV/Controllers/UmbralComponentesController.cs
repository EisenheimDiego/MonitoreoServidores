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
    public class UmbralComponentesController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/UmbralComponentes
        public IQueryable<UmbralComponente> GetUmbralComponente()
        {
            return db.UmbralComponente;
        }

        // GET: api/UmbralComponentes/5
        [ResponseType(typeof(UmbralComponente))]
        public async Task<IHttpActionResult> GetUmbralComponente(int id)
        {
            UmbralComponente umbralComponente = await db.UmbralComponente.FindAsync(id);
            if (umbralComponente == null)
            {
                return NotFound();
            }

            return Ok(umbralComponente);
        }

        // PUT: api/UmbralComponentes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUmbralComponente(int id, UmbralComponente umbralComponente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != umbralComponente.Codigo)
            {
                return BadRequest();
            }

            db.Entry(umbralComponente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UmbralComponenteExists(id))
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

        // POST: api/UmbralComponentes
        [ResponseType(typeof(UmbralComponente))]
        public async Task<IHttpActionResult> PostUmbralComponente(UmbralComponente umbralComponente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UmbralComponente.Add(umbralComponente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UmbralComponenteExists(umbralComponente.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = umbralComponente.Codigo }, umbralComponente);
        }

        // DELETE: api/UmbralComponentes/5
        [ResponseType(typeof(UmbralComponente))]
        public async Task<IHttpActionResult> DeleteUmbralComponente(int id)
        {
            UmbralComponente umbralComponente = await db.UmbralComponente.FindAsync(id);
            if (umbralComponente == null)
            {
                return NotFound();
            }

            db.UmbralComponente.Remove(umbralComponente);
            await db.SaveChangesAsync();

            return Ok(umbralComponente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UmbralComponenteExists(int id)
        {
            return db.UmbralComponente.Count(e => e.Codigo == id) > 0;
        }
    }
}