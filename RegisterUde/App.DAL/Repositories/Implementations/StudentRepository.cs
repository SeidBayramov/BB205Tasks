using App.CORE.Entities;
using App.DAL.Context;
using App.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Repositories.Implementations
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
