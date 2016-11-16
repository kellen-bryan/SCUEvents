using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using final;
using System.Diagnostics;
             
namespace SCUEvents
{
	public class Mainpage : ContentPage
	{
		Image logo;
		Button my_events_nav, day_button, week_button, month_button, event_specific_nav, day_event, week_event, month_event;
		Entry search_entry;
		Label event_title;
		List<EventItem> eventList; 
		ListView listview;

		public Mainpage()
		{
			eventList = new List<EventItem>();

			eventList.Add(new EventItem
			{
				Name = "Day Event",
				Date = "Date",
				Location = "Location",
				Time = "Time",
				Info = "Info"
			});

			eventList.Add(new EventItem
			{
				Name = "Week Event",
				Date = "Date",
				Location = "Location",
				Time = "Time",
				Info = "Info"
			});

			eventList.Add(new EventItem
			{
				Name = "Month Event",
				Date = "Date",
				Location = "Location",
				Time = "Time",
				Info = "Info"
			});

			listview = new ListView();

			listview.ItemsSource = new[] { eventList[0].Name, eventList[1].Name, eventList[2].Name };
			listview.ItemTapped += async (sender, e) =>
			{
				Debug.WriteLine("Tapped: " + e.Item);
				//await DisplayAlert("Tapped", e.Item + " row was tapped", "OK");
				await Navigation.PushAsync(new EventSpecificPage(e.Item.ToString()));
				((ListView)sender).SelectedItem = null; // de-select the row
			};


			event_title = new Label
			{
				Text = eventList[0].Name
			};

			day_event = new Button
			{
				Text = "Event today",
				BorderWidth = 1,
				TextColor = Color.Maroon
			};

			week_event = new Button
			{
				Text = "Event this week",
				BorderWidth = 1,
				TextColor = Color.Maroon
			};

			month_event = new Button
			{
				Text = "Event this month",
				BorderWidth = 1,
				TextColor = Color.Maroon
			};



			logo = new Image
			{
				Source = ImageSource.FromResource("final.SCU_Events_logo.jpg"),
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

			event_specific_nav = new Button
			{
				Text = "Event Specific Nav Test",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 140,
				TextColor = Color.Maroon
			};



			my_events_nav.Clicked += buttonClicked;
			event_specific_nav.Clicked += buttonClicked;
			day_event.Clicked += buttonClicked;
			week_event.Clicked += buttonClicked;
			month_event.Clicked += buttonClicked;

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

					listview,
					my_events_nav,
					//event_specific_nav

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

			if (sender == day_event)
			{
				Navigation.PushAsync(new EventSpecificPage(day_event.Text));
			}

			if (sender == week_event)
			{
				Navigation.PushAsync(new EventSpecificPage(week_event.Text));
			}

			if (sender == month_event)
			{
				Navigation.PushAsync(new EventSpecificPage(month_event.Text));
			}

			/*
			if (sender == event_specific_nav)
			{
				Navigation.PushAsync(new EventSpecificPage("event specific nav"));
				this.Title = "Home";
			}
			*/
		}
				
	}
}
