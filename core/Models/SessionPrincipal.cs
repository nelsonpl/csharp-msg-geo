using Npx.Geomsg.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Npx.Geomsg.Core.Models
{
	public class SessionPrincipal : IPrincipal
	{
		public SessionIdentity SessionIdentity
		{
			get { return Identity as SessionIdentity; }
		}

		public IIdentity Identity { get; private set; }

		public List<string> Roles { get; private set; }


		public SessionPrincipal(UserSession session)
		{
			Identity = new SessionIdentity(session.User);
		}

		public bool IsInRole(string role)
		{
			return Roles.Contains(role.ToUpper());
		}
	}
}