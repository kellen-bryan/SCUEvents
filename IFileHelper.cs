using System;
using System.Collections.Generic;

namespace SCUEvents
{
	public interface IFileHelper
	{
		bool Exists(string filename);

		void WriteText(string filename, string text);

		string ReadText(string filename);

		IEnumerable<string> GetFiles();

		void Delete(string filename);
	}
}
