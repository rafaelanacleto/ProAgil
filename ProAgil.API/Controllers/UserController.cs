using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Net;
using System.Net.Http.Headers;
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
        public async Task<IActionResult> GetUser()
        {
            try
            {                
                return Ok(new User());
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Erro Interno API - " + ex.Message);
            }

        }



    }
}