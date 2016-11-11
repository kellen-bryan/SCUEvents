using System;
using Xamarin.Forms;

namespace SCUEvents
{
	public class EventSpecificPage : ContentPage
	{
		Button add_to_events;
		Label event_label;
		Image logo;

		public EventSpecificPage()
		{
			event_label = new Label
			{
				Text = String.Format("Event Name Here"), //"{0}", event_name[i]TextColor = Color.Maroon,
				BackgroundColor = Color.Silver,
				TextColor = Color.Maroon,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 30
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

			logo = new Image
			{
				Source = ImageSource.FromResource("SCUEvents.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			add_to_events.Clicked += buttonClicked;

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					logo,
					event_label,
					add_to_events
				}
			};
		}

		void buttonClicked(object sender, EventArgs e)
		{
			if (sender == add_to_events)
			{
				DisplayAlert("Go Broncos!", "This event has been added to your MyEvents Page", "OK");
			}
		}
	}
}
