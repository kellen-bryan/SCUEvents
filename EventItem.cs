using System;
using Xamarin.Forms;
using System.Xml.Serialization;

namespace SCUEvents
{
	public class EventItem
	{
		public string Name { get; set; }

		public DateTime Date { get; set; }

		public string Location { get; set; }

		public string Time { get; set; }

		public string Info { get; set; }

		public bool IsFavorited { get; set; }

		public override string ToString()
		{
			return String.Format("{0}", String.IsNullOrWhiteSpace(Name) ? "???" : Name);
		}
	}
}
