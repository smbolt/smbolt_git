using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Org.GS;

namespace Org.GraphicReports
{
	public class DrawingObjectSet : SortedList<int, DrawingObject>
	{
		private int _selectedCount;
		public int SelectedCount
		{
			get { return _selectedCount; }
			set { _selectedCount = value; }
		}

		private int[] _selectedObjectKeys;
		public int[] SelectedObjectKeys
		{
			get { return _selectedObjectKeys; }
			set { _selectedObjectKeys = value; }
		}

		private int[] _getObjectKeys;
		public int[] GetObjectKeys
		{
			get { return _getObjectKeys; }
			set { _getObjectKeys = value; }
		}

		private bool _isDirty;
		public bool IsDirty
		{
			get { return _isDirty; }
			set { _isDirty = value; }
		}

		private string _selectedObjectKeysString;
		public string SelectedObjectKeysString
		{
			get { return _selectedObjectKeysString; }
		}

		private bool IsSelectionCountingSuppressed;

		public DrawingObjectSet()
		{
			_selectedObjectKeys = new int[0];
			_getObjectKeys = new int[0];
			_selectedCount = 0;
			IsSelectionCountingSuppressed = false;
			_selectedObjectKeysString = String.Empty;
		}

		public void DeselectAll()
		{
			IsSelectionCountingSuppressed = true;
			foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in this)
				kvpDrawingObject.Value.Selected = false;
			IsSelectionCountingSuppressed = false;
			_selectedObjectKeys = new int[0];
			_selectedCount = 0;

			SetDirty();
		}

		public int GetSelectedObject()
		{
			foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in this)
				if (kvpDrawingObject.Value.Selected)
					return kvpDrawingObject.Key;
			return -1;
		}


		public int[] GetSelectedObjects()
		{
			return _selectedObjectKeys;
		}

		public int GetSoleSelectedTextObject()
		{
			foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in this)
			{
				if (kvpDrawingObject.Value.Selected & kvpDrawingObject.Value.ObjType == ObjectType.TextObject)
				{
					return kvpDrawingObject.Key;
				}
			}
			return -1;
		}

		public int GetSoleSelectedTextOrBlockObject()
		{
			foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in this)
			{
				if ((kvpDrawingObject.Value.Selected &
						(kvpDrawingObject.Value.ObjType == ObjectType.TextObject ||
							kvpDrawingObject.Value.ObjType == ObjectType.EllipseObject ||
							kvpDrawingObject.Value.ObjType == ObjectType.RectangleObject)))
					return kvpDrawingObject.Key;
			}
			return -1;
		}

		public void AddObject(int key, DrawingObject obj)
		{
			obj.SelectionChanged += new SelectionChangedHandler(SelectedObjectsChanged);
			obj.SetDrawingObjectsDirty += new SetDrawingObjectsDirtyHandler(SetDirty);
			this.Add(key, obj);
			UpdateSelectedArrayAndCount();

			SetDirty();
		}

		public void RemoveObject(int key)
		{
			this.Remove(key);
			UpdateSelectedArrayAndCount();

			SetDirty();
		}

		public void RemoveObjectAt(int index)
		{
			this.RemoveAt(index);
			UpdateSelectedArrayAndCount();

			SetDirty();
		}

		public void ChangeObjectWidth(int key, float width)
		{
			this[key].Width = width;

			SetDirty();
		}

		public void ChangeObjectPosition(int key, float left, float top)
		{
			this[key].Left = left;
			this[key].Top = top;

			SetDirty();
		}

		public void LockObject(int key)
		{
			this[key].IsLocked = true;

			SetDirty();
		}

		public void UnlockObject(int key)
		{
			this[key].IsLocked = false;

			SetDirty();
		}

		public void ChangeObjectHeight(int key, float height)
		{
			this[key].Height = height;

			SetDirty();
		}

		public void BringToFront(int key)
		{
			DrawingObject obj = this[key];
			int index = this.IndexOfKey(key);
			this.RemoveObjectAt(index);
			List<DrawingObject> drawingObjectList = new List<DrawingObject>(this.Values);
			this.Clear();

			foreach (DrawingObject o in drawingObjectList)
				this.AddObject(this.Count + 1, o);

			this.AddObject(this.Count + 1, obj);

			SetDirty();
		}

		public void BringForward(int key)
		{
			if (this.Count - 1 == key)
				return;

			DrawingObject obj = (DrawingObject)this[key];
			int index = this.IndexOfKey(key);
			this.RemoveObjectAt(index);
			List<DrawingObject> drawingObjectList = new List<DrawingObject>(this.Values);
			this.Clear();

			for (int n = 0; n < key + 1; n++)
				this.AddObject(this.Count + 1, drawingObjectList[n]);

			this.AddObject(this.Count + 1, obj);

			for (int n = key + 1; n < drawingObjectList.Count; n++)
				this.AddObject(this.Count + 1, drawingObjectList[n]);

			SetDirty();
		}

		public void SendBackward(int key)
		{
			if (key == 0)
				return;

			DrawingObject obj = (DrawingObject)this[key];
			int index = this.IndexOfKey(key);
			this.RemoveObjectAt(index);
			List<DrawingObject> drawingObjectList = new List<DrawingObject>(this.Values);
			this.Clear();

			for (int n = 0; n < key - 1; n++)
				this.AddObject(this.Count + 1, drawingObjectList[n]);

			this.AddObject(this.Count + 1, obj);

			for (int n = key - 1; n < drawingObjectList.Count; n++)
				this.AddObject(this.Count + 1, drawingObjectList[n]);

			SetDirty();
		}

		public void SendToBack(int key)
		{
			DrawingObject obj = (DrawingObject)this[key];
			int index = this.IndexOfKey(key);
			this.RemoveObjectAt(index);
			List<DrawingObject> drawingObjectList = new List<DrawingObject>(this.Values);
			this.Clear();

			this.AddObject(this.Count + 1, obj);
			for (int n = 0; n < drawingObjectList.Count; n++)
				this.AddObject(this.Count + 1, drawingObjectList[n]);

			SetDirty();
		}

		public void SelectedObjectsChanged()
		{
			if (!IsSelectionCountingSuppressed)
				UpdateSelectedArrayAndCount();

			SetDirty();
		}

		public void SetDirty()
		{
			_isDirty = true;
		}


		private void UpdateSelectedArrayAndCount()
		{
			_selectedCount = 0;
			_selectedObjectKeys = new int[0];
			_selectedObjectKeysString = String.Empty;

			foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in this)
			{
				if (kvpDrawingObject.Value.Selected)
				{
					_selectedCount++;
					int[] newselectedObjectKeysArray = new int[_selectedCount];
					for (int i = 0; i < _selectedObjectKeys.Length; i++)
						newselectedObjectKeysArray[i] = _selectedObjectKeys[i];
					newselectedObjectKeysArray[_selectedCount - 1] = kvpDrawingObject.Key;
					_selectedObjectKeys = newselectedObjectKeysArray;

					if (_selectedObjectKeysString.Length == 0)
						_selectedObjectKeysString = kvpDrawingObject.Key.ToString().Trim();
					else
						_selectedObjectKeysString += "," + kvpDrawingObject.Key.ToString().Trim();
				}
			}
		}

		public int[] KeysAtXY(float x, float y, float scale)
		{
			int[] keyArray = new int[0];

			foreach (KeyValuePair<int, DrawingObject> kvpDrawingObject in this)
			{
				float left = kvpDrawingObject.Value.Left * scale;
				float top = kvpDrawingObject.Value.Top * scale;
				float width = kvpDrawingObject.Value.Width * scale;
				float height = kvpDrawingObject.Value.Height * scale;
				RectangleF rect = new RectangleF(left, top, width, height);
				if (rect.Contains(x, y))
				{
					int[] newKeyArray = new int[keyArray.Length + 1];
					for (int i = 0; i < keyArray.Length; i++)
						newKeyArray[i] = keyArray[i];
					newKeyArray[newKeyArray.Length - 1] = kvpDrawingObject.Key;
					keyArray = newKeyArray;
				}
			}

			return keyArray;
		}


	}
}
