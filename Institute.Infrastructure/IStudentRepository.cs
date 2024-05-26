using System;
using System.Collections.Generic;
using Institute.Models.Models;

namespace Institute.Infrastructure
{
	public interface IStudentRepository
	{
		Result<int> Create(Student student);
		Result<bool> Exists(Student student);
        Result<ICollection<Student>> GetStudentsByCouseOfDate(DateTime? startCourse, DateTime? endCourse);
    }
}

