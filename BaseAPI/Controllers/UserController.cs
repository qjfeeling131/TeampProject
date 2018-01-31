using System;
using System.Threading.Tasks;
using BaseAPI.Application;
using Microsoft.AspNetCore.Mvc;
using BaseAPI.DataTransferObject;
using BaseAPI.Filter;
using BaseAPI.Application.Abstracts;

namespace BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(ExceptionFilter))]
    public class StudentController:Controller
    {
        private readonly IUserService _userService;
        public StudentController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>The all users.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return  Ok(await _userService.GetStudnets());
        }

        /// <summary>
        /// Adds the student.
        /// </summary>
        /// <returns>The student.</returns>
        /// <param name="studentInfo">Student info.</param>
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto studentInfo)
        {
            return Ok(await _userService.AddStudent(studentInfo));
        }

        /// <summary>
        /// Updates the studnet.
        /// </summary>
        /// <returns>The studnet.</returns>
        /// <param name="studentInfo">Student info.</param>
        [HttpPut()]
        public async Task<IActionResult> UpdateStudnet([FromBody]StudentDto studentInfo){
            return Ok(await _userService.UpdateStudent(studentInfo));
        }

        /// <summary>
        /// Deletes the studnet.
        /// </summary>
        /// <returns>The studnet.</returns>
        /// <param name="Id">Identifier.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudnet(Guid Id)
        {
            return Ok(await _userService.DeleteStudent(Id));
        }
    }

}
