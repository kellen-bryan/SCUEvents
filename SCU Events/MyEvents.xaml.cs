using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace SCUEvents
{
	public partial class MyEvents : ContentPage
	{
		App app = (App)Application.Current;
		TimeSpan Week = new System.TimeSpan(7, 0, 0, 0);
		TimeSpan day = new System.TimeSpan(1, 0, 0, 0);

		public MyEvents()
		{
			InitializeComponent();

			listview_my.ItemTapped += async (sender, e) =>
			{
				await Navigation.PushAsync(new EventSpecificPage((EventItem)listview_my.SelectedItem));
				((ListView)sender).SelectedItem = null; // de-select the row
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
			{
				listview_my.ItemsSource = app.MyEvents_collection;
			}
		}
	}
}
