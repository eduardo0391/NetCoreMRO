using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCoreReact.Context;
using NetCoreReact.Model;

namespace NetCoreReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementController : ControllerBase
    {
        private readonly AppDbContext context;

        public MovementController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: api/Manager
        [HttpGet]
        public ActionResult Get()
        {
            try {
                return Ok(context.Movement.ToList());
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
                return Ok(context.Movement.FirstOrDefault(x => x.Id == id));
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
                context.Movement.Add(movement);
                context.SaveChanges();
                return CreatedAtRoute("GetManager", new { id = movement.Id }, movement);
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
                    context.Entry(value).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetManager", new { id = value.Id }, value);
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
                var manager = context.Movement.FirstOrDefault(x=>x.Id == id);
                if (manager != null)
                {
                    context.Movement.Remove(manager);
                    context.SaveChanges();
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
