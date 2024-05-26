using System;
using Institute.Models.Models;

namespace Institute.Core.Payments
{
	public class PaymentService
	{
		private readonly IPayment _payment;
		public PaymentService(IPayment payment)
		{
			_payment = payment;
		}

		public Result<bool> ProcessPayment(decimal amount)
		{
			return _payment.Execute(amount);
		}
	}
}

