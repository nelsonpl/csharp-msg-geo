using Npx.Geomsg.Core.Business;
using Npx.Geomsg.Core.Dto;
using Npx.Geomsg.Core.Models;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace Npx.Geomsg.Api.Controllers
{
	public class MessageController : ApiController
	{
		private MessageBus _bus = new MessageBus();

		// GET: api/Messages
		public IEnumerable<MessageDto> Get(string search)
		{
			return _bus.Get(search);
		}

		// GET: api/Messages/5
		[ResponseType(typeof(MessageDto))]
		public IHttpActionResult Get(int id)
		{
			MessageDto message = _bus.Get(id);
			if (message == null)
			{
				return NotFound();
			}

			return Ok(message);
		}

		// POST: api/Messages
		[ResponseType(typeof(MessageDto))]
		public IHttpActionResult Post(MessageDto message)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = HttpContext.Current.User as SessionPrincipal;

			message.UserId = user.SessionIdentity.Id;

			var id = _bus.Create(message);

			return CreatedAtRoute("DefaultApi", new { id = id }, message);
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
