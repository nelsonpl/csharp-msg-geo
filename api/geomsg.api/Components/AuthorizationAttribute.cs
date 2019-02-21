using Npx.Geomsg.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Npx.Geomsg.Api.Components
{

	public class AuthorizationAttribute : AuthorizeAttribute
	{
		private SessionPrincipal _logged;

		public AuthorizationAttribute(string roles)
		{
			Roles = roles;
		}

		public override void OnAuthorization(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}

			if (SkipAuthorization(actionContext))
			{
				return;
			}

			if (!IsAuthenticated(actionContext))
			{
				return;
			}

			//if (!IsAuthorized(actionContext))
			//{
			//	HandleUnauthorizedRequest(actionContext);
			//}

		}

		//protected override bool IsAuthorized(HttpActionContext actionContext)
		//{
		//	if (actionContext == null)
		//	{
		//		throw new ArgumentNullException("actionContext");
		//	}

		//	if (string.IsNullOrEmpty(Roles))
		//		return true;

		//	var requiredList = UtilitarioGeral.SepararChaves(Roles);


		//	//Verificando permissões
		//	var result = requiredList.Any(role => _logado.IsInRole(role));

		//	return result;
		//}

		private bool IsAuthenticated(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}

			_logged = HttpContext.Current == null ? null : HttpContext.Current.User as SessionPrincipal;

			if (_logged == null)
			{
				HandleUnauthenticatedRequest(actionContext);
				return false;
			}

			return true;
		}

		protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
		{
			if (actionContext == null)
			{
				throw new ArgumentNullException("actionContext");
			}

			actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Access Denied.");
		}

		private void HandleUnauthenticatedRequest(HttpActionContext actionContext)
		{
			actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Access Denied.");
		}

		private static bool SkipAuthorization(HttpActionContext actionContext)
		{
			Contract.Assert(actionContext != null);

			return (
				actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
				actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
			);
		}
	}
}