using System;
using Institute.Models.Models;

namespace Institute.Infrastructure
{
    public class PaymentRepository : IPaymentRepository
    {
        public Result<bool> ExistPayment(int course, int student)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Register(decimal amount, int course, int student)
        {
            throw new NotImplementedException();
        }
    }
}

