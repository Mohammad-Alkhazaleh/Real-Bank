﻿using BankApiBussinessLayer;
using BankApiDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {

        [HttpGet("GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<clsUsersDTO>> GetAllUsers()
        {
           List<clsUsersDTO> ListUsers =  BankApiBussinessLayer.clsUsers.GetAllUsers();
            if (ListUsers.Count ==0)
            {
                return NotFound("No Students Found !");
            }
            return Ok(ListUsers);

        }
        [HttpGet("{UserID}",Name = "GetUserByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<clsUsersDTO> GetUserByID(int UserID)
        {
            if (UserID <0)
            {
                return BadRequest("Invalid UserID !");
            }
            clsUsers User = BankApiBussinessLayer.clsUsers.GetUserByID(UserID);
            if (User == null)
            {
                return BadRequest("No user with this ID !");
            }
            return Ok(User.UserDTO);
        }

        [HttpPost("AddNewUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<clsClientsDTO> AddNewClient(clsUsersDTO UserDTO)
        {
            if (UserDTO.UserName ==string.Empty|| UserDTO.Password == string.Empty || UserDTO.PersonID <0 || UserDTO.Permissions <-1 )
            {
                return BadRequest("Invalid Client Data ! ");
            }
            BankApiBussinessLayer.clsUsers User = new clsUsers(UserDTO,clsUsers.enMode.AddNew);
            if (!User.Save())
            {
                return BadRequest("User failed to save successfully !");
            }
            return Ok(User.UserDTO);


        }
    }

}
