using Npx.Geomsg.Api.DataAccess;
using Npx.Geomsg.Api.Models;
using Npx.Geomsg.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Npx.Geomsg.Api.Controllers
{
	public class AccountController : ApiController
	{
		private GeoMsgContext db = new GeoMsgContext();

		[HttpGet]
		[AllowAnonymous]
		public string SignIn(string email, string password)
		{
			var emailLower = email.ToLower().Trim();
			var user = db.User.SingleOrDefault(x => x.Email.Equals(emailLower));

			if (user == null)
			{
				new Exception("Email or password incorrect.");
			}

			var passwordCrypt = Crypt.Encrypt(user.Email + "*" + user.Password);

			if (!user.Password.Equals(passwordCrypt))
			{
				new Exception("Email or password incorrect.");
			}

			var session = new UserSession
			{
				Token = Guid.NewGuid().ToString(),
				User = user,
				DateCreate = DateTime.Now
			};

			db.UserSession.Add(session);

			db.SaveChanges();

			return session.Token;
		}

		[HttpPost]
		[AllowAnonymous]
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

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
