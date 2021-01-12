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
using ProductManagementWebApi.Models;
using ProductManagementWebApi.Security;

namespace ProductManagementWebApi.Controllers
{

    public class UsersController : ApiController
    {
        private DbProductManagementEntities db = new DbProductManagementEntities();

        //Disabled ProxyCreationEnabled in DbProductManagementEntities(Context) Class  Date: 6-1-2020
        //public UsersController()
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //}

        [HttpPost]
        [ResponseType(typeof(User))]
        [Route("api/Users/Login")]
        public IHttpActionResult Login(User UserView)
        {
            User DBUser = db.Users.Where(user => user.Email.Equals(UserView.Email) && user.Password.Equals(UserView.Password)).FirstOrDefault();

            if (DBUser == null)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(DBUser);
            }
        }

        //Not Using User Creation Or Get User Or Delete

        //// GET: api/Users
        //[BasicAuthentication]
        //public IQueryable<User> GetUsers()
        //{
        //    return db.Users;
        //}

        //// GET: api/Users/5
        //[BasicAuthentication]
        //[ResponseType(typeof(User))]
        //public IHttpActionResult GetUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        //// PUT: api/Users/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUser(int id, User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Users
        //[ResponseType(typeof(User))]
        //public IHttpActionResult PostUser(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        //}

        //// DELETE: api/Users/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool UserExists(int id)
        //{
        //    return db.Users.Count(e => e.Id == id) > 0;
        //}
    }
}