using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiDataAccessLayer
{
    public class clsCurrenciesDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int CurrencyID { set; get; }
        public string CountryName { set; get; }
        public string CountryCode { set; get; }
        public string CurrencyName { set; get; }
        public decimal Rate { set; get; }


        public clsCurrenciesDTO()
        {
            _Mode = enMode.AddNew;
            CurrencyID = -1;
            CountryName = "";
            CountryCode = "";
            CurrencyName = "";
            Rate = 0;
        }
        public clsCurrenciesDTO(int CurrencyID, string CountryName, string CountryCode, string CurrencyName, decimal Rate)
        {
            _Mode = enMode.Update;
            this.CurrencyID = CurrencyID;
            this.CountryName = CountryName;
            this.CountryCode = CountryCode;
            this.CurrencyName = CurrencyName;
            this.Rate = Rate;
        }
        public class clsCurrenciesData
        {

        }
    }
}