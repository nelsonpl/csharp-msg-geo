using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Npx.Geomsg.Api.Models
{
	public class Map
	{
		public string Type { get; set; }
		public List<Feature> Features { get; set; }

		public class Feature
		{
			public Geo Geometry { get; set; }
			public Propertie Properties { get; set; }
			public string Type { get; set; }

			public class Propertie
			{
				public string Message { get; set; }
				public string DateCreate { get; set; }
			}

			public class Geo
			{
				public string Type { get; set; }
				public List<string> Coordinates { get; set; }
			}
		}
	}
}