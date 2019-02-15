using Npx.Geomsg.Api.DataAccess;
using Npx.Geomsg.Api.Models;
using Npx.Geomsg.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Npx.Geomsg.Api.Controllers
{
	public class SessionController : ApiController
	{
		private GeoMsgContext db = new GeoMsgContext();

		/// <summary>
		/// Deletes a specific TodoItem.
		/// </summary>
		[HttpDelete]
		[AllowAnonymous]
		public void Delete(string token)
		{
			var sessionDb = db.UserSession.SingleOrDefault(x => x.Token.Equals(token));

			if(sessionDb != null)
			{
				db.UserSession.Remove(sessionDb);
				db.SaveChanges();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
