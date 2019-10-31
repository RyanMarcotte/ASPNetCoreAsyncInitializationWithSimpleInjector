using System;
using System.Collections.Generic;
using System.Text;

namespace AISIDemo.School.Domain
{
	public class Course
	{
		public Course(int id, string title, int credits)
		{
			ID = id;
			Title = title;
			Credits = credits;
		}

		public int ID { get; }
		public string Title { get; }
		public int Credits { get; }
	}
}
