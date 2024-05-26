using System;
using Institute.Models.Models;

namespace Institute.Models.Validators
{
	public interface IPaymentValidator
	{
		void Validate(Payment payment);
	}
}

