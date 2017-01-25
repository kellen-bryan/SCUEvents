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
		Button my_events_nav, day_button, week_button, month_button, show_all_button;
		Entry search_entry;
		ListView listview_all;
		TimeSpan Week = new System.TimeSpan(7, 0, 0, 0);
		TimeSpan day = new System.TimeSpan(1, 0, 0, 0);
		int notification_flag = 0;
		DateTime notification = new DateTime(1995, 12, 20);
		App app = (App)Application.Current;
		int count = 0;
		int only_one = 0;


		public Mainpage()
		{
			//set collection to listview
			listview_all = new ListView()
			{
				ItemsSource = app.AllEvents_collection,
				SeparatorColor = Color.Maroon,
				IsPullToRefreshEnabled = true,
			};

			//call when refreshing
			listview_all.Refreshing += OnRefresh;

			if (app.AllEvents_collection.Count == 0)//w/out this line the page adds another set of sample data
			{
				//manually imputting example data
				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Men's Basketball vs. Gonzaga",
					Date = DateTime.Today,
					Location = "Leavy Athletic Center",
					Time = "7:00PM",
					Info = "Santa Clara Men's basketball team faces off against #14 ranked Gonzaga",
					Source = "https://www.scu.edu/media/offices/omc/scu-brand-guidelines/visual-identity-amp-photography/visual-identity-toolkit/logos-amp-seals/Athletics_Primary(onWhiteOrBlack)-360x207.png"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Women's Soccer vs. Pepperdine",
					Date = DateTime.Today,
					Location = "Leavy Athletic Center",
					Time = "4:00PM",
					Info = "Santa Clara Women's Soccer team team faces off against Pepperdine",
					Source = "https://www.scu.edu/media/offices/omc/scu-brand-guidelines/visual-identity-amp-photography/visual-identity-toolkit/logos-amp-seals/Athletics_Primary(onWhiteOrBlack)-360x207.png"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Undergraduate Photo Exhibit",
					Date = new DateTime(2016, 12, 7),
					Location = "Art Building",
					Time = "12:00PM-7:00PM",
					Info = "Undergraduates show off their photography skills in new exhibit, this week only",
					Source = "http://www.clipartbest.com/cliparts/dir/e5L/dire5Lp5T.png"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Jazz Band Performance",
					Date = new DateTime(2016, 12, 29),
					Location = "Mayer Theatre",
					Time = "6:00PM",
					Info = "The SCU jazz ensemble will be performing their holidy concert",
					Source = "http://www.scupresents.org/sites/default/files/styles/half/public/COLORSmallImageMusic@Noon.jpg?itok=BpsG08Ae"
				});
			}

			logo = new Image
			{
				Source = ImageSource.FromResource("SCUEvents.SCU_Events_logo.jpg"),
				HorizontalOptions = LayoutOptions.Center, //embedded resource image
				WidthRequest = 300,
				HeightRequest = 50
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

			//display alert if event is today. use flag to check if event has already been displayed that day
			//have another alert for an hour before the event as well?
			//reset flag every day
			//flag will be reset by checking a datetime against todays date. the datetime only updates inside an if
			//statement that checks if 24/or12 hours have passed since last datetime update

			my_events_nav.Clicked += buttonClicked;
			search_entry.PropertyChanged += buttonClicked;
			day_button.Clicked += buttonClicked;
			week_button.Clicked += buttonClicked;
			month_button.Clicked += buttonClicked;
			show_all_button.Clicked += buttonClicked;

			//navigation when user selects specific event
			listview_all.ItemTapped += async (sender, e) =>
			{
				this.Title = "Home";
				Debug.WriteLine("Tapped: " + e.Item);
				await Navigation.PushAsync(new EventSpecificPage((EventItem)listview_all.SelectedItem));
				((ListView)sender).SelectedItem = null; // de-select the row
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
					show_all_button,

					listview_all,
					my_events_nav
				}
			};
		}

		async void buttonClicked(object sender, EventArgs e)
		{
			if (sender == my_events_nav)
			{
				if (notification.Date.Day == DateTime.Now.Day)
					notification_flag = 1;//not a new day
				else
				{
					notification = DateTime.Now;
					notification_flag = 0; //if 0, new day
				}

				if (notification_flag == 1)
				{
					await Navigation.PushAsync(new MyEvents());
					return;
				}

				for (int i = 0; i < app.MyEvents_collection.Count; i++)
				{
					if (DateTime.Now.Day == app.MyEvents_collection[i].Date.Day)
					{
						//need a count bc if multiple events are today you cant display multiple alerts
						count++;
						only_one = i; //saves index in case only one event that day
					}
				}
				if (count == 1)
				{
					System.Diagnostics.Debug.WriteLine("count = 1");
					bool response = await DisplayAlert("Don't Forget!", app.MyEvents_collection[only_one].Name + " " + app.MyEvents_collection[only_one].Time, "See Event", "Continue");
					//notify_function(app.MyEvents_collection[i]);
					if (response == true)
						await Navigation.PushAsync(new MyEvents()); //navigate to MyEvents page
					else
					{
						await Navigation.PushAsync(new MyEvents()); //navigate to MyEvents page
						this.Title = "Home";
					}
				}
				if (count == 0)
				{
					System.Diagnostics.Debug.WriteLine("count = 0");
					await Navigation.PushAsync(new MyEvents()); //navigate to MyEvents page
				}
				if (count > 1)
				{
					System.Diagnostics.Debug.WriteLine("count > 1");
					await DisplayAlert("Don't Forget!", "Multiple events today", "OK");
					await Navigation.PushAsync(new MyEvents());

				}

			}

			else if (sender == day_button)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				//populate list w/new correct data
				for (int i = 0; i < app.AllEvents_collection.Count; i++)
				{
					if (DateTime.Now.Day == app.AllEvents_collection[i].Date.Day)
					{
						app.Sorted_collection.Add(app.AllEvents_collection[i]);
					}
					//show list
					listview_all.ItemsSource = app.Sorted_collection;
				}
			}

			else if (sender == week_button)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				for (int i = 0; i < app.AllEvents_collection.Count; i++)
				{
					if (DateTime.Now >= app.AllEvents_collection[i].Date - Week)
					{
						if (DateTime.Compare(DateTime.Now - day, app.AllEvents_collection[i].Date) <= 0)
							app.Sorted_collection.Add(app.AllEvents_collection[i]);
					}
					listview_all.ItemsSource = app.Sorted_collection;
				}
			}

			else if (sender == month_button)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				for (int i = 0; i < app.AllEvents_collection.Count; i++)
				{
					if (DateTime.Now.Month == app.AllEvents_collection[i].Date.Month)
					{
						app.Sorted_collection.Add(app.AllEvents_collection[i]);
					}
					listview_all.ItemsSource = app.Sorted_collection;
				}
			}

			else if (sender == search_entry)
			{
				//clear list of any old data
				while (app.Sorted_collection.Count != 0)
				{
					app.Sorted_collection.Remove(app.Sorted_collection[0]);
				}

				if (!string.IsNullOrWhiteSpace(search_entry.Text))
				{
					string s = search_entry.Text;
					int size = s.Length;

					for (int i = 0; i < app.AllEvents_collection.Count; i++)
					{
						for (int j = 0; j < size; j++)
						{
							string x = (app.AllEvents_collection[i].Name.Substring(0, j + 1));
							if (string.Equals(app.AllEvents_collection[i].Name.Substring(0, j + 1), search_entry.Text))
								app.Sorted_collection.Add(app.AllEvents_collection[i]);
						}
					}
					listview_all.ItemsSource = app.Sorted_collection;
				}
				else
					listview_all.ItemsSource = app.AllEvents_collection;
			}

			else if (sender == show_all_button)
			{
				listview_all.ItemsSource = app.AllEvents_collection;
			}
		}

		void notify_function(EventItem item)
		{
			DisplayAlert("Don't Forget", item.Name + " " + item.Time, "OK");
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