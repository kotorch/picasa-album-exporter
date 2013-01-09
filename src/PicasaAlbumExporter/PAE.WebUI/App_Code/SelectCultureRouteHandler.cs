using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Compilation;

namespace PAE.WebUI
{
	public class SelectCultureRouteHandler : IRouteHandler
	{
		#region Constants

		public const string ROUTE_NAME = "SelectCultureRoute";
		public const string URL = "{" + ROUTE_VALUE_CULTURE + "}";
		public const string ROUTE_VALUE_CULTURE = "culture";
		
		private const string CULTURE_REGEX_FORMAT = @"^(?:{0})$";

		#endregion

		#region Properties

		public static string CultureRegex
		{
			get
			{
				string supportedCultures = string.Join("|", Enum.GetNames(typeof(SupportedLanguages))).ToLower();
				string output = string.Format(CULTURE_REGEX_FORMAT, supportedCultures);

				return output;
			}
		}

		#endregion

		#region Methods

		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			_Default defaultPage = (_Default) BuildManager.CreateInstanceFromVirtualPath("~/Default.aspx", typeof(_Default));
			defaultPage.SelectedLanguage = requestContext.RouteData.Values[ROUTE_VALUE_CULTURE].ToString().ToLower();

			return defaultPage;
		}

		#endregion
	}
}
