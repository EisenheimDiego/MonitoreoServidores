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
    public class ComponentesController : ApiController
    {
        private MonitoreoEntities db = new MonitoreoEntities();

        // GET: api/Componentes
        public IQueryable<Componente> GetComponente()
        {
            return db.Componente;
        }

        // GET: api/Componentes/5
        [ResponseType(typeof(Componente))]
        public async Task<IHttpActionResult> GetComponente(int id)
        {
            Componente componente = await db.Componente.FindAsync(id);
            if (componente == null)
            {
                return NotFound();
            }

            return Ok(componente);
        }

        // PUT: api/Componentes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComponente(int id, Componente componente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != componente.CodigoC)
            {
                return BadRequest();
            }

            db.Entry(componente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponenteExists(id))
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

        // POST: api/Componentes
        [ResponseType(typeof(Componente))]
        public async Task<IHttpActionResult> PostComponente(Componente componente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Componente.Add(componente);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ComponenteExists(componente.CodigoC))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = componente.CodigoC }, componente);
        }

        // DELETE: api/Componentes/5
        [ResponseType(typeof(Componente))]
        public async Task<IHttpActionResult> DeleteComponente(int id)
        {
            Componente componente = await db.Componente.FindAsync(id);
            if (componente == null)
            {
                return NotFound();
            }

            db.Componente.Remove(componente);
            await db.SaveChangesAsync();

            return Ok(componente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComponenteExists(int id)
        {
            return db.Componente.Count(e => e.CodigoC == id) > 0;
        }
    }
}