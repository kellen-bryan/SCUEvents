﻿using System;
using SCUEvents;
using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace SCUEvents
{
	public class EventSpecificPage : ContentPage
	{
		Button add_to_events, remove_button;
		Label event_label, desc;
		Image logo;
		int all_index, my_index;

		App app = (App)Application.Current;

		public EventSpecificPage(EventItem item)
		{

			//get AllEvents index
			for (int i = 0; i < app.AllEvents_collection.Count; i++)
			{
				if (string.Equals(item.Name, app.AllEvents_collection[i].Name) == true)
					all_index = i;
			}

			//get myEvents index and set AllEvents isFav
			for (int i = 0; i < app.MyEvents_collection.Count; i++)
			{
				if (string.Equals(item.Name, app.MyEvents_collection[i].Name) == true)
				{
					my_index = i;
					app.AllEvents_collection[all_index].IsFavorited = true;
					break;
				}
				else 
					app.AllEvents_collection[all_index].IsFavorited = false;
			}


			event_label = new Label
			{
				Text = item.Name,
				TextColor = Color.Maroon,
				HorizontalTextAlignment = TextAlignment.Center,
				FontSize = 30
			};

			desc = new Label
			{
				Text = "\nDate: " + item.Date + "\nTime: " + item.Time + "\nLocation: " + item.Location + "\n\n" + item.Info + " \n\n",
				HorizontalTextAlignment = TextAlignment.Center,
				FontAttributes = FontAttributes.Bold
			};

			add_to_events = new Button
			{
				Text = "Add to MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 160,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End

			};

			remove_button = new Button
			{
				Text = "Remove from MyEvents",
				BorderWidth = 1,
				BorderColor = Color.Maroon,
				HeightRequest = 30,
				WidthRequest = 160,
				TextColor = Color.Maroon,
				VerticalOptions = LayoutOptions.End,
			};

			logo = new Image
			{
				Source = ImageSource.FromResource("SCUEvents.SCU_Events_logo.jpg"),
				WidthRequest = 300,
				HeightRequest = 50
			};

			add_to_events.Clicked += buttonClicked;
			remove_button.Clicked += buttonClicked;

			//set add/remove button
			setAddRemove();

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Children =
				{
					logo,
					event_label,
					desc,

					new StackLayout{
						VerticalOptions = LayoutOptions.End,
						Children=
						{
							add_to_events,
							remove_button,
						}
					}
				}
			};
		}

		void buttonClicked(object sender, EventArgs e)
		{
			if (sender == add_to_events)
			{
				app.AllEvents_collection[all_index].IsFavorited = true;

				app.MyEvents_collection.Add(app.AllEvents_collection[all_index]);

				setAddRemove();

				app.AppData.saveData();

				DisplayAlert("Go Broncos!", "This event has been added to your MyEvents Page", "OK");
			
			}

			else if (sender == remove_button)
			{
				app.AllEvents_collection[all_index].IsFavorited = false;

				app.MyEvents_collection.Remove(app.MyEvents_collection[my_index]);

				setAddRemove();

				app.AppData.saveData();

				DisplayAlert("Success", "Event has been removed", "OK");
			}
		}

		void setAddRemove()
		{
			if (app.AllEvents_collection[all_index].IsFavorited == false)
			{
				add_to_events.IsVisible = true;
				remove_button.IsVisible = false;
			}

			else
			{
				add_to_events.IsVisible = false;
				remove_button.IsVisible = true;
			}
		}
			
	}
}