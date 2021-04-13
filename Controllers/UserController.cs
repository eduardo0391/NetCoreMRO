using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreReact.Context;
using NetCoreReact.Model;

namespace NetCoreReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext context;

        public UserController(AppDbContext context)
        {
            this.context = context;

        }
        // GET: api/Login
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            try
            {
                var user= context.User.FirstOrDefault(x => (login.User==x.UserName || login.User == x.Email));
                if (user != null)
                {
                    if (user.Password.Replace(" ", "") == Encrypt(login.Password))
                    {
                        if (!user.IsConfirmed)
                            return Ok(new Response { Message = "Your account needed to be actived, please check your email", Status = false });
                        else
                            return Ok(new Response { Message = "Success", Status = true });
                    }
                    else
                    {
                        return Ok(new Response { Message = "Password incorrect", Status = false });
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
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public ActionResult Post([FromBody] UserViewModel user)
        {
            try
            {
                User auxUser = new User();
                auxUser.Name = user.Name;
                auxUser.Password = this.decrypt(user.Password);
                auxUser.Email = user.Email;
                auxUser.UserName = user.UserName;
                auxUser.IsSuperUser = false;
                auxUser.IsConfirmed = false;
                auxUser.CreationDate = DateTime.Now;
                context.User.Add(auxUser);
                context.SaveChanges();
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

        /// Encripta una cadena
        private string Encrypt(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        private string decrypt(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
