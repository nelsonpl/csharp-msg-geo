using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Core.Business;
using Npx.Geomsg.Core.Models;
using Npx.Geomsg.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Npx.Geomsg.Api.Controllers
{
	public class AccountController : ApiController
	{
		private AccountBus bus = new AccountBus();

		[HttpPost]
		[AllowAnonymous]
		public void Post(User user)
		{
			bus.SignUp(user);
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
