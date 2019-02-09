using Npx.Geomsg.Api.DataAccess;
using Npx.Geomsg.Api.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace Npx.Geomsg.Api.Security
{
	public class UserSessionBus
	{
		internal static void Start(bool extendSession)
		{
			var token = HttpContext.Current.Request.Headers["Authorization"];

			UserSession sessionDb = null;
			UserSession session = null;

			using (var repository = new GeoMsgContext())
			{
				sessionDb = repository.UserSession.SingleOrDefault(x => x.Token == token);

				if (sessionDb == null)
					return;

				session = new UserSession
				{
					ID = sessionDb.ID,
					DateCreate = sessionDb.DateCreate,
					User = sessionDb.User,
					UserId = sessionDb.UserId
				};
			}

			var principal = new SessionPrincipal(session);

			HttpContext.Current.User = principal;
			Thread.CurrentPrincipal = principal;
		}

		internal static SessionPrincipal GetLogged()
		{
			return HttpContext.Current == null ? null : HttpContext.Current.User as SessionPrincipal;
		}
	}
}