using System;
using Institute.Models.Models;

namespace Institute.Core.Payments
{
	public interface IPayment
	{
        Result<bool> Execute(decimal amount);
	}
}

