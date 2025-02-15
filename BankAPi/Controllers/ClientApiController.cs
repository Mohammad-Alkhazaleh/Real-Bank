using BankApiBussinessLayer;
using BankApiDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientApiController : ControllerBase
    {
        [HttpGet("AllClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<clsClientsDTO>> GetAllStudents()
        {
            List<clsClientsDTO> StudentList = (BankApiBussinessLayer.clsClients.GetAllClients());
            if (StudentList.Count == 0)
            {
                return NotFound("No Students Found");
            }
            return Ok(StudentList);
        }

        [HttpGet("{ClientID}", Name = "GetClientByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<clsClientsDTO> GetClientByID(int ClientID)
        {
            if (ClientID < 0)
            {
                return BadRequest("Invalid ClientID ! ");
            }
            clsClients Client = BankApiBussinessLayer.clsClients.GetClientByID(ClientID);
            if (Client == null)
            {
                return NotFound("Client is not exist !");
            }
            clsClientsDTO CDTO = Client.ClientDTO;
            return Ok(CDTO);

        }

        [HttpPost("AddNewClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<clsClientsDTO> AddNewClient(clsClientsDTO ClientDTO)
        {
            if (ClientDTO.ClientID < 0 || ClientDTO.PersonID < 0 || ClientDTO.AccountNumber == string.Empty || ClientDTO.Balance == 0 || ClientDTO.PinCode == string.Empty || ClientDTO == null)
            {
                return BadRequest("Invalid Client Data ! ");
            }
            clsClients Client = new clsClients(ClientDTO);
            if (!Client.Save())
            {
                return BadRequest("Client failed to save successfully !");
            }
            return Ok(Client.ClientDTO);
        }

        [HttpPut("UpdateClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<clsClientsDTO> UpdateClient(clsClientsDTO ClientDTO)
        {
            if (ClientDTO.ClientID < 0 || ClientDTO.PersonID < 0 || ClientDTO.AccountNumber == string.Empty || ClientDTO.Balance == 0 || ClientDTO.PinCode == string.Empty || ClientDTO == null)
            {
                return BadRequest("Invalid Client Data ! ");
            }
            clsClients Client = clsClients.GetClientByID(ClientDTO.ClientID);
            if (Client == null)
            {
                return NotFound("No Client with this ID !");
            }
            Client.AccountNumber = ClientDTO.AccountNumber;
            Client.Balance = ClientDTO.Balance;
            Client.PinCode = ClientDTO.PinCode;
            Client.PersonID = ClientDTO.PersonID;
            if (!Client.Save())
            {
                return BadRequest("Client failed to update successfully !");
            }
            return Ok(Client.ClientDTO);
        }
    
    }
}
