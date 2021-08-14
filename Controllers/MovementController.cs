using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreReact.Context;
using NetCoreReact.Context.Repositories;
using NetCoreReact.Model;

namespace NetCoreReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userContext;

        public MovementController(AppDbContext context, IUserRepository userRepository )
        {
            this._context = context;
            this._userContext = userRepository;
        }
        // GET: api/Manager
        [HttpGet]
        public ActionResult Get()
        {
            try {
                return Ok(_context.Movement.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Manager/5
        [HttpGet("{id}", Name = "GetMovement")]
        public ActionResult Get(int id)
        {
            try
            {
                return Ok(_context.Movement.FirstOrDefault(x => x.Id == id));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Manager
        [HttpPost]
        public ActionResult Post([FromBody] Movement movement)
        {
            try
            {
                _context.Movement.Add(movement);
                _context.SaveChanges();
                return CreatedAtRoute("GetMovement", new { id = movement.Id }, movement);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Manager/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Movement value)
        {
            try
            {
                if (id == value.Id)
                {
                    _context.Entry(value).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetMovement", new { id = value.Id }, value);
                }
                else
                    return BadRequest();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var manager = _context.Movement.FirstOrDefault(x=>x.Id == id);
                if (manager != null)
                {
                    _context.Movement.Remove(manager);
                    _context.SaveChanges();
                    return Ok(id);
                }
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
