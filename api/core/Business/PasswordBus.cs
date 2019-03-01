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
	public class PasswordBus : IDisposable
	{
		private GeoMsgContext db = new GeoMsgContext();

		public void Update(int idUser, string oldPassword, string newPassword)
		{
			if (idUser == 0 || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
				throw new Exception("Fields required.");

			if (newPassword.Length < 8)
				throw new Exception("Min 8 characters.");

			var user = db.User.Find(idUser);

			if (user == null)
				throw new Exception("Not found user.");

			var passwordCrypt = Crypt.Encrypt(user.Email + "*" + oldPassword);

			if (!user.Password.Equals(passwordCrypt))
				throw new Exception("Not found user.");

			user.Password = Crypt.Encrypt(user.Email + "*" + newPassword);
									   
			db.SaveChanges();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
