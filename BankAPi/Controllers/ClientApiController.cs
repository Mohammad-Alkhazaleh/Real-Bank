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
    }
}
