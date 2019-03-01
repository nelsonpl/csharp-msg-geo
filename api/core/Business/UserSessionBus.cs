using Npx.Geomsg.Api.Core;
using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Core.Models;
using Npx.Geomsg.Core.Utils;
using System;
using System.Linq;

namespace Npx.Geomsg.Core.Business
{
	public class UserSessionBus : IDisposable
	{
		private GeoMsgContext db = new GeoMsgContext();

		public static SessionPrincipal GetSessionPrincipal(string token)
		{
			UserSession sessionDb = null;
			UserSession session = null;

			using (var repository = new GeoMsgContext())
			{
				sessionDb = repository.UserSession.SingleOrDefault(x => x.Token == token);

				if (sessionDb == null)
					return null;

				session = new UserSession
				{
					ID = sessionDb.ID,
					DateCreate = sessionDb.DateCreate,
					User = sessionDb.User,
					UserId = sessionDb.UserId
				};
			}

			var principal = new SessionPrincipal(session);

			return principal;
		}

		public void DeleteByUserId(int userId)
		{
			var list = db.UserSession.Where(x => x.UserId.Equals(userId));
			db.UserSession.RemoveRange(list);

		}

		public string Create(string email, string password)
		{
			if (String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password))
			{
				throw new Exception("Email or password incorrect.");
			}

			var emailLower = email.ToLower().Trim();
			var user = db.User.SingleOrDefault(x => x.Email.Equals(emailLower));

			if (user == null)
			{
				throw new Exception("Email or password incorrect.");
			}

			var passwordCrypt = Crypt.Encrypt(user.Email + "*" + password);

			if (!user.Password.Equals(passwordCrypt))
			{
				throw new Exception("Email or password incorrect.");
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

		public void Delete(string token)
		{
			var sessionDb = db.UserSession.SingleOrDefault(x => x.Token.Equals(token));

			if (sessionDb != null)
			{
				db.UserSession.Remove(sessionDb);
				db.SaveChanges();
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}