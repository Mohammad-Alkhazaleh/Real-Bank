using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace BankApiDataAccessLayer
{
    public class clsClientsDTO
    {

        public int ClientID { set; get; }
        public string AccountNumber { set; get; }
        public decimal Balance { set; get; }
        public string PinCode { set; get; }
        public int PersonID { set; get; }


        public clsClientsDTO()
        {

            ClientID = -1;
            AccountNumber = "";
            Balance = 0;
            PinCode = "";
            PersonID = -1;
        }
        public clsClientsDTO(int ClientID, string AccountNumber, decimal Balance, string PinCode, int PersonID)
        {
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;
            this.Balance = Balance;
            this.PinCode = PinCode;
            this.PersonID = PersonID;
        } 
    }
        public class clsClientsData
        {
            public static List<clsClientsDTO> GetAllClients() { 

            var Clients = new List<clsClientsDTO>();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[GetAllClients]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        while (reader.Read())
                                Clients.Add(new clsClientsDTO(

                                 reader.GetInt32(reader.GetOrdinal("ClientID")),
                                reader.GetString(reader.GetOrdinal("AccountNumber")),
                                reader.GetDecimal(reader.GetOrdinal("Balance")),
                                reader.GetString(reader.GetOrdinal("PinCode")),
                                reader.GetInt32(reader.GetOrdinal("PersonID"))
                                 ));
                        }
                    }
                }
            
            return Clients;
        }
        }
 }