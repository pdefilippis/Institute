using System;
using Institute.Models.Validators;

namespace Institute.Models.Models
{
	public class Payment
	{
		private decimal _fee;
		private int _course;
		private int _student;
		private readonly IPaymentValidator _validator;

        public Payment(decimal fee, int course, int student, IPaymentValidator validator)
		{
			_validator = validator ?? throw new ArgumentException(nameof(validator));
			_course = course;
			_student = student;
			_fee = fee;

			_validator.Validate(this);
		}

		public decimal Fee
		{
			get => _fee;
			set
			{
				_fee = value;
				_validator.Validate(this);
			}
		}

		public int Course
		{
			get => _course;
			set
			{
				_course = value;
				_validator.Validate(this);
			}
		}

		public int Student
		{
			get => _student;
			set
			{
				_student = value;
				_validator.Validate(this);
			}
		}
	}
}

