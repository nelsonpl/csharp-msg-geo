using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Api.ViewModels;
using Npx.Geomsg.Core.Business;
using Npx.Geomsg.Core.Models;
using Npx.Geomsg.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Npx.Geomsg.Api.Controllers
{
	public class PasswordController : ApiController
	{
		private PasswordBus bus = new PasswordBus();
		private UserSessionBus _userSessionbus = new UserSessionBus();

		[HttpPut]
		public IHttpActionResult Put(PasswordChange passwordChange)
		{
			var user = HttpContext.Current.User as SessionPrincipal;

			bus.Update(user.SessionIdentity.Id, passwordChange.OldPassword, passwordChange.NewPassword);

			_userSessionbus.DeleteByUserId(user.SessionIdentity.Id);

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				bus.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
