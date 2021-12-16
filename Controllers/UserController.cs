using System;
using Microsoft.AspNetCore.Mvc;
using Minimal.DataAccess;
using Minimal.Models;

namespace Minimal.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : FireBaseController
    {
        readonly UserRepository _repo;
        public UserController(UserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("createUser")]
        public IActionResult CreateNewUser(User newUser)
        {
            _repo.CreateNewUser(newUser);

            return Created($"/api/users/{newUser.UserId}", newUser);
        }

        [HttpGet("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_repo.GetAllUsers());
        }

        [HttpGet("getUserById/{userId}")]
        public IActionResult GetUserById(Guid userId)
        {
            return Ok(_repo.GetUserById(userId));
        }

        [HttpGet("getUserByFirebaseKey/{firebaseKey}")]
        public IActionResult GetUserByFirebaseKey(String firebaseKey)
        {
            return Ok(_repo.GetUserByFirebaseKey(firebaseKey));
        }

        [HttpPut("updateUser/{userId}")]
        public IActionResult UpdateUser(Guid userId, User user)
        {
            var userToUpdate = _repo.GetUserById(userId);

            if (userToUpdate == null)
            {
                return NotFound($"Could not find user with the id {userId} for updating");
            }

            var updatedUser = _repo.UpdateUser(userId, user);

            return Ok(updatedUser);
        }

        [HttpPut("softDelete/{userId}")]
        public IActionResult SoftDeleteUser(Guid userId, User user)
        {
            var userToSoftDelete = _repo.GetUserById(userId);

            if (userToSoftDelete == null)
            {
                return NotFound($"Could not find user with the id {userId} for removal");
            }

            var softDeletedUser = _repo.SoftDeleteUser(userId, user);

            return Ok(softDeletedUser);
        }

        [HttpDelete("deleteUser/{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            _repo.HardDeleteUser(userId);

            return Ok();
        }
    }
}
