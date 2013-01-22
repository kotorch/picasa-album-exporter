using System;
using PAE.WebUI.Properties;
using Resources;

namespace PAE.WebUI
{
    public partial class Preview : BasePage
    {
        #region Properties
        
        public override string LanguageUrlFormat
        {
            get { throw new NotSupportedException(); }
        }

        #endregion
        
        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            string html = (string)this.Session[Variables.PreviewHtmlSessionName];

            if (string.IsNullOrEmpty(html))
            {
                html = Strings.EmptyPreviewHtml;
            }

            this.Response.Write(html);
        }

        #endregion
    }
}
