using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseAPI.DataTransferObject;
using BaseAPI.Models;

namespace BaseAPI.Application.Abstracts
{
    public interface IUserService:IDisposable
    {
        Task<IEnumerable<StudentDto>> GetStudnets();
        Task<bool> AddStudent(StudentDto studentInfo);
        Task<StudentDto> UpdateStudent(StudentDto studentInfo);
        Task<bool> DeleteStudent(Guid id);
        Task<StudentDto> GetStudentById(Guid id);

    }
}
