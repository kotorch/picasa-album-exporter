using Google.GData.Photos;

namespace PAE.Logic
{
	public class PicasaServiceClient
	{
		#region Constants

		private const string APP_NAME = "PicasaAlbumExporter";

		#endregion

		#region Fields

		private PicasaService m_service;

		#endregion

		#region Properties

		protected PicasaService Service
		{
			get { return this.m_service ?? (this.m_service = new PicasaService(APP_NAME)); }
		}

		#endregion
	}
}
