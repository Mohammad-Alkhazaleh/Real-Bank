using BankApiDataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BankAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesApiController : ControllerBase
    {
        [HttpPut("UpdateCurrencyRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<object> UpdateCurrencyRate(clsCurrenciesDTO CurrencyDTO, int UserID)
        {
            if (CurrencyDTO.CountryCode == string.Empty || CurrencyDTO.CountryName == string.Empty || CurrencyDTO.Rate < 0)
            {
                return BadRequest(new { success = false, message = "Invalied Data !" });
            }
            if (!BankApiBussinessLayer.clsCurrencies.UpdateRate(CurrencyDTO, UserID))
            {
                return BadRequest(new { success = false, message = "Rate failed to update successfully!" });
            }
            else
            {
                return Ok(new { success = true, message = "Rate updated successfully!" });
            }
        }


        [HttpGet( ("{CountryCode_NameFrom}/{CountryCode_NameTo}/{Amount}/{UserID}"),Name = "CurrencyCalculater")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<decimal> CurrencyCalculater(string CountryCode_NameFrom, string CountryCode_NameTo, 
           decimal Amount ,int UserID)
        {
            if (CountryCode_NameFrom== string.Empty || CountryCode_NameTo== string.Empty || Amount< 0|| UserID<0)
            {
                return BadRequest( "Invalied Data !" );
            }
           decimal Result =  BankApiBussinessLayer.clsCurrencies.CurrencyCalculater(CountryCode_NameFrom, CountryCode_NameTo,
                Amount ,UserID);
            if (Result ==-1)
            {
                return NotFound("CountryCode_NameFrom is not exist");
            }
            else if (Result == -2)
            {
                return NotFound("CountryCode_NameTo is not exist");
            }
            else if (Result ==-3)
            {
                return BadRequest("Amount less than 0 !");
            }
            else if (Result == -4)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok(Result);
        }
        [HttpGet("GetAllCurrencies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult<List<clsCurrenciesDTO>>GetAllCurrencies()
        {
           List<clsCurrenciesDTO>Currencies =  BankApiBussinessLayer.clsCurrencies.GetAllCurrencies();
            if (Currencies.Count == 0)
            {
                return NotFound("No Currencies !");
            }
            return Ok(Currencies);
        }
        [HttpGet("GetAllCountryCode_Name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<clsCurrenciesDTO>> GetAllCountryCode_Name()
        {
            List<string> CountryCode_Names = BankApiBussinessLayer.clsCurrencies.GetAllCountryCode_Name();
            if (CountryCode_Names.Count == 0)
            {
                return NotFound("No CountryCode_Name !");
            }
            return Ok(CountryCode_Names);
        }

        [HttpGet("GetAllCountryCodes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<clsCurrenciesDTO>> GetAllCountryCodes()
        {
            List<string> CountryCodes = BankApiBussinessLayer.clsCurrencies.GetAllCountryCode();
            if (CountryCodes.Count == 0)
            {
                return NotFound("No CountryCodes !");
            }
            return Ok(CountryCodes);
        }
        [HttpGet("GetAllCountryNames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<clsCurrenciesDTO>> GetAllCountryNames()
        {
            List<string> CountryNames = BankApiBussinessLayer.clsCurrencies.GetAllCountryNames();
            if (CountryNames.Count == 0)
            {
                return NotFound("No CountryNames !");
            }
            return Ok(CountryNames);
        }

        [HttpGet("{CountryName}",Name = "GetCurrencyRateByCountryName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<decimal> GetCurrencyRateByCountryName(string CountryName)
        {
            decimal Rate = 0;
            if ((Rate = BankApiBussinessLayer.clsCurrencies.GetRateByCountryName(CountryName))==-1)
            {
                return NotFound("No Country with this name !");
            }
            return Ok(Rate);
        }
    }
}
