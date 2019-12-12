using Npx.Geomsg.Core.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Npx.Geomsg.Api.Core.DataAccess
{
	internal class GeoMsgContext : DbContext
	{
		public GeoMsgContext() : base("DefaultConnection")
		{

		}

		public DbSet<Message> Message { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<UserSession> UserSession { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
