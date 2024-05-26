using System;
using Institute.Models.Models;

namespace Institute.Infrastructure
{
	public interface IPaymentRepository
	{
		Result<bool> Register(decimal amount, int course, int student);
		Result<bool> ExistPayment(int course, int student);
	}
}

