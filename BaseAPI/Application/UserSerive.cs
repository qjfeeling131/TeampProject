using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseAPI.DataTransferObject;
using BaseAPI.EntityFrameworkCore;
using BaseAPI.Models;
using Microsoft.EntityFrameworkCore;
using BaseAPI.Application.Abstracts;
namespace BaseAPI.Application
{
    public class UserSerive:IUserService
    {
        private readonly BaseContext _baseContext;
        private readonly IMapper _mapper;
        public UserSerive(BaseContext dbContext,IMapper mapper)
        {
            _baseContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StudentDto>> GetStudnets()
        {
            List<StudentDto> userDto = new List<StudentDto>();
            try
            {
                var students = await _baseContext.ss_student.ToListAsync();
                students.ForEach(item => userDto.Add(_mapper.Map<Student, StudentDto>(item)));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userDto;
        }

        public async Task<bool> AddStudent(StudentDto studentInfo)
        {
            try
            {
                Student studentModel = Mapper.Map<StudentDto, Student>(studentInfo);
                await _baseContext.ss_student.AddAsync(studentModel);
                await _baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"It occurs some error when adding the model, detalied information as below:{ex.Message}");
            }
        }

        public async Task<StudentDto> UpdateStudent(StudentDto studentInfo){
            var studentList = await _baseContext.ss_student.Where(item => item.Id.Equals(studentInfo.Id)).ToListAsync();
            var studentModel = studentList.FirstOrDefault();
            if (studentModel!=null)
            {
                studentModel.Name = studentInfo.Name;
                studentModel.Age = studentInfo.Age;
            }

            try
            {
                var result = _baseContext.ss_student.Update(studentModel).Entity;
                await _baseContext.SaveChangesAsync();
                return Mapper.Map<Student,StudentDto>(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"It occurs some error when updating the model, detalied information as below:{ex.Message}");
            }

        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            var studentList = await _baseContext.ss_student.Where(item => item.Id.Equals(id)).ToListAsync();
            var studentModel = studentList.FirstOrDefault();
            if (studentModel == null)
            {
                throw new ArgumentNullException("Can not be found the studnet model, please re-try agian");
            }
            try
            {
                _baseContext.ss_student.Remove(studentModel);
                await _baseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<StudentDto> GetStudentById(Guid id){
            return null;
        }
        public void Dispose()
        {

        }

    }
}
