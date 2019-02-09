using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Npx.Geomsg.Api.Models
{
	public class User
	{
		public int ID { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public DateTime DateCreate { get; set; }

		public ICollection<UserSession> Sessions { get; set; }
	}
}