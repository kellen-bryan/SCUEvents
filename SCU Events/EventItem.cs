using System;
using Xamarin.Forms;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SCUEvents
{
	public class EventItem : ViewModelBase
	{
		App app = (App)Application.Current;
		string _name, _location, _time, _info, _source;
		DateTime _date;
		bool _isFav;

		[XmlIgnore]
		public ObservableCollection<EventItem> myList
		{
			get { return app.MyEvents_collection; }
		}

		[XmlIgnore]
		public ObservableCollection<EventItem> allList
		{
			get { return app.AllEvents_collection; }
		}


		public void RemoveItem(EventItem item)
		{
			app.MyEvents_collection.Remove(this);
			app.AllEvents_collection[app.all_index].IsFavorited = false;
		}

		public EventItem()
		{
			RemoveCommand = new Command(() => RemoveItem(this));
		}

		public string Source
		{
			set { SetProperty(ref _source, value); }
			get { return _source; }
		}

		public string Name
		{
			set { SetProperty(ref _name, value); }
			get { return _name; }
		}

		public DateTime Date
		{
			set { SetProperty(ref _date, value); }
			get { return _date; }
		}

		public string Location
		{
			set { SetProperty(ref _location, value); }
			get { return _location; }
		}

		public string Time
		{
			set { SetProperty(ref _time, value); }
			get { return _time; }
		}

		public string Info
		{
			set { SetProperty(ref _info, value); }
			get { return _info; }
		}

		public bool IsFavorited
		{
			set { SetProperty(ref _isFav, value); }
			get { return _isFav; }
		}

		public override string ToString()
		{
			return String.Format("{0}", String.IsNullOrWhiteSpace(Name) ? "???" : Name);
		}

		[XmlIgnore]
		public ICommand RemoveCommand { private set; get; }
	}
}
