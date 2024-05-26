using System;
using Institute.Models.Models;

namespace Institute.Models.Validators
{
	public class StudentValidator : IStudentValidator
	{
        const int _requiredAge = 18;
        public void Validate(Student student)
        {
            if (CalculateAge(DateTime.Now, student.Birthday) < _requiredAge)
            {
                throw new ArgumentException(string.Format("the student age cannot be less than {0}", _requiredAge));
            }
        }

        private int CalculateAge(DateTime currentDate, DateTime birthday)
        {
            int age = currentDate.Year - birthday.Year;
            if (currentDate < birthday.AddYears(age))
            {
                return age--;
            }

            return age;
        }
    }
}

