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
    public class TransactionController : ControllerBase
    {
        private APIDbContext _dbContext;

        public TransactionController(APIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllTransactionsForID/{Id}")]
        public IActionResult GetAllTransactionsForID([FromRoute] int Id)
        {
            try
            {
                var transactions = new List<Transaction>();

                foreach(Transaction t in _dbContext.transactions.ToList())
                {
                    if (t.SenderAccountID == Id)
                    {
                        transactions.Add(t);
                    }
                }

                if (transactions.Count == 0)
                {
                    return StatusCode(404, "No transactions has been found for this user.");
                }

                return Ok(transactions);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }

        [HttpPost("CreateTransaction")]
        public IActionResult CreateTransaction(Transaction request)
        {
            try
            {
                _dbContext.transactions.Add(request);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }

            var transactions = _dbContext.transactions.ToList();
            return Ok(transactions);
        }

        [HttpGet("GetNextID")]
        public IActionResult GetNextID()
        {
            try
            {
                var transactions = _dbContext.transactions.ToList();

                if (transactions.Count == 0)
                {
                    return Ok(0);
                }

                var latestId = 0;
                foreach (Transaction t in transactions)
                {
                    if (t.TransId > latestId) latestId = t.TransId;
                }

                return Ok(latestId + 1);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error has occurred.\n {e}");
            }
        }
    }
}
