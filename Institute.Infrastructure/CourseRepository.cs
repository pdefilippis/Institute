using System;
using Institute.Models.Models;

namespace Institute.Infrastructure
{
    public class CourseRepository : ICourseRepository
    {
        public Result<int> Create(Course course)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Exist(Course course)
        {
            throw new NotImplementedException();
        }
    }
}

