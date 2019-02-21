using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Npx.Geomsg.Core.Business
{
	public class MessageBus : IDisposable
	{
		private GeoMsgContext db = new GeoMsgContext();

		public IEnumerable<Message> Get()
		{
			return db.Message.OrderByDescending(x => x.DateCreate).ToList();
		}

		public Message Get(int id)
		{
			Message message = db.Message.Find(id);
			return message;
		}

		public void Update(Message message)
		{
			db.Entry(message).State = EntityState.Modified;
			db.SaveChanges();
		}

		public int Create(Message message)
		{
			message.DateCreate = DateTime.Now;
			db.Message.Add(message);
			var id = db.SaveChanges();

			return id;
		}

		public void Delete(int id)
		{
			Message message = db.Message.Find(id);

			if (message == null)
			{
				return;
			}

			db.Message.Remove(message);
			db.SaveChanges();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
