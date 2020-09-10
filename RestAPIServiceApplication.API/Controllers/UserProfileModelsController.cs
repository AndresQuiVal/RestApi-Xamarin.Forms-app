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
using Microsoft.AspNet.Identity;
using RestAPIServiceApplication.API.Data;
using RestAPIServiceApplication.API.Models;

namespace RestAPIServiceApplication.API.Controllers
{
    public class UserProfileModelsController : ApiController
    {
        private DataContext db = new DataContext();

        [Authorize(Roles = "Administrator")]
        // GET: api/UserProfileModels
        public IQueryable<UserProfileModel> GetUserProfileModels()
        {
            return db.UserProfileModels;
        }

        [Authorize(Roles = "Administrator")]
        // GET: api/UserProfileModels/5
        [ResponseType(typeof(UserProfileModel))]
        public IHttpActionResult GetUserProfileModel(int id)
        {
            UserProfileModel userProfileModel = db.UserProfileModels.Find(id);
            if (userProfileModel == null)
            {
                return NotFound();
            }

            return Ok(userProfileModel);
        }

        // PUT: api/UserProfileModels
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserProfileModel(UserProfileModel model)
        {
            string userName = User.Identity.GetUserName();

            if (model.Email != userName)
                return StatusCode(HttpStatusCode.Unauthorized);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserProfileModelExists(model.Id))
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

        // POST: api/UserProfileModels
        [ResponseType(typeof(UserProfileModel))]
        public IHttpActionResult PostUserProfileModel(UserProfileModel userProfileModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserProfileModels.Add(userProfileModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userProfileModel.Id }, userProfileModel);
        }

        // DELETE: api/UserProfileModels/5
        [ResponseType(typeof(UserProfileModel))]
        public IHttpActionResult DeleteUserProfileModel(int id)
        {
            UserProfileModel userProfileModel = db.UserProfileModels.Find(id);
            if (userProfileModel == null)
            {
                return NotFound();
            }

            db.UserProfileModels.Remove(userProfileModel);
            db.SaveChanges();

            return Ok(userProfileModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserProfileModelExists(int id) => 
            db.UserProfileModels.Count(e => e.Id == id) > 0;
    }
}