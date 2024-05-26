using System;
using System.Collections.Generic;
using Institute.Core.Payments;
using Institute.Infrastructure;
using Institute.Models.Models;

namespace Institute.Core.Managers
{
	public class StudentManager : IStudentManager
	{
        private readonly IStudentRepository _studentRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPayment _paymentService;

        public StudentManager(IStudentRepository studentRepository, IPaymentRepository paymentRepository,
            IPayment paymentService)
        {
            _studentRepository = studentRepository;
            _paymentRepository = paymentRepository;
            _paymentService = paymentService;
        }

        public Result<int> Create(Student student)
        {
            if (student == null)
                return Result<int>.Failure("cannot create a student");

            var existsStudent = _studentRepository.Exists(student);
            if (!existsStudent.IsSucces)
                return Result<int>.Failure(existsStudent.Error);

            if (existsStudent.Value)
                return Result<int>.Failure(string.Format("the {0}, {1} course was previously registered", student.LastName, student.FirsName));

            var resStudent = _studentRepository.Create(student);
            if (!resStudent.IsSucces)
                return Result<int>.Failure(resStudent.Error);

            return Result<int>.Success(resStudent.Value);

        }

        public Result<ICollection<Student>> GetStudentsByCouseOfDate(DateTime? startCourse, DateTime? endCourse)
        {
            if (startCourse > endCourse)
                return Result<ICollection<Student>>.Failure("the start date cannot be greater than the end date");

            return _studentRepository.GetStudentsByCouseOfDate(startCourse, endCourse);
        }

        public Result<bool> HireCourse(Student student, Course course)
        {
            if (!student.Id.HasValue || !course.Id.HasValue)
                return Result<bool>.Failure("a course and a student must be assigned");

            var processPayment = new PaymentService(_paymentService).ProcessPayment(course.RegistrationFee);

            if (!processPayment.IsSucces)
                return Result<bool>.Failure(processPayment.Error);

            if (!processPayment.Value)
                return Result<bool>.Failure("payment could not be processed");
                

            return _paymentRepository.Register(course.RegistrationFee, course.Id.Value, student.Id.Value);
        }
    }
}

