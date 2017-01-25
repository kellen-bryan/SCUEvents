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
	public partial class HomePage : ContentPage
	{
		App app = (App)Application.Current;

		TimeSpan Week = new System.TimeSpan(7, 0, 0, 0);
		TimeSpan day = new System.TimeSpan(1, 0, 0, 0);
		int notification_flag = 0;
		DateTime notification = new DateTime(1995, 12, 20);
		int count = 0;
		int only_one = 0;


		public HomePage()
		{
			InitializeComponent();
		

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
					Date = DateTime.Today+Week,
					Location = "Leavy Athletic Center",
					Time = "4:00PM",
					Info = "Santa Clara Women's Soccer team team faces off against Pepperdine",
					Source = "https://www.scu.edu/media/offices/omc/scu-brand-guidelines/visual-identity-amp-photography/visual-identity-toolkit/logos-amp-seals/Athletics_Primary(onWhiteOrBlack)-360x207.png"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Undergrad Photo Exhibit",
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
					Info = "The SCU jazz ensemble will be performing their holiday concert",
					Source = "http://www.scupresents.org/sites/default/files/styles/half/public/COLORSmallImageMusic@Noon.jpg?itok=BpsG08Ae"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "On campus tutoring at the HUB",
					Date = new DateTime(2016, 12, 6),
					Location = "Benson 22",
					Time = "4:00PM - 10:00PM",
					Info = "Undergraduate examination period for the 2016 fall quarter",
					Source = "https://lwcal.scu.edu/live/image/gid/39/width/200/height/200/crop/1/src_region/0,0,2048,1365/1226_pexels-photo-167682.jpg"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Into The Wild Tabling",
					Date = new DateTime(2016, 12, 9),
					Location = "Benson",
					Time = "10:00 AM - 2:00PM",
					Info = "Come sign up for fun, outdoor trips!",
					Source = "https://pbs.twimg.com/profile_images/783811842208509952/ywhpo3SC.jpg"});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Santa Clara Farmer's Market",
					Date = new DateTime(2016, 12, 10),
					Location = "Jackson St, between Homestead and Benton",
					Time = "9:00AM - 1:00PM",
					Info = "The Downtown Santa Clara Farmers Market is in full swing every Saturday from 9 a.m. to 1 p.m. on Jackson Street between Homestead and Benton, all year round.",
					Source = "http://ecx.images-amazon.com/images/I/51GGaVMa7tL.jpg"
				});

				app.AllEvents_collection.Add(new EventItem
				{
					Name = "Alpha Kappa Psi Winter Rush Tabling",
					Date = new DateTime(2017, 1, 18),
					Location = "Benson Lobby 1E",
					Time = "10:00AM - 2:00PM",
					Info = "Come learn about what it means to join Alpha Kappa Psi, a nationally renowned business fraternity.",
					Source = "https://www.akpsi.org/view.image?Id=773"
				});
			}


			System.Diagnostics.Debug.WriteLine("made it here");

			//navigation when user selects specific event
			listview_all.ItemTapped += async (sender, e) =>
			{
				this.Title = "Home";
				Debug.WriteLine("Tapped: " + e.Item);
				await Navigation.PushAsync(new EventSpecificPage((EventItem)listview_all.SelectedItem));
				((ListView)sender).SelectedItem = null; // de-select the row
			};

			System.Diagnostics.Debug.WriteLine("made it here 2");
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
						//need a count bc if multiple events are today you cant display multiple alerts, so just display alert saying you have multiple events today
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
						await Navigation.PushAsync(new EventSpecificPage(app.MyEvents_collection[only_one])); //navigate to MyEvents page
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

				//populate list w new correct data
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
							//string x = (app.AllEvents_collection[i].Name.Substring(0, j + 1));
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
			DisplayAlert("Don't Forget", item.Name +
						 " " + item.Time, "OK");	             
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