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
    public class UmbralesController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/Umbrales
        public IQueryable<Umbral> GetUmbral()
        {
            return db.Umbral;
        }

        // GET: api/Umbrales/5
        [ResponseType(typeof(Umbral))]
        public async Task<IHttpActionResult> GetUmbral(int id)
        {
            Umbral umbral = await db.Umbral.FindAsync(id);
            if (umbral == null)
            {
                return NotFound();
            }

            return Ok(umbral);
        }

        // PUT: api/Umbrales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUmbral(int id, Umbral umbral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != umbral.CodigoUmbral)
            {
                return BadRequest();
            }

            db.Entry(umbral).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UmbralExists(id))
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

        // POST: api/Umbrales
        [ResponseType(typeof(Umbral))]
        public async Task<IHttpActionResult> PostUmbral(Umbral umbral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Umbral.Add(umbral);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UmbralExists(umbral.CodigoUmbral))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = umbral.CodigoUmbral }, umbral);
        }

        // DELETE: api/Umbrales/5
        [ResponseType(typeof(Umbral))]
        public async Task<IHttpActionResult> DeleteUmbral(int id)
        {
            Umbral umbral = await db.Umbral.FindAsync(id);
            if (umbral == null)
            {
                return NotFound();
            }

            db.Umbral.Remove(umbral);
            await db.SaveChangesAsync();

            return Ok(umbral);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UmbralExists(int id)
        {
            return db.Umbral.Count(e => e.CodigoUmbral == id) > 0;
        }
    }
}