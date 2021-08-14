using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreReact.Context;
using NetCoreReact.Context.Repositories;
using NetCoreReact.Helpers;
using NetCoreReact.Model;
using NetCoreReact.Model.ViewModel;

namespace NetCoreReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userContext;

        private readonly JWTService _JWTService;

        public UserController(IUserRepository userContext, JWTService _JWTService)
        {
            this._userContext = userContext;
            this._JWTService = _JWTService;
        }
        // GET: api/Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            try
            {
                var user= _userContext.userByName(login.User);
                if (user != null)
                {
                    if (user.Password.Replace(" ", "") == _userContext.Encrypt(login.Password))
                    {
                        if (!user.IsConfirmed)
                            return Ok(new LoginResponse { Message = "Your account needed to be actived, please check your email", Status = false });
                        else
                        {
                            var token = _JWTService.Generate(user.Id);
                            Response.Cookies.Append(key: "token", value: token, options: new CookieOptions
                            {
                               HttpOnly = true
                            });
                            return Ok(value: 
                                            new LoginResponse { Message = "Success", Status = true,
                                                                User = new UserResponse { Token = token, Email = user.Email, User = user.UserName }
                                    });
                        }
                            
                    }
                    else
                    {
                        return Ok(new LoginResponse { Message = "Password incorrect", Status = false });
                    }
                }
                else
                    return Ok(new Response { Message = "User is not exist", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            var jwt = Request.Cookies["token"];
            var token = _JWTService.Verify(jwt);
            int userID = int.Parse(token.Issuer);
            return userID.ToString();
        }

        // POST: api/User
        [HttpPost]
        public ActionResult Post([FromBody] UserViewModel user)
        {
            try
            {
                User auxUser = new User();
                auxUser.Name = user.Name;
                auxUser.Password = _userContext.Encrypt(user.Password);
                auxUser.Email = user.Email;
                auxUser.UserName = user.UserName;
                auxUser.IsSuperUser = false;
                auxUser.IsConfirmed = false;
                auxUser.CreationDate = DateTime.Now;
                this._userContext.Add(auxUser);
                return Ok("The user was created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
