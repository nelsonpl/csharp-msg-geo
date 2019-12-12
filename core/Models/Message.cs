using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Npx.Geomsg.Core.Models
{
	public class Message
	{
		public int ID { get; set; }
		public string Msg { get; set; }
		public string Type { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime DateCreate { get; set; }
		public int UserId { get; set; }
		public virtual User User { get; set; }

	}
}