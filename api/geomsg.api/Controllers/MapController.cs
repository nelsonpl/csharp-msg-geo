﻿using Npx.Geomsg.Api.DataAccess;
using Npx.Geomsg.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Npx.Geomsg.Api.Controllers
{
	public class MapController : ApiController
	{
		private GeoMsgContext db = new GeoMsgContext();

		public Map Get(double west, double east, double south, double north)
		{
			Map viewModel = new Map { Type = "FeatureCollection" };

			var list = db.Message.ToList();

			viewModel.Features = list
				.Where(x => x.Longitude >= west && x.Longitude <= east && x.Latitude >= south && x.Latitude <= north)
				.OrderByDescending(x => x.DateCreate)
				.Select(x => BuildFeature(x))
				.ToList();

			return viewModel;
		}


		private Map.Feature BuildFeature(Message message)
		{
			return new Map.Feature
			{
				Type = "Feature",
				Properties = new Map.Feature.Propertie
				{
					Message = message.Msg,
					DateCreate = FormatDateCreate(message.DateCreate)
				},
				Geometry = new Map.Feature.Geo
				{
					Type = "Point",
					Coordinates = new List<string> { message.Longitude.ToString(CultureInfo.InvariantCulture), message.Latitude.ToString(CultureInfo.InvariantCulture) }
				}

			};
		}

		private string FormatDateCreate(DateTime date)
		{
			var dif = DateTime.Now - date;

			if ((int)dif.TotalMinutes == 0)
			{
				return "agora mesmo";
			}

			if ((int)dif.TotalMinutes <= 30)
			{
				return "cercar de " + (int)dif.TotalMinutes + " minutos";
			}

			if ((int)dif.TotalHours <= 10)
			{
				return "cercar de " + (int)dif.TotalHours + " horas";
			}

			if (DateTime.Now.Date == date.Date)
			{
				return "hoje";
			}

			if (DateTime.Now.AddDays(-1).Date == date.Date)
			{
				return "ontem";
			}

			return date.ToShortDateString();
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
