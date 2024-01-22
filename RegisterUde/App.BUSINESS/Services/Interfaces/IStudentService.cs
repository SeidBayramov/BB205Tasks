using App.BUSINESS.DTOs.Brand;
using App.BUSINESS.DTOs.Category;
using App.BUSINESS.DTOs.Student;
using App.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ICollection<Student>> GetAllAsync();
        Task<ICollection<Student>> RecycleBin();
        Task<Student> GetById(int id);
        Task Create(CreateStudentDto createStudentDto);
        Task Delete(int id);
        Task deleteAll();
        Task Update(UpdateStudentDto updateStudentDto);
        Task Restore();
    }
}
