using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using Xamarin.Forms;


namespace SCUEvents
{
	public class HtmlParse
	{
		List<EventItem> eventList;

		string url = "https://www.scu.edu/events/";

		public HtmlParse()
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(url);
			System.Diagnostics.Debug.WriteLine(doc.ToString());

			foreach (HtmlNode node in doc.DocumentNode.Descendants("//@lw_events_title"))
			{
				eventList.Add(new EventItem { Name = node.ChildNodes[0].InnerHtml });
			}

			foreach (HtmlNode node in doc.DocumentNode.Descendants("//@lw_events_time"))
			{
				eventList.Add(new EventItem { Time = node.ChildNodes[0].InnerHtml });
			}

			foreach (HtmlNode node in doc.DocumentNode.Descendants("//@lw_events_summary"))
			{
				eventList.Add(new EventItem { Info = node.ChildNodes[0].InnerHtml });
			}

			foreach (HtmlNode node in doc.DocumentNode.Descendants("//@lw_events_location"))
			{
				eventList.Add(new EventItem { Location = node.ChildNodes[0].InnerHtml });
			}
		}
	}
}
