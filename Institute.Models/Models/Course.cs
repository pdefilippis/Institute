using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Institute.Models.Validators;

namespace Institute.Models.Models
{
	public class Course
	{
        private int? _id;
        private string _name;
        private decimal _registrationFee;
        private DateTime _start;
        private DateTime _end;
        private ICollection<Student> _students;
        private readonly ICourseValidator _validator;

        public Course(string name, decimal refostrationFee, DateTime start, DateTime end, ICourseValidator validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _name = name;
            _registrationFee = refostrationFee;
            _start = start;
            _end = end;

            _validator.Validate(this);
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                _validator.Validate(this);
            }
        }

        public decimal RegistrationFee
        {
            get => _registrationFee;
            set
            {
                _registrationFee = value;
                _validator.Validate(this);
            }
        }

        public DateTime Start
        {
            get => _start;
            set
            {
                _start = value;
                _validator.Validate(this);
            }
        }

        public DateTime End
        {
            get => _end;
            set
            {
                _end = value;
                _validator.Validate(this);
            }
        }

        public ICollection<Student> Students
        {
            get => _students;
            set
            {
                _students = value;
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

