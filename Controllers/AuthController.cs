using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg2.Data;
using dotnet_rpg2.Dtos.User;
using dotnet_rpg2.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDTO request)
        {
            var response = await _authRepo.Register(
                new User { Username = request.Username }, request.Password


            );
            if (!response.success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}