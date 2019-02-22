using Npx.Geomsg.Api.Core.DataAccess;
using Npx.Geomsg.Core.Dto;
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

		public IEnumerable<MessageDto> Get()
		{
			return db.Message.OrderByDescending(x => x.DateCreate).ToList().Select(Convert);
		}

		public MessageDto Get(int id)
		{
			Message message = db.Message.Find(id);
			return Convert(message);
		}

		public int Create(MessageDto message)
		{
			message.DateCreate = DateTime.Now;
			db.Message.Add(Convert(message));
			var id = db.SaveChanges();

			return id;
		}

		private MessageDto Convert(Message message)
		{
			var dto = new MessageDto
			{
				ID = message.ID,
				Msg = message.Msg,
				Type = message.Type,
				Latitude = message.Latitude,
				Longitude = message.Longitude,
				DateCreate = message.DateCreate,
				UserId = message.UserId,
				UserName = message.User.Name
			};
			return dto;
		}

		private Message Convert(MessageDto message)
		{
			var dto = new Message
			{
				ID = message.ID,
				Msg = message.Msg,
				Type = message.Type,
				Latitude = message.Latitude,
				Longitude = message.Longitude,
				DateCreate = message.DateCreate,
				UserId = message.UserId
			};
			return dto;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
