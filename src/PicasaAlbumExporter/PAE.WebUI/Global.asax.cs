using System;
using System.Web;
using System.Web.Routing;
using PAE.WebUI.Routes;

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
            Route expressRoute = new Route(ExpressModeRouteHandler.URL, new ExpressModeRouteHandler());
            routeCollection.Add(ExpressModeRouteHandler.ROUTE_NAME, expressRoute);

            Route previewRoute = new Route(PreviewRouteHandler.URL, new PreviewRouteHandler());
            routeCollection.Add(PreviewRouteHandler.ROUTE_NAME, previewRoute);

            Route cultureRoute = new Route(SelectCultureRouteHandler.URL, new SelectCultureRouteHandler());
            cultureRoute.Constraints = new RouteValueDictionary { { SelectCultureRouteHandler.ROUTE_VALUE_CULTURE, SelectCultureRouteHandler.CultureRegex } };
            routeCollection.Add(SelectCultureRouteHandler.ROUTE_NAME, cultureRoute);
		}

		#endregion
	}
}