using System;
using SCUEvents;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace SCUEvents
{
	public class EventSpecificPage : ContentPage
	{
		Button add_to_events, remove_button;
		Label event_label, desc;
		Image logo;

		App app = (App)Application.Current;

		public EventSpecificPage(EventItem item)
		{
			   
			//get item index
			for (int i = 0; i < app.AllEvents_collection.Count; i++)
			{
				if (app.all_or_my_index == 0)
				{
					if (string.Equals(item.Name, app.AllEvents_collection[i].Name) == true)
						app.current_event_index = i;
				}
				else
				{
					if (string.Equals(item.Name, app.MyEvents_collection[i].Name) == true)
						app.current_event_index = i;
				}
			}


			event_label = new Label
			{
				Text = item.Name,
				TextColor = Color.Maroon,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 30
			};

			desc = new Label
			{
				Text = "\nDate: " + item.Date + "\nTime: " + item.Time + "\nLocation: " + item.Location + "\n\n" + item.Info + " \n\n",
				HorizontalTextAlignment = TextAlignment.Center,
				FontAttributes = FontAttributes.Bold
			};

			add_to_events = new Button
			{
				Text = "Add to MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 160,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End

			};

			remove_button = new Button
			{
				Text = "Remove from MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 160,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End,
				IsVisible = false
			};

			logo = new Image
			{
				Source = ImageSource.FromResource("SCUEvents.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			add_to_events.Clicked += buttonClicked;
			remove_button.Clicked += buttonClicked;

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					logo,
					event_label,
					desc,

					new StackLayout{
						VerticalOptions = LayoutOptions.End,
						Children=
						{
							add_to_events,
							remove_button,
						}
					}
				}
			};

			//makes proper button visible to user
			if (app.MyEvents_collection.Contains(item))
			{
				remove_button.IsVisible = true;
				add_to_events.IsVisible = false;
			}
			else
			{
				remove_button.IsVisible = false;
				add_to_events.IsVisible = true;
			}
		}

		void buttonClicked(object sender, EventArgs e)
		{
			if (sender == add_to_events)
			{
				app.MyEvents_collection.Add(app.AllEvents_collection[app.current_event_index]);
				DisplayAlert("Go Broncos!", "This event has been added to your MyEvents Page", "OK");
				add_to_events.IsVisible = false;
				remove_button.IsVisible = true;
			}
			else if (sender == remove_button)
			{
				if (app.all_or_my_index == 0)
					app.MyEvents_collection.Remove(app.AllEvents_collection[app.current_event_index]);
				else
					app.MyEvents_collection.Remove(app.MyEvents_collection[app.current_event_index]);
				DisplayAlert("Success", "Event has been removed", "OK");
				add_to_events.IsVisible = true;
				remove_button.IsVisible = false;
			}
		}
	}
}