using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace PAE.WebUI.Routes
{
    public class ExpressModeRouteHandler : IRouteHandler
    {
        #region Constants

        public const string ROUTE_NAME = "ExpressModeRoute";
        public const string URL = "exp";

        #endregion

        #region Methods

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            Express expressPage = (Express)BuildManager.CreateInstanceFromVirtualPath("~/Express.aspx", typeof(Express));
            return expressPage;
        }

        #endregion
    }
}
