using System;
using System.Collections.Generic;
using Institute.Models.Validators;

namespace Institute.Models.Models
{
	public class Student
	{
        private int? _id;
        private string _firstName;
        private string _lastName;
        private DateTime _birthday;
        private ICollection<Course> _courses;
        private readonly IStudentValidator _validator;

        public Student(string firstName, string lastName, DateTime birthday, IStudentValidator validator)
		{
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _firstName = firstName;
            _lastName = lastName;
            _birthday = birthday;

            _validator.Validate(this);
		}

        public string FirsName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                _validator.Validate(this);
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                _validator.Validate(this);
            }
        }

        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                _validator.Validate(this);
            }
        }

        public ICollection<Course> Courses
        {
            get => _courses;
            set
            {
                _courses = value;
                _validator.Validate(this);
            }
        }

        public int? Id
        {
            get => _id;
            set
            {
                _id = value;
                _validator.Validate(this);
            }
        }
	}
}

