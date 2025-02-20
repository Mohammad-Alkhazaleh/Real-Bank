using BankApiBussinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsApiController : ControllerBase
    {
        [HttpPost("{AccountNumber}/{TransactionsAmount}", Name = "Transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> Deposit(string AccountNumber , decimal TransactionsAmount)
        {
            if (AccountNumber==string.Empty)
            {
                return BadRequest(new { success = false,message =  "Invalid Data !" });
            }
           
            if(BankApiBussinessLayer.clsTransactions.Transactions(AccountNumber, TransactionsAmount))
            {
                return Ok(new { success = true, message = "Transaction done successfully..." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Transaction failed..." });
            }
        }

       
    }
}
