using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Npx.Geomsg.Core.Models
{
	public class UserSession
	{
		public int ID { get; set; }
		public string Token { get; set; }

		public int UserId { get; set; }
		public virtual User User { get; set; }
		public DateTime DateCreate { get; set; }
	}
}