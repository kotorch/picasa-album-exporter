using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.UI;
using PAE.WebUI.Routes;

namespace PAE.WebUI
{
	public abstract class BasePage : Page
    {
        #region Constants

        protected const string LANGUAGE_QUERY_NAME = "ln";

        #endregion

        #region Fields

        private string m_selectedLanguage;

		#endregion

		#region Properties

		public string SelectedLanguage
		{
			get
			{
                return this.m_selectedLanguage;
			}

			set
			{
				if (value != null && value.ToLower() == SupportedLanguages.En.ToString().ToLower())
				{
                    this.m_selectedLanguage = string.Empty;
				}
				else
				{
                    this.m_selectedLanguage = value;
				}
			}
		}

        public abstract string LanguageUrlFormat { get; }

		#endregion

		#region Methods

		protected override void InitializeCulture()
		{
            if (string.IsNullOrEmpty(this.SelectedLanguage))
            {
                string qsLanguage = this.Request.QueryString[LANGUAGE_QUERY_NAME] ?? string.Empty;
                
                if (Regex.IsMatch(qsLanguage, SelectCultureRouteHandler.CultureRegex))
                {
                    this.SelectedLanguage = qsLanguage;
                }
            }    
                
            if (!string.IsNullOrEmpty(this.SelectedLanguage))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(this.SelectedLanguage);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.SelectedLanguage);

                this.Culture = Thread.CurrentThread.CurrentCulture.Name;
                this.UICulture = Thread.CurrentThread.CurrentUICulture.Name;
            }

			base.InitializeCulture();
		}

		#endregion
	}
}
