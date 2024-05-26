using System;
using Institute.Models.Models;

namespace Institute.Core
{
	public interface ICourseManager
	{
        Result<int> Create(Course course);
	}
}

