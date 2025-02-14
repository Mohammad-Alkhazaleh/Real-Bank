using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiDataAccessLayer
{
    public class clsTransferLogDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int TransferLogID { set; get; }
        public int ClientID_Sender { set; get; }
        public int ClientID_Reciever { set; get; }
        public decimal TransferAmount { set; get; }

        public DateTime TransferDate { set; get; }
        public int UserID { set; get; }



        public clsTransferLogDTO()
        {
            _Mode = enMode.AddNew;
            TransferLogID = -1;
            ClientID_Sender = -1;
            ClientID_Reciever = -1;
            TransferAmount = 0;
            TransferDate = DateTime.Now;
            UserID = -1;
        }
        public clsTransferLogDTO(int TransferLogID, int ClientID_Sender, int ClientID_Reciever, decimal TransferAmount, DateTime TransferDate, int UserID)
        {
            _Mode = enMode.Update;
            this.TransferLogID = TransferLogID;
            this.ClientID_Sender = TransferLogID;
            this.ClientID_Reciever = ClientID_Reciever;
            this.TransferAmount = TransferAmount;
            this.TransferDate = TransferDate;
            this.UserID = UserID;

        }
        public class clsTransferLogData
        {

        }
    }
}
