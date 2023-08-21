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
    public class EncargadoServiciosController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/EncargadoServicios
        public IQueryable<EncargadoServicio> GetEncargadoServicio()
        {
            return db.EncargadoServicio;
        }

        // GET: api/EncargadoServicios/5
        [ResponseType(typeof(EncargadoServicio))]
        public async Task<IHttpActionResult> GetEncargadoServicio(string id)
        {
            EncargadoServicio encargadoServicio = await db.EncargadoServicio.FindAsync(id);
            if (encargadoServicio == null)
            {
                return NotFound();
            }

            return Ok(encargadoServicio);
        }

        // PUT: api/EncargadoServicios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEncargadoServicio(string id, EncargadoServicio encargadoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != encargadoServicio.Usuario)
            {
                return BadRequest();
            }

            db.Entry(encargadoServicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncargadoServicioExists(id))
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

        // POST: api/EncargadoServicios
        [ResponseType(typeof(EncargadoServicio))]
        public async Task<IHttpActionResult> PostEncargadoServicio(EncargadoServicio encargadoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EncargadoServicio.Add(encargadoServicio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EncargadoServicioExists(encargadoServicio.Usuario))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = encargadoServicio.Usuario }, encargadoServicio);
        }

        // DELETE: api/EncargadoServicios/5
        [ResponseType(typeof(EncargadoServicio))]
        public async Task<IHttpActionResult> DeleteEncargadoServicio(string id)
        {
            EncargadoServicio encargadoServicio = await db.EncargadoServicio.FindAsync(id);
            if (encargadoServicio == null)
            {
                return NotFound();
            }

            db.EncargadoServicio.Remove(encargadoServicio);
            await db.SaveChangesAsync();

            return Ok(encargadoServicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EncargadoServicioExists(string id)
        {
            return db.EncargadoServicio.Count(e => e.Usuario == id) > 0;
        }
    }
}