using SCUEvents;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace final
{
	public partial class App : Application
	{
		public App()
		{
			AllEvents_collection = new ObservableCollection<EventItem>();

			MyEvents_collection = new ObservableCollection<EventItem>();

			MainPage = new NavigationPage(new Mainpage());
		}

		public IList<EventItem> AllEvents_collection { private set; get;}

		public IList<EventItem> MyEvents_collection { private set; get; }

		public int current_event_index {set; get;}

		public int all_or_my_index { set; get; }

		/*this tracks whether a move to an event specific page came from my events or home. 
		 * the index of an event in the home list is often different from the index of the same event in the my events list.
		 * thus, when remove from my events is clicked, we need to know whether the item we are on was clicked in the home page or
		 * my events page, because if we don't, and its sent from the wrong page, we remove the wrong item.
		 * 0 means it came from home
		 * 1 means it came from myevents
		 */




		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
