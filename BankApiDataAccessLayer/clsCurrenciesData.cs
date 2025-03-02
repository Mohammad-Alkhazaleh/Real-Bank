using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
    }
        public class clsCurrenciesData
        {
            public static bool UpdateRate(clsCurrenciesDTO CurrencyDTO , int UserID)
            {
                using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("[dbo].[UpdateRate]", Connection))
                    {
                        
                        Command.Parameters.AddWithValue("@CountryCode", CurrencyDTO.CountryCode);
                        Command.Parameters.AddWithValue("@CountryName", CurrencyDTO.CountryName);
                        Command.Parameters.AddWithValue("@NewRate", CurrencyDTO.Rate);
                        Command.Parameters.AddWithValue("@UserID", UserID);

                        Command.CommandType = CommandType.StoredProcedure;
                        Connection.Open();
                        return Command.ExecuteNonQuery() > 0;
                    }
                }
            }
        public static decimal CurrencyCalculater(string CountryCode_NameFrom,
            string CountryCode_NameTo, decimal Amount , int UserID)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[CurrencyCalaculater]", Connection))
                {
                    Command.Parameters.AddWithValue("@CountryCode_NameFrom", CountryCode_NameFrom);
                    Command.Parameters.AddWithValue("@CountryCode_NameTo", CountryCode_NameTo);
                    Command.Parameters.AddWithValue("@Amount", Amount);
                    Command.Parameters.AddWithValue("@UserID", UserID);
                    var OutPutIdParameter = new SqlParameter("@Result", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output,
                        Precision = 18, // التأكد من تطابق الدقة مع قاعدة البيانات
                        Scale = 5
                    };
                    Command.Parameters.Add(OutPutIdParameter);
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    return OutPutIdParameter.Value != DBNull.Value ? Convert.ToDecimal(OutPutIdParameter.Value) : -4;
                }
            }
        }

        public static List<clsCurrenciesDTO> GetAllCurrencies()
        {
            List<clsCurrenciesDTO> Currencies = new List<clsCurrenciesDTO>();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[GetAllCurrencies]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Currencies.Add(new clsCurrenciesDTO(
                                    reader.GetInt32(reader.GetOrdinal("CurrencyID")),
                                    reader.GetString(reader.GetOrdinal("CountryName")),
                                    reader.GetString(reader.GetOrdinal("CountryCode")),
                                    reader.GetString(reader.GetOrdinal("CurrencyName")),
                                     reader.GetDecimal(reader.GetOrdinal("Rate"))));
                            }
                        }
                        return Currencies;
                    }
                }
            }
        }
             public static List<string> GetAllCountryCode_Name()
        {
            List<string> CountryCode_Name = new List<string>();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[GetAllCountryCode_Name]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CountryCode_Name.Add(reader.GetString(reader.GetOrdinal("CountryCode_Name")));
                            }
                        }
                        return CountryCode_Name;
                    }
                }
            }
        }
        public static List<string> GetAllCountryCode()
        {
            List<string> CountryCodes = new List<string>();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[GetAllCountryCodes]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CountryCodes.Add(reader.GetString(reader.GetOrdinal("CountryCode")));
                            }
                        }
                        return CountryCodes;
                    }
                }
            }
        }
        public static List<string> GetAllCountryNames()
        {
            List<string> CountryNames = new List<string>();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[GetAllCountryNames]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CountryNames.Add(reader.GetString(reader.GetOrdinal("CountryName")));
                            }
                        }
                        return CountryNames;
                    }
                }
            }
        }

         public static decimal GetRateByCountryName(string CountryName)
        {

            using (SqlConnection conn = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("[dbo].[GetRateByCountryName]", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CountryName", CountryName);

                    SqlParameter rateParam = new SqlParameter("@Rate", SqlDbType.Decimal);
                    rateParam.Precision = 18;
                    rateParam.Scale = 5;
                    rateParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(rateParam);

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    return (decimal)cmd.Parameters["@Rate"].Value;

                   
                }
            }
        }
    }
    }
