using System;
using System.Collections.Generic;
using Institute.Models.Models;

namespace Institute.Infrastructure
{
    public class StudentRepository : IStudentRepository
    {
        public Result<int> Create(Student student)
        {
            throw new NotImplementedException();
        }

        public Result<bool> Exists(Student student)
        {
            throw new NotImplementedException();
        }

        public Result<ICollection<Student>> GetStudentsByCouseOfDate(DateTime? startCourse, DateTime? endCourse)
        {
            throw new NotImplementedException();
        }
    }
}

