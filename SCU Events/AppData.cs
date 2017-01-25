using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using Xamarin.Forms;

namespace SCUEvents
{
	public class AppData
	{
		App app = (App)Application.Current;
		FileHelper fileHelper = new FileHelper();
		public static readonly string save_file_name = "save_file_name";

		public AppData()
		{
			InfoCollection = new ObservableCollection<EventItem>();
		}

		public ObservableCollection<EventItem> InfoCollection { set; get; }

		public string Serialize()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(AppData));
			using (StringWriter stringWriter = new StringWriter())
			{
				serializer.Serialize(stringWriter, this);
				string xml_string = stringWriter.GetStringBuilder().ToString();
				return xml_string;
			}
		}

		public static AppData Deserialize(string strAppData)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(AppData));
			using (StringReader stringReader = new StringReader(strAppData))
			{
				AppData appData = (AppData)serializer.Deserialize(stringReader);

				return appData;
			}
		}


		public void saveData()
		{
			string xml_save = Serialize();

			fileHelper.WriteText(save_file_name, xml_save);

		}

		public void loadData()
		{
			if (fileHelper.Exists(save_file_name))
			{
				string xml_string_load = fileHelper.ReadText(save_file_name);

				app.AppData = Deserialize(xml_string_load);
			}
		}
	}
}