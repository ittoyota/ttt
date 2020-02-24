using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : ApiController
    {
        private EmployeeDatabaseEntities db = new EmployeeDatabaseEntities();
        // GET: api/Account/5
        [ResponseType(typeof(EMPLOYEE))]
        public IHttpActionResult PostUserLogin(EMPLOYEE emp)
        {
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Where(x => x.USERNAME == emp.USERNAME && x.USERPASSWORD == emp.USERPASSWORD).FirstOrDefault();
            if (eMPLOYEE == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("DefaultApi", new { id = eMPLOYEE.USERNAME }, eMPLOYEE);
        }
    }
}
