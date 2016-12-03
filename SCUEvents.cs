using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SCUEvents
{
	public partial class App : Application
	{
		//public const string app_Data = "app_Data";

		public App()
		{
			// Load previous AppData if it exists
			AppData = new AppData();
			AppData.loadData();
		
			AllEvents_collection = new ObservableCollection<EventItem>();

			Sorted_collection = new ObservableCollection<EventItem>();

			MainPage = new NavigationPage(new Mainpage());

		}

		public AppData AppData { set; get; }

		public ObservableCollection<EventItem> AllEvents_collection { private set; get; }

		public ObservableCollection<EventItem> MyEvents_collection
		{
			set { AppData.InfoCollection = value; }
			get { return AppData.InfoCollection; }
		}

		public ObservableCollection<EventItem> Sorted_collection { private set; get; }

		/*this tracks whether a move to an event specific page came from my events or home. 
		 * the index of an event in the home list is often different from the index of the same event in the my events list.
		 * thus, when remove from my events is clicked, we need to know whether the item we are on was clicked in the home page or
		 * my events page, because if we don't, and its sent from the wrong page, we remove the wrong item.
		 * 0 means it came from home
		 * 1 means it came from myevents
		 */

		//data keys for returning OnSleep data


		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
			// Save AppData serialized into string.
			AppData.saveData();
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}