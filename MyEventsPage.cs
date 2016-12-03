using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Linq;

namespace SCUEvents
{
	public class MyEventsPage : ContentPage
	{
		Label main_label;
		Button day_button, month_button, week_button, show_all_button;
		Image logo;
		ListView listview_my;
		TimeSpan Week = new System.TimeSpan(7, 0, 0, 0);
		TimeSpan day = new System.TimeSpan(1, 0, 0, 0);

		App app = (App)Application.Current;

		public MyEventsPage()
		{
			listview_my = new ListView()
			{
				ItemsSource = app.MyEvents_collection,
				SeparatorColor = Color.Maroon,
			};


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

			show_all_button = new Button
			{
				Text = "Show All Events",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 50,
				TextColor = Color.Maroon
			};

			listview_my.ItemTapped += async (sender, e) =>
			{
				Debug.WriteLine("Tapped: " + e.Item);
				await Navigation.PushAsync(new EventSpecificPage((EventItem)listview_my.SelectedItem));
				((ListView)sender).SelectedItem = null; // de-select the row
			};

			day_button.Clicked += onButtonClicked;
			week_button.Clicked += onButtonClicked;
			month_button.Clicked += onButtonClicked;
			show_all_button.Clicked += onButtonClicked;

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
					show_all_button
				}
			};
		}

		void onButtonClicked(object sender, EventArgs e)
		{
			if (sender == day_button)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				//populate list w new correct data
				for (int i = 0; i < app.MyEvents_collection.Count; i++)
				{
					if (DateTime.Now.Day == app.MyEvents_collection[i].Date.Day)
					{
						app.Sorted_collection.Add(app.MyEvents_collection[i]);
					}
					//show list
					listview_my.ItemsSource = app.Sorted_collection;
				}
			}

			else if (sender == week_button)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				for (int i = 0; i < app.MyEvents_collection.Count; i++)
				{
					if (DateTime.Now >= app.MyEvents_collection[i].Date - Week)
					{
						if (DateTime.Compare(DateTime.Now - day, app.AllEvents_collection[i].Date) <= 0)
							app.Sorted_collection.Add(app.MyEvents_collection[i]);
					}
					listview_my.ItemsSource = app.Sorted_collection;
				}
			}

			else if (sender == month_button)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				for (int i = 0; i < app.MyEvents_collection.Count; i++)
				{
					if (DateTime.Now.Month == app.MyEvents_collection[i].Date.Month)
					{
						app.Sorted_collection.Add(app.MyEvents_collection[i]);
					}
					listview_my.ItemsSource = app.Sorted_collection;
				}
			}
			else if (sender == show_all_button)
				listview_my.ItemsSource = app.AllEvents_collection;

		}
	}
}