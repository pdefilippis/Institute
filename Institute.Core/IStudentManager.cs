using System;
using System.Collections.Generic;
using Institute.Models;
using Institute.Models.Models;

namespace Institute.Core
{
	public interface IStudentManager
	{
		Result<int> Create(Student student);
		Result<bool> HireCourse(Student student, Course course);
		Result<ICollection<Student>> GetStudentsByCouseOfDate(DateTime? startCourse, DateTime? endCourse);
	}
}

