using System;
using PAE.WebUI.Properties;

namespace PAE.WebUI
{
    public partial class Express : BasePage
    {
        #region Constants

        public static readonly string LANGUAGE_URL_FORMAT = @"~/exp?" + Variables.LanguageQueryStringName + "={0}";

        #endregion

        #region Properties

        public override string LanguageUrlFormat
        {
            get { return LANGUAGE_URL_FORMAT; }
        }

        #endregion

        #region Event Handler

        protected void FullModeLink_Click(object sender, EventArgs e)
        {
            string url = string.Format(Default.LANGUAGE_URL_FORMAT, this.SelectedLanguage);
            this.Response.Redirect(url);
        }

        #endregion
    }
}
