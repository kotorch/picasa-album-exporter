using System;

namespace PAE.WebUI
{
    public partial class Default : BasePage
    {
        #region Constants

        public const string LANGUAGE_URL_FORMAT = @"~/{0}";

        #endregion

        #region Properties

        public override string LanguageUrlFormat
        {
            get { return LANGUAGE_URL_FORMAT; }
        }

        #endregion
    }
}
