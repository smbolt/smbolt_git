using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.GR
{
	public class ReportImageSet : SortedList<int, ReportImage>
	{
		public ReportImageSet()
		{


		}

		public void AddItem(int key, ReportImage reportImage)
		{
			if (this.ContainsKey(key))
				throw new Exception("An item with the key value '" + key.ToString() + "' already exists in the ReportImageSet.");

			reportImage.ReportImageSet = this;
			this.Add(key, reportImage);
		}

		public void RemoveItem(int key)
		{
			if (!this.ContainsKey(key))
				throw new Exception("An item with the key value '" + key.ToString() + "' does not exist in the ReportImageSet.");

			this[key].ReportImageSet = null;
			this.Remove(key);
		}
	}
}
