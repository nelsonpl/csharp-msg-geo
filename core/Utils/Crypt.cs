using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Npx.Geomsg.Core.Utils
{
	internal class Crypt
	{
		public static string Encrypt(string value)
		{
			byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
			data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
			return System.Text.Encoding.ASCII.GetString(data);
		}
	}
}