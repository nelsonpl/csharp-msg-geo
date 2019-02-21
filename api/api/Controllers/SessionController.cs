using Npx.Geomsg.Api.Core;
using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Api.ViewModels;
using Npx.Geomsg.Core.Business;
using Npx.Geomsg.Core.Models;
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

		private UserSessionBus _bus = new UserSessionBus();

		[HttpPost]
		[AllowAnonymous]
		public string Post(SessionViewModel user)
		{
			return _bus.Create(user.Email, user.Password);
		}

		/// <summary>
		/// Deletes a specific TodoItem.
		/// </summary>
		[HttpDelete]
		[AllowAnonymous]
		public void Delete(string token)
		{
			_bus.Delete(token);
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
