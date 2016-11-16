using System;
using SCUEvents;
using Xamarin.Forms;

namespace SCUEvents
{
	public class EventSpecificPage : ContentPage
	{
		Button add_to_events, myevents_button;
		Label event_label, desc;
		Image logo;

		public EventSpecificPage(string event_name)
		{
			event_label = new Label
			{
				Text = event_name, //"{0}", event_name[i]TextColor = Color.Maroon,
				BackgroundColor = Color.Silver,
				TextColor = Color.Maroon,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 30
			};

			desc = new Label
			{
				Text = "\n\nDate: \nTime:\n\nthis is a sample description for " + event_name,
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

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					logo,
					event_label,
					add_to_events,
					desc,
					new StackLayout{
						VerticalOptions = LayoutOptions.End,
						Children= {
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
				DisplayAlert("Go Broncos!", "This event has been added to your MyEvents Page", "OK");
			}
			else if (sender == myevents_button)
			{
				Navigation.PushAsync(new MyEventsPage());
			}
		}
	}
}
