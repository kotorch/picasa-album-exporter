using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Photos;

namespace PAE.Logic
{
	public class AlbumExporter : PicasaServiceClient
	{
		#region Classes

		public static class Placeholders
		{
			public const string COUNTER = "<<COUNTER>>";
			public const string CAPTION = "<<CAPTION>>";
			public const string ORIGINAL = "<<ORIGINAL>>";
			public const string IMAGE = "<<IMAGE>>";
			public const string PICASA_URL = "<<PICASA-URL>>";
			public const string FILE_NAME = "<<FILE-NAME>>";
			public const string PHOTO_EXTENSIONS = "<<PHOTO-EXTENSIONS>>";
		}

		#endregion

		#region Constants

		private const string APP_NAME = "PicasaAlbumExporter";

		private const string FORWARD_SLASH = @"/";
		private const string SIZE_PREFIX = FORWARD_SLASH + "s";
		private const string FULL_SIZE = "0";
		private const string QUESTION_MARK = @"?";
		private const string HASH = "#";
		private const string NOREDIRECT = @"?noredirect=1";
		private const string AMPERSAND = @"&";
		private const string NEW_LINE = "\n";
		private const string HTML_BREAK = "<br />";

		public const int DEFAULT_PREVIEW_WIDTH = 1024;
		public const int DEFAULT_PREVIEW_HEIGHT = 768;
		public const string DEFAULT_TEMPLATE = "<p><a name=\"" + Placeholders.COUNTER + "\">" + Placeholders.COUNTER + "</a>. " + Placeholders.CAPTION 
												+ "</p><p><a href=\"" + Placeholders.ORIGINAL + "\" title=\"Open full-size\"><img src=\"" 
												+ Placeholders.IMAGE + "\" alt=\"[picasa-web]\" style=\"border:1px solid gray;\" /></a>" 
												+ "<br /><sub><i><a href=\"" + Placeholders.PICASA_URL + "\">View on Picasa</a></i></sub></p>";

		#endregion

		#region Fields

		private delegate string ValueGetter(PicasaEntry photo);

		private Dictionary<string, ValueGetter> valueGetters;
	
		private int photoCounter = 0;
		private int previewWidth = DEFAULT_PREVIEW_WIDTH;
		private int previewHeight = DEFAULT_PREVIEW_HEIGHT;

		#endregion

		#region Constructor

		public AlbumExporter()
		{
			this.valueGetters = new Dictionary<string, ValueGetter> 
			{ 
				{ Placeholders.COUNTER, (photo) => { return (++photoCounter).ToString(); } },
				{ Placeholders.CAPTION, (photo) => { return GetPhotoCaption(photo); } },
				{ Placeholders.ORIGINAL, (photo) => { return GetImageUrl(photo, FULL_SIZE); } },
				{ Placeholders.IMAGE, (photo) => { return GetPreviewUrl(photo); } },
				{ Placeholders.PICASA_URL, (photo) => { return GetPicasaUrl(photo); } },
				{ Placeholders.FILE_NAME, (photo) => { return photo.Media.Title.Value; } },
				{ Placeholders.PHOTO_EXTENSIONS, (photo) => { return GetPhotoExtensions(photo); } },
			};
		}

		#endregion

		#region Methods

		public string ExportAlbum(string albumFeedUri, string template, int previewMaxWidth, int previewMaxHeight)
		{
			PhotoQuery query = new PhotoQuery(albumFeedUri);
			PicasaFeed feed = this.Service.Query(query);

			photoCounter = 0;
			this.previewWidth = previewMaxWidth;
			this.previewHeight = previewMaxHeight;

			StringBuilder result = new StringBuilder();

			foreach (PicasaEntry photo in feed.Entries)
			{
				string html = template;

				foreach (var valueGetter in this.valueGetters)
				{
					html = html.Replace(valueGetter.Key, valueGetter.Value(photo));
				}

				result.AppendLine(html);
			}

			string output = result.ToString();

			return output;
		}

		#endregion

		#region Implementation

		private string GetPhotoCaption(PicasaEntry photo)
		{
			string output = photo.Summary.Text.Replace(NEW_LINE, HTML_BREAK);
			return output;
		}

		private string GetImageUrl(PicasaEntry photo, string size)
		{
			string url = photo.Content.Src.ToString();
			int insertPosition = url.LastIndexOf(FORWARD_SLASH);
			string output = url.Insert(insertPosition, SIZE_PREFIX + size);

			return output;
		}

		private string GetPreviewUrl(PicasaEntry photo)
		{
			int originalWidth = int.Parse(photo.GetPhotoExtensionValue(GPhotoNameTable.Width));
			int originalHeight = int.Parse(photo.GetPhotoExtensionValue(GPhotoNameTable.Height));
			
			int size = originalWidth > originalHeight 
					 ? Math.Min(this.previewWidth, originalWidth) 
					 : Math.Min(this.previewHeight, originalHeight);
				
			string output = GetImageUrl(photo, size.ToString());

			return output;
		}

		private string GetPicasaUrl(PicasaEntry photo)
		{
			string url = photo.AlternateUri.ToString();
			string output;

			if (url.Contains(QUESTION_MARK))
			{
				output = url.Replace(QUESTION_MARK, NOREDIRECT + AMPERSAND);
			}
			else
			{
				int insertPosition = url.LastIndexOf(HASH);
				output = url.Insert(insertPosition, NOREDIRECT);
			}

			return output;
		}

		private string GetPhotoExtensions(PicasaEntry photo)
		{
			StringBuilder info = new StringBuilder();
		
			foreach (var extension in photo.ExtensionElements)
			{
				info.AppendLine(extension.XmlName + " = " + photo.GetPhotoExtensionValue(extension.XmlName) + HTML_BREAK);
			}
			
			string output = info.ToString();
			
			return output;
		}

		#endregion
	}
}
