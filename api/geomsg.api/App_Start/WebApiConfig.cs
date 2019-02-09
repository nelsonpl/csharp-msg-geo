using Newtonsoft.Json.Serialization;
using Npx.Geomsg.Api.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace geomsg.api
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			//Enable CORS for the Angular App
			var cors = new EnableCorsAttribute("http://localhost:8080", "*", "*");
			config.EnableCors(cors);

			// Use camel case for JSON data.
			// Set JSON formatter as default one and remove XmlFormatter
			var jsonFormatter = config.Formatters.JsonFormatter;
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
			config.Formatters.Remove(config.Formatters.XmlFormatter);
			jsonFormatter.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			config.MessageHandlers.Add(new SessionLoaderMessageHandler(true));
			config.Filters.Add(new AuthorizationAttribute(""));
		}
	}
}
