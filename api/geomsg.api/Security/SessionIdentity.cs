using Npx.Geomsg.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Npx.Geomsg.Api.Security
{
	internal class SessionIdentity : IIdentity
	{
		public virtual int Id { get; private set; }
		public virtual string Email { get; private set; }
		public virtual string Nome { get; private set; }



		// Required by Interface
		public string Name { get { return Nome; } }
		public string AuthenticationType { get { return ""; } }
		public bool IsAuthenticated { get { return true; } }

		public SessionIdentity(User usuario)
		{
			Id = usuario.ID;
			Email = usuario.Email;
			Nome = usuario.Name;
		}  
	}
}