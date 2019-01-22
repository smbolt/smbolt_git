using System;

namespace NameTags
{
	[Serializable]
	public class Person
	{
		private string _fullName;
		public string FullName
		{
			get { return _fullName; }
			set { _fullName = value.Trim(); }
		}

		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set 
            { 
                _firstName = value.Trim(); 
                _fullName = (_firstName.Trim() + " " + _lastName.Trim()).Trim();
			}
		}

		private string _lastName;
		public string LastName
		{
			get { return _lastName.Trim(); }
			set 
            {
				_lastName = value;
				_fullName = (_firstName.Trim() + " " + _lastName.Trim()).Trim();
			}
		}

		private string _grade;
		public string Grade
		{
			get { return _grade; }
			set { _grade = value; }
		}

		private string _group;
		public string Group
		{
			get {return _group;}
			set {_group = value;}
		}

		private bool _selected;
		public bool Selected
		{
			get { return _selected; }
			set { _selected = value; }
		}
        
		public Person()
		{
			Initialize();
		}

		private void Initialize()
		{
			_fullName = "";
			_firstName = "";
			_lastName = "";
			_grade = "";
			_group = "";
			_selected = false;
		}
	}
}
