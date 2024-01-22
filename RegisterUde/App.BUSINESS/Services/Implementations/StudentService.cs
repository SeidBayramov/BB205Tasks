using App.BUSINESS.DTOs.Brand;
using App.BUSINESS.DTOs.Category;
using App.BUSINESS.DTOs.Student;
using App.BUSINESS.Services.Interfaces;
using App.CORE.Entities;
using App.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.Services.Implementations
{
    public class StudentService:IStudentService
    {
        private readonly IStudentRepository _repo;

        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task Create(CreateStudentDto createStudentDto)
        {
          
            Student student = new Student()
            {
                Name = createStudentDto.Name,
                Surname = createStudentDto.Surname,
                Age = createStudentDto.Age,
                CreatedAt = DateTime.Now,




            };
            await _repo.Create(student);

            _repo.Save();
        }

        public async Task Delete(int id)
        {
            _repo.delete(id);
            _repo.Save();
        }

        public async Task<ICollection<Student>> GetAllAsync()
        {
            var students = await _repo.GetAllAsync();
            return await students.ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            return await _repo.GetById(id);
        }

        public async Task<ICollection<Student>> RecycleBin()
        {
            var students = await _repo.RecycleBin();
            return await students.ToListAsync();
        }

        public async Task Update(UpdateStudentDto updateStudentDto)
        {

            if (updateStudentDto == null) throw new Exception("Bad Request");

            var existingStudent = await _repo.GetById(updateStudentDto.Id);
            existingStudent.Name = updateStudentDto.Name;
            existingStudent.Surname = updateStudentDto.Surname;
            existingStudent.UpdatedAt = DateTime.Now;


            _repo.Update(existingStudent);
            _repo.Save();
        }
        public async Task Restore()
        {
            _repo.Restore();
            _repo.Save();
        }

        public async Task deleteAll()
        {
            _repo.deleteAll();
            _repo.Save();
        }
    }
}
