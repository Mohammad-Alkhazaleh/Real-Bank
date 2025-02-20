using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiDataAccessLayer
{
    public class clsTransactionsDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int TransferLogID { set; get; }
        public int ClientID_Sender { set; get; }
        public int ClientID_Reciever { set; get; }
        public decimal TransferAmount { set; get; }

        public DateTime TransferDate { set; get; }
        public int UserID { set; get; }



        public clsTransactionsDTO()
        {
            _Mode = enMode.AddNew;
            TransferLogID = -1;
            ClientID_Sender = -1;
            ClientID_Reciever = -1;
            TransferAmount = 0;
            TransferDate = DateTime.Now;
            UserID = -1;
        }
        public clsTransactionsDTO(int TransferLogID, int ClientID_Sender, int ClientID_Reciever, decimal TransferAmount, DateTime TransferDate, int UserID)
        {
            _Mode = enMode.Update;
            this.TransferLogID = TransferLogID;
            this.ClientID_Sender = TransferLogID;
            this.ClientID_Reciever = ClientID_Reciever;
            this.TransferAmount = TransferAmount;
            this.TransferDate = TransferDate;
            this.UserID = UserID;

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
      
        
    }
    
}
