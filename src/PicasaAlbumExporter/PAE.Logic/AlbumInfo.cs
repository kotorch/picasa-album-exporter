using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Photos;

namespace PAE.Logic
{
	public class AlbumInfo
	{
		public string FeedUri { get; private set; }

		public string Title { get; private set; }

        public string Url { get; private set; }

		public AlbumInfo(PicasaEntry album)
		{
			this.FeedUri = album.FeedUri;
			this.Title = album.Title.Text + " [" + album.GetPhotoExtensionValue(GPhotoNameTable.NumPhotos) + "]";
            this.Url = album.AlternateUri.Content;
		}
	}
}
