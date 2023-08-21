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
    public class ParametroServiciosController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/ParametroServicios
        public IQueryable<ParametroServicio> GetParametroServicio()
        {
            return db.ParametroServicio;
        }

        // GET: api/ParametroServicios/5
        [ResponseType(typeof(ParametroServicio))]
        public async Task<IHttpActionResult> GetParametroServicio(int id)
        {
            ParametroServicio parametroServicio = await db.ParametroServicio.FindAsync(id);
            if (parametroServicio == null)
            {
                return NotFound();
            }

            return Ok(parametroServicio);
        }

        // PUT: api/ParametroServicios/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParametroServicio(int id, ParametroServicio parametroServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parametroServicio.CodigoP)
            {
                return BadRequest();
            }

            db.Entry(parametroServicio).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametroServicioExists(id))
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

        // POST: api/ParametroServicios
        [ResponseType(typeof(ParametroServicio))]
        public async Task<IHttpActionResult> PostParametroServicio(ParametroServicio parametroServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ParametroServicio.Add(parametroServicio);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParametroServicioExists(parametroServicio.CodigoP))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = parametroServicio.CodigoP }, parametroServicio);
        }

        // DELETE: api/ParametroServicios/5
        [ResponseType(typeof(ParametroServicio))]
        public async Task<IHttpActionResult> DeleteParametroServicio(int id)
        {
            ParametroServicio parametroServicio = await db.ParametroServicio.FindAsync(id);
            if (parametroServicio == null)
            {
                return NotFound();
            }

            db.ParametroServicio.Remove(parametroServicio);
            await db.SaveChangesAsync();

            return Ok(parametroServicio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParametroServicioExists(int id)
        {
            return db.ParametroServicio.Count(e => e.CodigoP == id) > 0;
        }
    }
}