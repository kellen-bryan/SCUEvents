using System;
using Xamarin.Forms;

namespace SCUEvents
{
	public class MyEventsPage : ContentPage
	{
		Label main_label;
		Button day_button, month_button, week_button;
		Image logo;

		public MyEventsPage()
		{
			logo = new Image
			{
				Source = ImageSource.FromResource("SCUEvents.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			main_label = new Label
			{
				Text = "MyEvents",
				TextColor = Color.Maroon,
				BackgroundColor = Color.Silver,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 40
			};

			day_button = new Button
			{
				Text = "Day",
				HeightRequest = 30,
				WidthRequest = 50,
				TextColor = Color.Maroon
			};

			week_button = new Button
			{
				Text = "Week",
				HeightRequest = 30,
				WidthRequest = 50,
				TextColor = Color.Maroon
			};

			month_button = new Button
			{
				Text = "Month",
				HeightRequest = 30,
				WidthRequest = 50,
				TextColor = Color.Maroon,
			};

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					logo,
					main_label,

					new StackLayout
					{
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						Spacing = 50,
						Children =
						{
							day_button,
							week_button,
							month_button
						}
					}

				}
			};
		}
	}
}
