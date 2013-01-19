using System;
namespace PAE.WebUI
{
    public partial class Express : BasePage
    {
        #region Constants

        public const string LANGUAGE_URL_FORMAT = @"~/exp?" + LANGUAGE_QUERY_NAME + "={0}";

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
