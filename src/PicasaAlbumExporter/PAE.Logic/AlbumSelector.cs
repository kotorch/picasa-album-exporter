using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Google.GData.Photos;

namespace PAE.Logic
{
	public class AlbumSelector : PicasaServiceClient
	{
		#region Methods

		public Collection<AlbumInfo> GetAlbums(string username, string password)
		{
			List<AlbumInfo> data = new List<AlbumInfo>();

			if (!string.IsNullOrEmpty(username))
			{
				if (!string.IsNullOrEmpty(password))
				{
					this.Service.setUserCredentials(username, password);
				}
			
				AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri(username));
				PicasaFeed feed = this.Service.Query(query);

				foreach (PicasaEntry album in feed.Entries)
				{
					data.Add(new AlbumInfo(album));
				}
			}

			var sorted = data.OrderBy(i => i.Title).ToList();
			Collection<AlbumInfo> output = new Collection<AlbumInfo>(sorted);

			return output;
		}

		#endregion
	}
}
