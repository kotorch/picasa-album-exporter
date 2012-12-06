using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Photos;

namespace PAE.Logic
{
	public class AlbumExporter : PicasaServiceClient
	{
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

		private const string COUNTER = "<<COUNTER>>";
		private const string CAPTION = "<<CAPTION>>";
		private const string ORIGINAL_URL = "<<ORIGINAL-URL>>";
		private const string PREVIEW_URL = "<<PREVIEW-URL>>";
		private const string PICASA_URL = "<<PICASA-URL>>";
		private const string FILE_NAME = "<<FILE-NAME>>";

		public const string DEFAULT_TEMPLATE = "<p><a name=\"" + COUNTER + "\">" + COUNTER + "</a>. " + CAPTION + "</p>"
			+ "<p><a href=\"" + ORIGINAL_URL + "\" title=\"Увеличить\"><img src=\"" + PREVIEW_URL + "\" alt=\"[picasa-web]\" style=\"border:1px solid gray;\" /></a>"
			+ "<br /><sub><i><a href=\"" + PICASA_URL + "\">Смотреть и комментировать на пикасе</a></i></sub></p>";

		#endregion

		#region Fields

		private delegate string ValueGetter(PicasaEntry photo);

		private Dictionary<string, ValueGetter> valueGetters;
	
		private int photoCounter = 0;
		private int previewWidth = 1024;
		private int previewHeight = 768;

		#endregion

		#region Constructor

		public AlbumExporter()
		{
			this.valueGetters = new Dictionary<string, ValueGetter> 
			{ 
				{ COUNTER, (photo) => { return (++photoCounter).ToString(); } },
				{ CAPTION, (photo) => { return GetPhotoCaption(photo); } },
				{ ORIGINAL_URL, (photo) => { return GetImageUrl(photo, FULL_SIZE); } },
				{ PREVIEW_URL, (photo) => { return GetPreviewUrl(photo); } },
				{ PICASA_URL, (photo) => { return GetPicasaUrl(photo); } },
				{ FILE_NAME, (photo) => { return photo.Media.Title.Value; } }
			};
		}

		#endregion

		#region Methods

		public string ExportAlbum(string albumFeedUri, string template, int? previewMaxWidth, int? previewMaxHeight)
		{
			PhotoQuery query = new PhotoQuery(albumFeedUri);
			PicasaFeed feed = this.Service.Query(query);

			photoCounter = 0;
			this.previewWidth = previewMaxWidth.GetValueOrDefault(this.previewWidth);
			this.previewHeight = previewMaxHeight.GetValueOrDefault(this.previewHeight);

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
			int contentWidth = int.Parse(photo.Media.Content.Width);
			int contentHeight = int.Parse(photo.Media.Content.Height);

			int size
				= contentWidth > contentHeight 
				? Math.Min(this.previewWidth, contentWidth)
				: Math.Min(this.previewHeight, contentHeight);

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

		#endregion
	}
}
