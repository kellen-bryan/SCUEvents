using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Diagnostics;
using final;

namespace SCUEvents
{
	public class MyEventsPage : ContentPage
	{
		Label main_label;
		Button day_button, month_button, week_button;
		Image logo;
		ListView listview_my;

		App app = (App)Application.Current;

		public MyEventsPage()
		{
			listview_my = new ListView()
			{
				ItemsSource = app.MyEvents_collection,
				SeparatorColor = Color.Maroon,
				IsPullToRefreshEnabled = true
			};

			logo = new Image
			{
				Source = ImageSource.FromResource("final.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			main_label = new Label
			{
				Text = "MyEvents",
				TextColor = Color.Maroon,
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

			listview_my.ItemTapped += async (sender, e) =>
			{
				Debug.WriteLine("Tapped: " + e.Item);
				//await DisplayAlert("Tapped", e.Item + " row was tapped", "OK");
				await Navigation.PushAsync(new EventSpecificPage((EventItem)listview_my.SelectedItem));
				((ListView)sender).SelectedItem = null; // de-select the row
			};

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Padding = 20,
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
					},
					listview_my,
				}
			};
		}

		void onButtonClicked(object sender, EventArgs e)
		{
		}
	}
}