using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace SCUEvents
{
	public partial class App : Application
	{

		public App()
		{
			// Load previous AppData if it exists
			AppData = new AppData();
			AppData.loadData();
		
			AllEvents_collection = new ObservableCollection<EventItem>();

			Sorted_collection = new ObservableCollection<EventItem>();

			MainPage = new NavigationPage(new HomePage());

			all_index = new int();

		}

		public AppData AppData { set; get; }

		public ObservableCollection<EventItem> AllEvents_collection { private set; get; }

		public ObservableCollection<EventItem> MyEvents_collection
		{
			set { AppData.InfoCollection = value; }
			get { return AppData.InfoCollection; }
		}

		public int all_index { set; get;}

		public ObservableCollection<EventItem> Sorted_collection { private set; get; }

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