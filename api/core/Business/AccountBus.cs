using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Core.Models;
using Npx.Geomsg.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npx.Geomsg.Core.Business
{
	public class AccountBus : IDisposable
	{
		private GeoMsgContext db = new GeoMsgContext();
																 
		public void SignUp(User user)
		{
			if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
			{
				throw new Exception("Incorrect username or password.");
			}

			user.Email = user.Email.ToLower().Trim();

			if (db.User.Any(x => x.Email.Equals(user.Email)))
			{
				throw new Exception("User already exists.");
			}

			user.Password = Crypt.Encrypt(user.Email + "*" + user.Password);
			user.DateCreate = DateTime.Now;

			db.User.Add(user);
			db.SaveChanges();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
