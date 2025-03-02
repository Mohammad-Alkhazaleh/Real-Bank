using BankApiDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApiBussinessLayer;
namespace BankAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleApiController : ControllerBase
    {
        [HttpPost("AddNewPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<clsPeopleDTO> AddNewPerson(clsPeopleDTO PersonDTO ,int UserID)
        {
            if (PersonDTO.FirstName ==string.Empty || PersonDTO.LastName== string.Empty 
                || PersonDTO.Phone== string.Empty  || UserID < 0)
            {
                return BadRequest("Invalid data");
            }
            BankApiDataAccessLayer.clsGeneralLibrary.UserID = UserID;
            clsPeople person = new clsPeople(PersonDTO,clsPeople.enMode.AddNew);
            if(!person.Save())
            {
                return BadRequest("Person failed to save successfully...");
            }
            return Ok(person.PersonDTO);
        }

        [HttpPut("UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<clsPeopleDTO> UpdatePerson(clsPeopleDTO PersonDTO, int UserID)
        {
            if ( PersonDTO.PersonID<0 || PersonDTO.FirstName == string.Empty || PersonDTO.LastName == string.Empty
                || PersonDTO.Phone == string.Empty ||  UserID <0)
            {
                return BadRequest("Invalid data");
            }
            BankApiDataAccessLayer.clsGeneralLibrary.UserID = UserID;
            clsPeople person = new clsPeople(PersonDTO, clsPeople.enMode.Update);
            if (!person.Save())
            {
                return BadRequest("Person failed to update successfully...");
            }
            return Ok(person.PersonDTO);
        }
        [HttpDelete("{PersonID}/{UserID}",Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> DeletePerson(int PersonID , int UserID)
        {
            if (PersonID < 0 || UserID<0)
            {
                return BadRequest(new { success = false, message = "Invalid data" });
            }
            BankApiDataAccessLayer.clsGeneralLibrary.UserID = UserID;
           
            if (!BankApiBussinessLayer.clsPeople.DeletePerson(PersonID , UserID))
            {
                return BadRequest(new { success = false, message = "Person failed to delete successfully..." });
            }
            return Ok(new { success = true, message = "Person deleted successfully..." });
        }
        [HttpGet("{PersonID}/{UserID}", Name = "FindPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<clsPeopleDTO> FindPerson(int PersonID, int UserID)
        {
            if (PersonID < 0 || UserID < 0)
            {
                return BadRequest( "Invalid data" );
            }
            BankApiDataAccessLayer.clsGeneralLibrary.UserID = UserID;
            clsPeople Person = new clsPeople();
            if ((Person = BankApiBussinessLayer.clsPeople.FindPerson(PersonID,UserID))==null)
            {
                return BadRequest("No Person with this ID");
            }
            return Ok(Person.PersonDTO);
        }
    }
}

