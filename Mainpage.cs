using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Linq;


namespace SCUEvents
{
	public class Mainpage : ContentPage
	{
		Image logo;
		Button my_events_nav, day_button, week_button, month_button;
		Entry search_entry;
		ListView listview_all;

		App app = (App)Application.Current;

		public Mainpage()
		{

			//set collection to listview
			listview_all = new ListView()
			{
				ItemsSource = app.AllEvents_collection,
				SeparatorColor = Color.Maroon,
				IsPullToRefreshEnabled = true
			};

			//call when refreshing
			listview_all.Refreshing += OnRefresh;

			if (app.AllEvents_collection.Count == 0)//w/out this line the page adds another set of sample data
			{

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Men's Basketball vs. Gonzaga",
					Date = "(Today)",
					Location = "Leavy Athletic Center",
					Time = "7:00PM",
					Info = "Santa Clara Men's basketball team faces off against #14 ranked Gonzaga"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Undergraduate Photo Exhibit",
					Date = "(This week)",
					Location = "Art Building",
					Time = "12:00PM-7:00PM",
					Info = "Undergraduates show off their photography skills in new exhibit, this week only"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Jazz Band Performance",
					Date = "(Later this month)",
					Location = "Mayer Theatre",
					Time = "6:00PM",
					Info = "The SCU jazz ensemble will be performing their holidy concert"
				});
			}

			logo = new Image
			{
				Source = ImageSource.FromResource("SCUEvents.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			my_events_nav = new Button
			{
				Text = "MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 140,
				TextColor = Color.Maroon
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
				TextColor = Color.Maroon
			};

			search_entry = new Entry
			{
				WidthRequest = 70,
				Placeholder = "Search",
				PlaceholderColor = Color.Gray
			};

			my_events_nav.Clicked += buttonClicked;

			listview_all.ItemTapped += async (sender, e) =>
			{
				this.Title = "Home";
				Debug.WriteLine("Tapped: " + e.Item);
				await Navigation.PushAsync(new EventSpecificPage((EventItem)listview_all.SelectedItem));
				((ListView)sender).SelectedItem = null; // de-select the row
				app.all_or_my_index = 0;//now we know this comes from home

			};

			//hides nav bar on main page
			NavigationPage.SetHasNavigationBar(this, false);


			Content = new StackLayout
			{
				Padding = 20,
				Orientation = StackOrientation.Vertical,
				Children =
				{
					logo,
					search_entry,

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

					listview_all,
					my_events_nav
				}
			};
		}

		void buttonClicked(object sender, EventArgs e)
		{
			if (sender == my_events_nav)
			{
				Navigation.PushAsync(new MyEventsPage());
				this.Title = "Home";
			}

			if (sender == day_button)
			{
			}

			if (sender == week_button)
			{
			}

			if (sender == month_button)
			{
			}

			if (sender == search_entry)
			{
			}
		}

		void OnRefresh(object sender, EventArgs e)
		{
			var list = (ListView)sender;

			//check for new events added
			var itemList = app.AllEvents_collection.ToList();

			//clear old list
			app.AllEvents_collection.Clear();

			//add updated list
			foreach (var s in itemList)
			{
				app.AllEvents_collection.Add(s);
			}
			//end the refresh state
			list.IsRefreshing = false;
		}

	}
}