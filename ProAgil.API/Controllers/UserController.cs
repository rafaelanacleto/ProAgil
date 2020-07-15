using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Dtos;
using ProAgil.Domain;
using ProAgil.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using ProAgil.Domain.Identity;
using Microsoft.AspNetCore.Authorization;

namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper _mapper;        

        public UserController(IConfiguration config,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,            
                              IMapper mapper)
        {
            this.config = config;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._mapper = mapper;            
        }


        [HttpGet]
        public async Task<IActionResult> GetUser(UserDto user)
        {
            try
            {                
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro API - " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {                
                var user = await this.userManager.FindByNameAsync(userLogin.UserName);
                    


            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro API - " + ex.Message);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto user)
        {
            try
            { 
                var usu = _mapper.Map<User>(user);
                var result = await this.userManager.CreateAsync(usu, user.Password);
                var userToReturn = _mapper.Map<UserDto>(usu);

                if (result.Succeeded)
                {
                    return Created("GetUser", userToReturn);
                }
                return BadRequest(result.Errors);         
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro API - " + ex.Message);
            }
        }
    }
}