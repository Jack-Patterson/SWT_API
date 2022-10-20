using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using REST_JP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_JP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private APIDbContext _dbContext;

        public AccountController(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _dbContext.userRequests.ToList();
                if (users.Count == 0)
                {
                    return StatusCode(404, "No user has been found.");
                }

                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] Account request)
        {
            try
            {
                _dbContext.userRequests.Add(request);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }

            var users = _dbContext.userRequests.ToList();
            return Ok(users);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Account request)
        {
            try
            {
                var user = _dbContext.userRequests.AsNoTracking().FirstOrDefault(x => x.Id == request.Id);

                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                user = request;

                _dbContext.Entry(user).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }

            var users = _dbContext.userRequests.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult DeleteUser([FromRoute] int Id)
        {
            try
            {
                var user = _dbContext.userRequests.AsNoTracking().FirstOrDefault(x => x.Id == Id);
                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }

            var users = _dbContext.userRequests.ToList();
            return Ok(users);
        }
    }
}
