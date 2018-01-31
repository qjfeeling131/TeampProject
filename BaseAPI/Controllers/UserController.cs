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
        /// <param name="userInfo">User info.</param>
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody] StudentDto userInfo)
        {
            return Ok(_userService.AddStudent(userInfo));
        }

        /// <summary>
        /// Updates the studnet.
        /// </summary>
        /// <returns>The studnet.</returns>
        /// <param name="userInfo">User info.</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudnet([FromBody] StudentDto userInfo){
            return Ok(await _userService.UpdateStudent(userInfo));
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
