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
    [Route("banksys/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private APIDbContext _dbContext;

        public AccountController(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _dbContext.accounts.ToList();
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

        [HttpGet("GetUserById/{Id}")]
        public IActionResult GetUserById([FromRoute] int Id)
        {
            try
            {
                var users = _dbContext.accounts.ToList();

                var user = FilterAccount(users, Id);
                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpGet("GetUserByName/{Name}")]
        public IActionResult GetUserByName([FromRoute] string name)
        {
            try
            {
                var users = _dbContext.accounts.ToList();
                
                var user = FilterAccount(users, name);
                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpGet("GetNameById/{Id}")]
        public IActionResult GetNameById([FromRoute] int Id)
        {
            try
            {
                var users = _dbContext.accounts.ToList();

                var user = FilterAccount(users, Id);
                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                return Ok(user.Name);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpGet("GetPacById/{Id}")]
        public IActionResult GetPacById([FromRoute] int id)
        {
            try
            {
                var users = _dbContext.accounts.ToList();

                var user = FilterAccount(users, id);
                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                return Ok(user.Pac);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpGet("GetBalanceById/{Id}")]
        public IActionResult GetBalanceById([FromRoute] int id)
        {
            try
            {
                var users = _dbContext.accounts.ToList();

                var user = FilterAccount(users, id);
                if (user == null)
                {
                    return StatusCode(404, "No user has been found.");
                }

                return Ok(user.Balance);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpGet("GetNextID")]
        public IActionResult GetNextID()
        {
            try
            {
                var users = _dbContext.accounts.ToList();

                if (users.Count == 0)
                {
                    return Ok(0);
                }

                var latestId = 0;
                foreach (Account a in users)
                {
                    if (a.AccountId > latestId) latestId = a.AccountId;
                }

                return Ok(latestId+1);
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
                _dbContext.accounts.Add(request);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }

            var users = _dbContext.accounts.ToList();
            return Ok(users);
        }
        
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Account request)
        {
            try
            {
                var user = _dbContext.accounts.AsNoTracking().FirstOrDefault(x => x.AccountId == request.AccountId);

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

            var users = _dbContext.accounts.ToList();
            return Ok(users);
        }

        private static Account FilterAccount(List<Account> accounts, int id)
        {
            foreach(Account a in accounts)
            {
                if (a.AccountId == id)
                {
                    return a;
                }
            }
            return null;
        }

        private static Account FilterAccount(List<Account> accounts, string name)
        {
            foreach (Account a in accounts)
            {
                string nameFixed = "";
                foreach (char c in name)
                {
                    if (c == '_')
                    {
                        nameFixed += ' ';

                    }
                    else
                    {
                        nameFixed += c;
                    }
                }
                Console.WriteLine(nameFixed);
                if (a.Name.Equals(nameFixed))
                {
                    return a;
                }
            }
            return null;
        }
    }
}
