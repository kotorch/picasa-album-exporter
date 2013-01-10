using System.Web.UI;
using System.Threading;
using System.Globalization;

namespace PAE.WebUI
{
	public class BasePage : Page
	{
		#region Fields

		private string m_selectedLanguage;

		#endregion

		#region Properties

		public string SelectedLanguage
		{
			protected get
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

		#endregion

		#region Methods

		protected override void InitializeCulture()
		{
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
