using System;
namespace final
{
		public class EventItem
		{
			public string Name { get; set; }

			//public string Date { get; set; }

		public DateTime Date { get; set;}
			
			public string Location { get; set; }

			public string Time { get; set; }

			public string Info { get; set; }

			public override string ToString()
			{
				return String.Format("{0}",String.IsNullOrWhiteSpace(Name) ? "???" : Name);
			}
		}
}

