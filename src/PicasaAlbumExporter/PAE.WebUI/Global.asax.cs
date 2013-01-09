using System;
using System.Web;
using System.Web.Routing;

namespace PAE.WebUI
{
	public class Global : HttpApplication
	{
		#region Event Handlers

		protected void Application_Start(object sender, EventArgs e)
		{
			RegisterRoutes(RouteTable.Routes);
		}

		protected void Session_Start(object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{

		}

		protected void Application_Error(object sender, EventArgs e)
		{

		}

		protected void Session_End(object sender, EventArgs e)
		{

		}

		protected void Application_End(object sender, EventArgs e)
		{

		}

		#endregion

		#region Implementation

		private static void RegisterRoutes(RouteCollection routeCollection)
		{
			Route route = new Route(SelectCultureRouteHandler.URL, new SelectCultureRouteHandler());
			route.Constraints = new RouteValueDictionary { { SelectCultureRouteHandler.ROUTE_VALUE_CULTURE, SelectCultureRouteHandler.CultureRegex } };
			routeCollection.Add(SelectCultureRouteHandler.ROUTE_NAME, route);
		}

		#endregion
	}
}