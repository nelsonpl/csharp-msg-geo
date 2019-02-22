﻿using Npx.Geomsg.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Npx.Geomsg.Core.Dto
{
	public class MessageDto
	{	 
		public int ID { get; set; }
		public string Msg { get; set; }
		public string Type { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public DateTime DateCreate { get; set; }
		public int UserId { get; set; }
		public string UserName { get; set; }   
	}
}