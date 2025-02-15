using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        public static clsClientsDTO Find(int ClientID)
        {
            clsClientsDTO ClientDTO = new clsClientsDTO();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[FindClient]",Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@ClientID",ClientID);
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ClientDTO = new clsClientsDTO
                           (
                           ClientID,
                           reader.GetString(reader.GetOrdinal("AccountNumber")),
                           reader.GetDecimal(reader.GetOrdinal("Balance")),
                           reader.GetString(reader.GetOrdinal("PinCode")),
                           reader.GetInt32(reader.GetOrdinal("PersonID"))
                           );
                        }
                       
                    }
                         
                }
            }
            return ClientDTO;
        }
        public static int AddNewClient(clsClientsDTO ClientDTO)
        {
            
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[AddNewClient]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                   
                    Command.Parameters.AddWithValue("@AccountNumber", ClientDTO.AccountNumber);
                    Command.Parameters.AddWithValue("@Balance", ClientDTO.Balance);
                    Command.Parameters.AddWithValue("@PinCode", ClientDTO.PinCode);
                    Command.Parameters.AddWithValue("@PersonID", ClientDTO.PersonID);
                    var OutPutIdParameter = new SqlParameter("@NewClientID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutPutIdParameter);
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    return (int)OutPutIdParameter.Value;

                }
            }

            }

        public static  bool UpdateClient(clsClientsDTO ClientDTO)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[UpdateClient]",Connection))
                {
                    Command.Parameters.AddWithValue("@ClientID", ClientDTO.ClientID);
                    Command.Parameters.AddWithValue("@AccountNumber", ClientDTO.AccountNumber);
                    Command.Parameters.AddWithValue("@Balance", ClientDTO.Balance);
                    Command.Parameters.AddWithValue("@PinCode", ClientDTO.PinCode);
                    Command.Parameters.AddWithValue("@PersonID", ClientDTO.PersonID);
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                   return ( Command.ExecuteNonQuery()>0);
                }
            }

        }
    }
 }