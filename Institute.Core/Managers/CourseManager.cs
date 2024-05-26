using System;
using Institute.Infrastructure;
using Institute.Models.Models;

namespace Institute.Core.Managers
{
    public class CourseManager : ICourseManager
    {
        private readonly ICourseRepository _courseRepository;

        public CourseManager(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Result<int> Create(Course course)
        {
            if (course == null)
                return Result<int>.Failure("cannot create a course");

            var resultExistCourse = _courseRepository.Exist(course);
            if (!resultExistCourse.IsSucces)
                return Result<int>.Failure(resultExistCourse.Error);

            if (resultExistCourse.Value)
                return Result<int>.Failure(string.Format("the {0} course was previously registered", course.Name));
            

            return _courseRepository.Create(course);
        }
    }
}

