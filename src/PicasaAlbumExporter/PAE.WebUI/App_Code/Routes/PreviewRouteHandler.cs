using System.Web;
using System.Web.Compilation;
using System.Web.Routing;

namespace PAE.WebUI.Routes
{
    public class PreviewRouteHandler : IRouteHandler
    {
        #region Constants

        public const string ROUTE_NAME = "PreviewRoute";
        public const string URL = "preview";

        #endregion

        #region Methods

        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            Preview previewPage = (Preview)BuildManager.CreateInstanceFromVirtualPath("~/Preview.aspx", typeof(Preview));
            return previewPage;
        }

        #endregion
    }
}
