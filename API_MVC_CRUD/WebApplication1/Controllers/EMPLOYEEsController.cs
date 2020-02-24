using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EMPLOYEEsController : ApiController
    {
        private EmployeeDatabaseEntities db = new EmployeeDatabaseEntities();

        // GET: api/EMPLOYEEs
        public IQueryable<EMPLOYEE> GetEMPLOYEEs()
        {
            return db.EMPLOYEEs;
        }

        // GET: api/EMPLOYEEs/5
        [ResponseType(typeof(EMPLOYEE))]
        public IHttpActionResult GetEMPLOYEE(int id)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return NotFound();
            }

            return Ok(eMPLOYEE);
        }
        // GET: api/EMPLOYEEs/5
        [ResponseType(typeof(EMPLOYEE))]
        public IHttpActionResult GetUserLogin(EMPLOYEE emp)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Where(x=>x.USERNAME== emp.USERNAME && x.USERPASSWORD==emp.USERPASSWORD).FirstOrDefault();
            if (eMPLOYEE == null)
            {
                return NotFound();
            }

            return Ok(eMPLOYEE);
        }
        // PUT: api/EMPLOYEEs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEMPLOYEE(int id, EMPLOYEE eMPLOYEE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eMPLOYEE.EMPLOYEEID)
            {
                return BadRequest();
            }

            db.Entry(eMPLOYEE).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EMPLOYEEExists(id))
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

        // POST: api/EMPLOYEEs
        [ResponseType(typeof(EMPLOYEE))]
        public IHttpActionResult PostEMPLOYEE(EMPLOYEE eMPLOYEE)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EMPLOYEEs.Add(eMPLOYEE);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = eMPLOYEE.EMPLOYEEID }, eMPLOYEE);
        }

        // DELETE: api/EMPLOYEEs/5
        [ResponseType(typeof(EMPLOYEE))]
        public IHttpActionResult DeleteEMPLOYEE(int id)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return NotFound();
            }

            db.EMPLOYEEs.Remove(eMPLOYEE);
            db.SaveChanges();

            return Ok(eMPLOYEE);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EMPLOYEEExists(int id)
        {
            return db.EMPLOYEEs.Count(e => e.EMPLOYEEID == id) > 0;
        }
    }
}