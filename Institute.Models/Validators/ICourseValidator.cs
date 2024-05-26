using System;
using Institute.Models.Models;

namespace Institute.Models.Validators
{
	public interface ICourseValidator
	{
		void Validate(Course course);
	}
}

