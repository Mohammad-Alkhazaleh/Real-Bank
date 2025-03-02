using BankApiDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiBussinessLayer
{
    public class clsCurrencies
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int CurrencyID { set; get; }
        public string CountryName { set; get; }
        public string CountryCode { set; get; }
        public string CurrencyName { set; get; }
        public decimal Rate { set; get; }


        public clsCurrencies()
        {
            _Mode = enMode.AddNew;
            CurrencyID = -1;
            CountryName = "";
            CountryCode = "";
            CurrencyName = "";
            Rate = 0;
        }
        public clsCurrencies(int CurrencyID, string CountryName, string CountryCode, string CurrencyName, decimal Rate)
        {
            _Mode = enMode.Update;
            this.CurrencyID = CurrencyID;
            this.CountryName = CountryName;
            this.CountryCode = CountryCode;
            this.CurrencyName = CurrencyName;
            this.Rate = Rate;
        }

        public static bool UpdateRate(clsCurrenciesDTO CurrencyDTO , int UserID)
        {
            return clsCurrenciesData.UpdateRate(CurrencyDTO , UserID);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        break;
                    }
                case enMode.Update:
                    {
                        break;
                    }
            }
            return false;
        }
        public static decimal CurrencyCalculater(string CountryCode_NameFrom,
           string CountryCode_NameTo, decimal Amount, int UserID)
        {
            return clsCurrenciesData.CurrencyCalculater(CountryCode_NameFrom, CountryCode_NameTo,
                Amount , UserID);
        }
        public static List<clsCurrenciesDTO> GetAllCurrencies()
        {
            return clsCurrenciesData.GetAllCurrencies();
        }
        public static List<string> GetAllCountryCode_Name()
        {
            return clsCurrenciesData.GetAllCountryCode_Name();
        }
        public static List<string> GetAllCountryCode()
        {
            return clsCurrenciesData.GetAllCountryCode();
        }
        public static List<string> GetAllCountryNames()
        {
            return clsCurrenciesData.GetAllCountryNames();
        }
        public static decimal GetRateByCountryName(string CountryName)
        {
            return clsCurrenciesData.GetRateByCountryName( CountryName);
        }
    }
}
