using Npx.Geomsg.Api.Core;
using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Core.Business;
using Npx.Geomsg.Core.Models;
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
		private MessageBus _bus = new MessageBus();

		// GET: api/Messages
		public IEnumerable<Message> Get()
		{
			return _bus.Get();
		}

		// GET: api/Messages/5
		[ResponseType(typeof(Message))]
		public IHttpActionResult Get(int id)
		{
			Message message = _bus.Get(id);
			if (message == null)
			{
				return NotFound();
			}

			return Ok(message);
		}

		// PUT: api/Messages/5
		[ResponseType(typeof(void))]
		public IHttpActionResult Put(Message message)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_bus.Update(message);

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

			var id = _bus.Create(message);

			return CreatedAtRoute("DefaultApi", new { id = id }, message);
		}

		// DELETE: api/Messages/5
		[ResponseType(typeof(Message))]
		public IHttpActionResult Delete(int id)
		{
			_bus.Delete(id);

			return StatusCode(HttpStatusCode.NoContent);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				_bus.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
