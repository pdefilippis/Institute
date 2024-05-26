using System;
using System.Collections.Generic;
using Institute.Core.Managers;
using Institute.Core.Payments;
using Institute.Infrastructure;
using Institute.Models.Models;
using Institute.Models.Validators;
using Moq;
using Xunit;

namespace Institute.Test
{
	public class StudentManagerTest
	{
        [Fact]
        public void Get_Students_Success()
        {
            var students = new List<Student>();

            students.Add(new Student("Mario", "Galeano", new DateTime(1973, 5, 16), new StudentValidator()));
            students.Add(new Student("Olivia", "Mansilla", new DateTime(1991, 10, 29), new StudentValidator()));

            var target = Result<ICollection<Student>>.Success(students);

            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(r => r.GetStudentsByCouseOfDate(It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).
                Returns(target);

            var stuentManager = new StudentManager(mockStudentRepo.Object, null, null);
            var result = stuentManager.GetStudentsByCouseOfDate(new DateTime(2024, 5, 22), DateTime.Now);
            
            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Get_Students_Failure_Date_From_and_To()
        {
            var target = Result<ICollection<Student>>.Failure("the start date cannot be greater than the end date");

            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(r => r.GetStudentsByCouseOfDate(It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).
                Returns(target);

            var stuentManager = new StudentManager(mockStudentRepo.Object, null, null);
            var result = stuentManager.GetStudentsByCouseOfDate(new DateTime(2024, 6, 22), new DateTime(2024, 5, 22));

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Failure_Null_Student()
        {
            var target = Result<int>.Failure("cannot create a student");

            var stuentManager = new StudentManager(null, null, null);
            var result = stuentManager.Create(null);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Failure_Existe_Student_Error()
        {
            var target = Result<int>.Failure("NotImplementedException");
            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(r => r.Exists(It.IsAny<Student>())).
                Returns(Result<bool>.Failure("NotImplementedException"));

            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());

            var stuentManager = new StudentManager(mockStudentRepo.Object, null, null);
            var result = stuentManager.Create(student);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Failure_Existe_Student()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var target = Result<int>.Failure(string.Format("the {0}, {1} course was previously registered", student.LastName, student.FirsName));
            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(r => r.Exists(It.IsAny<Student>())).
                Returns(Result<bool>.Success(true));

            var stuentManager = new StudentManager(mockStudentRepo.Object, null,null);
            var result = stuentManager.Create(student);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Failure_Student()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var target = Result<int>.Failure("NotImplementedException");
            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(r => r.Exists(student)).
                Returns(Result<bool>.Success(false));

            mockStudentRepo.Setup(r => r.Create(student)).Returns(target);

            var stuentManager = new StudentManager(mockStudentRepo.Object, null, null);
            var result = stuentManager.Create(student);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Success_Student()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var target = Result<int>.Success(15478);
            var mockStudentRepo = new Mock<IStudentRepository>();
            mockStudentRepo.Setup(r => r.Exists(student)).
                Returns(Result<bool>.Success(false));

            mockStudentRepo.Setup(r => r.Create(student)).Returns(target);

            var stuentManager = new StudentManager(mockStudentRepo.Object, null, null);
            var result = stuentManager.Create(student);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void HireCourse_Failure_Student_Without_Id()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var course = new Course("C# - .Net core", 11800M, new DateTime(2024, 7, 13), new DateTime(2024, 8, 31), new CourseValidator());
            course.Id = 1477;

            var target = Result<bool>.Failure("a course and a student must be assigned");
            var stuentManager = new StudentManager(null, null, null);
            var result = stuentManager.HireCourse(student, course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void HireCourse_Failure_Course_Without_Id()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var course = new Course("C# - .Net core", 11800M, new DateTime(2024, 7, 13), new DateTime(2024, 8, 31), new CourseValidator());
            student.Id = 7;

            var target = Result<bool>.Failure("a course and a student must be assigned");
            var stuentManager = new StudentManager(null, null, null);
            var result = stuentManager.HireCourse(student, course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void HireCourse_Payment_Proccess_Error()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var course = new Course("C# - .Net core", 11800M, new DateTime(2024, 7, 13), new DateTime(2024, 8, 31), new CourseValidator());
            student.Id = 7;
            course.Id = 1477;

            var target = Result<bool>.Failure("NotImplementedException");
            var mockPaymentSvc = new Mock<IPayment>();
            mockPaymentSvc.Setup(s => s.Execute(It.IsAny<Decimal>())).Returns(target);
            
            var stuentManager = new StudentManager(null, null, mockPaymentSvc.Object);
            var result = stuentManager.HireCourse(student, course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void HireCourse_Payment_Proccess_Success_Without_Payment()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var course = new Course("C# - .Net core", 11800M, new DateTime(2024, 7, 13), new DateTime(2024, 8, 31), new CourseValidator());
            student.Id = 7;
            course.Id = 1477;

            var target = Result<bool>.Failure("payment could not be processed");
            var mockPaymentSvc = new Mock<IPayment>();
            mockPaymentSvc.Setup(s => s.Execute(It.IsAny<Decimal>())).Returns(Result<bool>.Success(false));

            var stuentManager = new StudentManager(null, null, mockPaymentSvc.Object);
            var result = stuentManager.HireCourse(student, course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void HireCourse_Failure_Register_Payment()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var course = new Course("C# - .Net core", 11800M, new DateTime(2024, 7, 13), new DateTime(2024, 8, 31), new CourseValidator());
            student.Id = 7;
            course.Id = 1477;

            var target = Result<bool>.Failure("Error Establishing a Database Connection");
            var mockPaymentSvc = new Mock<IPayment>();
            mockPaymentSvc.Setup(s => s.Execute(It.IsAny<decimal>())).Returns(Result<bool>.Success(true));

            var mockPaymentRepo = new Mock<IPaymentRepository>();
            mockPaymentRepo.Setup(r => r.Register(It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>())).
                Returns(target);

            var stuentManager = new StudentManager(null, mockPaymentRepo.Object, mockPaymentSvc.Object);
            var result = stuentManager.HireCourse(student, course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void HireCourse_Success_Register_Payment()
        {
            var student = new Student("Grisel", "Rodriguez", new DateTime(1992, 10, 29), new StudentValidator());
            var course = new Course("C# - .Net core", 11800M, new DateTime(2024, 7, 13), new DateTime(2024, 8, 31), new CourseValidator());
            student.Id = 7;
            course.Id = 1477;

            var target = Result<bool>.Success(true);
            var mockPaymentSvc = new Mock<IPayment>();
            mockPaymentSvc.Setup(s => s.Execute(It.IsAny<decimal>())).Returns(Result<bool>.Success(true));

            var mockPaymentRepo = new Mock<IPaymentRepository>();
            mockPaymentRepo.Setup(r => r.Register(It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>())).
                Returns(target);

            var stuentManager = new StudentManager(null, mockPaymentRepo.Object, mockPaymentSvc.Object);
            var result = stuentManager.HireCourse(student, course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }
    }
}

