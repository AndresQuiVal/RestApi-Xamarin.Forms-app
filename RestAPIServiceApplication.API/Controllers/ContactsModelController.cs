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
    [Authorize]
    public class ContactsModelController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/ContactsModel
        public IQueryable<ContactsModel> GetContactsModels()
        {
            var userEmail = User.Identity.GetUserId();
            return db.ContactsModels.Where(e => e.UserId == userEmail);
        }

        // GET: api/ContactsModel/5
        [ResponseType(typeof(ContactsModel))]
        public IHttpActionResult GetContactsModel(int id)
        {
            ContactsModel contactsModel = db.ContactsModels.Find(id);
            if (contactsModel == null)
            {
                return NotFound();
            }

            return Ok(contactsModel);
        }

        // PUT: api/ContactsModel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContactsModel(int id, ContactsModel contactsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contactsModel.Id)
            {
                return BadRequest();
            }

            db.Entry(contactsModel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsModelExists(id))
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

        // POST: api/ContactsModel
        [ResponseType(typeof(ContactsModel))]
        public IHttpActionResult PostContactsModel(ContactsModel contactsModel)
        {
            contactsModel.UserId = User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ContactsModels.Add(contactsModel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contactsModel.Id }, contactsModel);
        }

        // DELETE: api/ContactsModel/5
        [ResponseType(typeof(ContactsModel))]
        public IHttpActionResult DeleteContactsModel(int id)
        {
            ContactsModel contactsModel = db.ContactsModels.Find(id);
            if (contactsModel == null)
            {
                return NotFound();
            }

            db.ContactsModels.Remove(contactsModel);
            db.SaveChanges();

            return Ok(contactsModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactsModelExists(int id)
        {
            return db.ContactsModels.Count(e => e.Id == id) > 0;
        }
    }
}