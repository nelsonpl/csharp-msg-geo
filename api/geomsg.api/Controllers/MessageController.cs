using Npx.Geomsg.Api.DataAccess;
using Npx.Geomsg.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Npx.Geomsg.Api.Controllers
{
	public class MessageController : ApiController
	{
		private GeoMsgContext db = new GeoMsgContext();

		// GET: api/Messages
		public IQueryable<Message> GetMessage()
		{
			return db.Message.OrderByDescending(x => x.DateCreate);
		}

		// GET: api/Messages/5
		[ResponseType(typeof(Message))]
		public IHttpActionResult Get(int id)
		{
			Message message = db.Message.Find(id);
			if (message == null)
			{
				return NotFound();
			}

			return Ok(message);
		}

		// PUT: api/Messages/5
		[ResponseType(typeof(void))]
		public IHttpActionResult Put(int id, Message message)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != message.ID)
			{
				return BadRequest();
			}

			db.Entry(message).State = EntityState.Modified;

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MessageExists(id))
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

		// POST: api/Messages
		[ResponseType(typeof(Message))]
		public IHttpActionResult Post(Message message)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			message.DateCreate = DateTime.Now;
			db.Message.Add(message);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = message.ID }, message);
		}

		// DELETE: api/Messages/5
		[ResponseType(typeof(Message))]
		public IHttpActionResult Delete(int id)
		{
			Message message = db.Message.Find(id);
			if (message == null)
			{
				return NotFound();
			}

			db.Message.Remove(message);
			db.SaveChanges();

			return Ok(message);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool MessageExists(int id)
		{
			return db.Message.Count(e => e.ID == id) > 0;
		}
	}
}
