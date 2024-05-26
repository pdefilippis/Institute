using System;
using Institute.Models.Models;

namespace Institute.Infrastructure
{
	public interface ICourseRepository
	{
		Result<int> Create(Course course);
        Result<bool> Exist(Course course);
	}
}

