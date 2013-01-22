using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Google.GData.Photos;

namespace PAE.Logic
{
	public class AlbumSelector : PicasaServiceClient
    {
        #region Constants

        private const string ALBUM_NOT_FOUND_FORMAT = "Album is not found. {0}";
        private const string PICASAWEB_DOT_GOOGLE = "picasaweb.google";
        private const string SLASH = "/";
        private const string ALBUM_NAME_TERMINATORS = @"/?#";
        
        #endregion

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

        public AlbumInfo GetAlbum(string url)
        {
            AlbumInfo output = null;

            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    //int queryStringStart = url.IndexOf("?") + 1;
                    //string queryString = queryStringStart > 0 ? url.Substring(queryStringStart).Replace("#", string.Empty) : string.Empty;
                    //var parameters = System.Web.HttpUtility.ParseQueryString(queryString);

                    string albumUrl = url; //queryStringStart > 0 ? url.Substring(0, queryStringStart) : url;
                    int start = albumUrl.IndexOf(PICASAWEB_DOT_GOOGLE);
                    int userStart = albumUrl.IndexOf(SLASH, start) + 1;
                    int userLength = albumUrl.IndexOf(SLASH, userStart) - userStart;
                    string username = albumUrl.Substring(userStart, userLength);

                    int albumStart = userStart + userLength + 1;
                    int albumLength = albumUrl.IndexOfAny(ALBUM_NAME_TERMINATORS.ToCharArray(), albumStart) - albumStart;
                    string albumName = albumLength > 0 ? albumUrl.Substring(albumStart, albumLength) : albumUrl.Substring(albumStart);

                    foreach (AlbumInfo album in this.GetAlbums(username, string.Empty))
                    {
                        if (album.Url.EndsWith(albumName, System.StringComparison.InvariantCultureIgnoreCase))
                        {
                            output = album;
                        }
                    }
                }
                catch
                {
                    //throw new ArgumentException(string.Format(ALBUM_NOT_FOUND_FORMAT, ex.Message));
                }
            }

            return output;
        }

		#endregion
	}
}
