using System;
using Institute.Core.Managers;
using Institute.Infrastructure;
using Institute.Models.Models;
using Institute.Models.Validators;
using Moq;
using Xunit;

namespace Institute.Test
{
    public class CourseManagerTest
    {
        [Fact]
        public void Create_Course_Success()
        {
            var target = Result<int>.Success(123);
            var mockCourseRepo = new Mock<ICourseRepository>();
            mockCourseRepo.Setup(r => r.Create(It.IsAny<Course>())).Returns(target);
            mockCourseRepo.Setup(r => r.Exist(It.IsAny<Course>())).Returns(Result<bool>.Success(false));

            var couserManager = new CourseManager(mockCourseRepo.Object);
            var course = new Course("C# - WCF", 23475M, new DateTime(2024, 5, 27), new DateTime(2024, 7, 14), new CourseValidator());
            var result = couserManager.Create(course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Course_Failure_Course_Is_Null()
        {
            var target = Result<int>.Failure("cannot create a course");
            var mockCourseRepo = new Mock<ICourseRepository>();

            var couserManager = new CourseManager(mockCourseRepo.Object);
            var result = couserManager.Create(null);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Course_Failure_Exists_Course()
        {
            const string courseName = "C# - Api Rest";
            var target = Result<int>.Failure(string.Format("the {0} course was previously registered", courseName));
            var mockCourseRepo = new Mock<ICourseRepository>();
            mockCourseRepo.Setup(r => r.Exist(It.IsAny<Course>())).Returns(Result<bool>.Success(true));

            var couserManager = new CourseManager(mockCourseRepo.Object);
            var course = new Course(courseName, 52360M, new DateTime(2024, 8, 6), new DateTime(2024, 12, 12), new CourseValidator());
            var result = couserManager.Create(course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Course_Failure_Check_Exists_Course()
        {
            const string courseName = "C# - Api Rest";
            var target = Result<int>.Failure("NotImplementedException");
            var mockCourseRepo = new Mock<ICourseRepository>();
            mockCourseRepo.Setup(r => r.Exist(It.IsAny<Course>())).Returns(Result<bool>.Failure("NotImplementedException"));

            var couserManager = new CourseManager(mockCourseRepo.Object);
            var course = new Course(courseName, 52360M, new DateTime(2024, 8, 6), new DateTime(2024, 12, 12), new CourseValidator());
            var result = couserManager.Create(course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }

        [Fact]
        public void Create_Course_Failure_Create_Course()
        {
            const string courseName = "C# - Api Rest";
            var target = Result<int>.Failure("NotImplementedException");
            var mockCourseRepo = new Mock<ICourseRepository>();

            mockCourseRepo.Setup(r => r.Create(It.IsAny<Course>())).Returns(target);
            mockCourseRepo.Setup(r => r.Exist(It.IsAny<Course>())).Returns(Result<bool>.Success(false));

            var couserManager = new CourseManager(mockCourseRepo.Object);
            var course = new Course(courseName, 52360M, new DateTime(2024, 8, 6), new DateTime(2024, 12, 12), new CourseValidator());
            var result = couserManager.Create(course);

            Assert.Equal(target.Error, result.Error);
            Assert.Equal(target.Value, result.Value);
            Assert.Equal(target.IsSucces, result.IsSucces);
        }
    }
}

