using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiDataAccessLayer
{
    public class clsBalancesDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public string AccountNumber { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public decimal Balance { set; get; }

          public clsBalancesDTO()
        {
            _Mode = enMode.AddNew;
            AccountNumber = "";
            FirstName = "";
            LastName = "";
            Balance = 0;
        }
        public clsBalancesDTO(string accountNumber, string firstName, string lastName, decimal balance)
        {
            _Mode = enMode.Update;
            AccountNumber = accountNumber;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
        }
    }

    public class clsTransferDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public string AccountNumber_Sender { set; get; }
        public string AccountNumber_Receiver { set; get; }
        public decimal Amount { set; get; }
        public int UserID { set; get; }

        public clsTransferDTO()
        {
            _Mode = enMode.AddNew;
            AccountNumber_Sender = "";
            AccountNumber_Receiver = "";
            Amount = 0;
            UserID = -1;
        }
        public clsTransferDTO(string accountNumber_Sender, string accountNumber_Receiver, decimal amount, int userID)
        {
            _Mode = enMode.Update;
            AccountNumber_Sender = accountNumber_Sender;
            AccountNumber_Receiver = accountNumber_Receiver;
            Amount = amount;
            UserID = userID;
        }
    }
    public class clsTransferLogsDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;

        public DateTime TransferDate { set; get; }
        public string   AccountNumber_Sender { set; get; }
        public string   AccountNumber_Receiver { set; get; }
        public decimal  TransferAmount { set; get; }
        public decimal  Balance_Sender { set; get; }
        public decimal  Balance_Receiver { set; get; }
        public string   UserName { set; get; }

        public clsTransferLogsDTO()
        {
            _Mode = enMode.AddNew;
            TransferDate = DateTime.Now;
            AccountNumber_Sender = "";
            AccountNumber_Receiver = "";
            TransferAmount = 0;
            Balance_Sender = 0;
            Balance_Receiver = 0;
            UserName = "";
        }
        public clsTransferLogsDTO(DateTime transferDate, string accountNumber_Sender, string accountNumber_Receiver, decimal amount,  decimal Balance_Sender, decimal Balance_Receiver, string userName)
        {
            _Mode = enMode.Update;
            TransferDate = transferDate;
            AccountNumber_Sender = accountNumber_Sender;
            AccountNumber_Receiver = accountNumber_Receiver;
            TransferAmount = amount;
            this.Balance_Sender = Balance_Sender;
            this.Balance_Receiver = Balance_Receiver;
            UserName = userName;
        }
    }
    public class clsTransactionsData
        {
            public static bool Transactions(string ClientAccountNumber, decimal Amount)
            {
                using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("[dbo].[Transactions]",Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.AddWithValue("@AccountNumber", ClientAccountNumber);
                        Command.Parameters.AddWithValue("@Amount", Amount);
                        Connection.Open();
                    try
                    {
                        return Command.ExecuteNonQuery() > 0;
                    }
                    catch (SqlException ex)
                    { 
                            return false;
                    }
                    }
                }
            }
        public static List<clsBalancesDTO> GetAllBalances()
        {
            List<clsBalancesDTO>TotalBalances = new List<clsBalancesDTO>();
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[TotalBalances]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                   
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TotalBalances.Add(new clsBalancesDTO(reader.GetString(reader.GetOrdinal("AccountNumber")),
                                    reader.GetString(reader.GetOrdinal("FirstName")) , reader.GetString(reader.GetOrdinal("LastName")) ,
                                    reader.GetDecimal(reader.GetOrdinal("Balance"))));
                            }
                            return TotalBalances;
                        }
                        else
                        {
                            return TotalBalances;
                        }
                    }
                }
            }
        }
        public static int TransferOperation(clsTransferDTO TransferDTO)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[TransferOperation]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@AccountNumber1", TransferDTO.AccountNumber_Sender);
                    Command.Parameters.AddWithValue("@AccountNumber2", TransferDTO.AccountNumber_Receiver);
                    Command.Parameters.AddWithValue("@Amount", TransferDTO.Amount);
                    Command.Parameters.AddWithValue("@UserID", TransferDTO.UserID);
                    Connection.Open();
                   try
                    {
                        int result = Command.ExecuteNonQuery();
                            return result;
                       
                    }
                    catch (Exception ex)
                    {
                        // Log the error for further investigation
                        Console.WriteLine($"Error: {ex.Message}");
                        return -5;
                    }
                }
            }
        }

        public static List<clsTransferLogsDTO> GetAllTransferLogs()
        {
            List<clsTransferLogsDTO> Logs = new List<clsTransferLogsDTO>();

            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {

                using (SqlCommand Command = new SqlCommand("[dbo].[GetAllTransferLogs]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();

                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Logs.Add(new clsTransferLogsDTO(reader.GetDateTime(reader.GetOrdinal("TransferDate")),
                            reader.GetString(reader.GetOrdinal("AccountNumber_Sender")),
                            reader.GetString(reader.GetOrdinal("AccountNumber_Receiver")),
                            reader.GetDecimal(reader.GetOrdinal("TransferAmount")),
                            reader.GetDecimal(reader.GetOrdinal("Balance_Sender")),
                            reader.GetDecimal(reader.GetOrdinal("Balance_Receiver")),
                            reader.GetString(reader.GetOrdinal("UserName"))
                            )); 
                        }
                        return Logs;
                    }
                }
            }
        }
    }
}
       

