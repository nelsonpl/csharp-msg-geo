using Npx.Geomsg.Core.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Npx.Geomsg.Api.Components
{
	public class SessionLoaderMessageHandler : DelegatingHandler
	{
		private readonly bool _extendSession;

		public SessionLoaderMessageHandler(bool extendSession)
		{
			_extendSession = extendSession;
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var token = HttpContext.Current.Request.Headers["Authorization"];

			var principal = UserSessionBus.GetSessionPrincipal(token);

			HttpContext.Current.User = principal;
			Thread.CurrentPrincipal = principal;


			return base.SendAsync(request, cancellationToken);
		}
	}
}