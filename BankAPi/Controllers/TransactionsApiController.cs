using BankApiBussinessLayer;
using BankApiDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [HttpGet( "GetAllTotalBalances")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<clsBalancesDTO>> GetAllTotalBalances()
        {
            List<clsBalancesDTO> list = new List<clsBalancesDTO>();
            if ((list = BankApiBussinessLayer.clsTransactions.GetAllBalances()).Count==0 )
            {
                return NotFound("No Clients !");
            }
            return Ok(list);
        }

        [HttpPost("Transfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object>Transfer(clsTransferDTO TransferDTO)
        {
          
            if (TransferDTO.AccountNumber_Sender ==string.Empty || TransferDTO.AccountNumber_Receiver == string.Empty ||
                TransferDTO.Amount <=0 || TransferDTO.UserID<=0)
            {
                return BadRequest("Invalid data !");
            }
           int result =  BankApiBussinessLayer.clsTransactions.TransferOperation(TransferDTO);
            if (result > 1)
            {
                return Ok(new { success = true , message = "Transfer done successfully..."});
            }
            else if (result == -1)
            {
                return BadRequest(new { success = false, message = "AccountNum1 is equal the same AccountNum2 !" });

            }
            else if (result == -2)
            {
                return BadRequest(new { success = false, message = "Invalid amount !" });
  
            }
            else if (result == -3)
            {
                return NotFound(new { success = false, message = "One of account numbers are not exist !" });
            }
            else if (result == -4)
            {
                return BadRequest(new { success = false, message = "Amount is less than Balance !" });
                
            }
            else if (result == -5)
            {
                return BadRequest(new { success = false, message = "User is not exist !" });
            }
            else 
            {
                return BadRequest(new { success = false, message = "Something went wrong ! [don't worry everything is rolledback]" });
            }
        }

        [HttpGet("GetAllTransferLogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<clsTransferLogsDTO>> GetAllTransferLogs()
        {
          List<clsTransferLogsDTO> Logs =   BankApiBussinessLayer.clsTransactions.GetAllTransferLogs();
            if (Logs.Count ==0)
            {
                return NotFound("No TransferLogs...");
            }
            return Ok(Logs);
        }
    }
}
