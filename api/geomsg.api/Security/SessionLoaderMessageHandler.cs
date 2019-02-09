using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Npx.Geomsg.Api.Security
{
	public class SessionLoaderMessageHandler : DelegatingHandler
	{
		private readonly bool _extendSession;

		private UserSessionBus userSession;

		public SessionLoaderMessageHandler(bool extendSession)
		{
			_extendSession = extendSession;
			userSession = new UserSessionBus();
		}

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			UserSessionBus.Start(this._extendSession);

			return base.SendAsync(request, cancellationToken);
		}
	}
}