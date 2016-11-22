using System;
using SCUEvents;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using final;
using System.Diagnostics;

namespace SCUEvents
{
	public class EventSpecificPage : ContentPage
	{
		Button add_to_events, myevents_button, remove_button;
		Label event_label, desc;
		Image logo;

		App app = (App)Application.Current;

		public EventSpecificPage(EventItem item)
		{
			//get item index
			for (int i = 0; i < app.AllEvents_collection.Count; i++)
			{
				if (string.Equals(item.Name, app.AllEvents_collection[i].Name) == true)
					app.current_event_index = i;
			}


			event_label = new Label
			{
				Text = item.Name,
				BackgroundColor = Color.Silver,
				TextColor = Color.Maroon,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 30
			};

			desc = new Label
			{
				Text = "\n\nDate: " + item.Date + "\nTime: " + item.Time + "\n\nthis is a sample description for " + item.Name
			};

			add_to_events = new Button
			{
				Text = "Add to MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 140,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End

			};

			remove_button = new Button
			{
				Text = "Remove from MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 140,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End
			};

			myevents_button = new Button
			{
				Text = "MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 140,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End
			};

			logo = new Image
			{
				Source = ImageSource.FromResource("final.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			add_to_events.Clicked += buttonClicked;
			myevents_button.Clicked += buttonClicked;
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
						Children= {
							add_to_events,
							remove_button,
							myevents_button
						}
					}
				}
			};
		}

		void buttonClicked(object sender, EventArgs e)
		{
			if (sender == add_to_events)
			{
				app.MyEvents_collection.Add(app.AllEvents_collection[app.current_event_index]);
				DisplayAlert("Go Broncos!", "This event has been added to your MyEvents Page", "OK");
			}
			else if (sender == myevents_button)
			{
				Navigation.PushAsync(new MyEventsPage());
			}
			else if (sender == remove_button)
			{
				DisplayAlert("Success","Event has been removed", "OK");
				app.MyEvents_collection.Remove(app.AllEvents_collection[app.current_event_index]);
			}
		}
	}
}
