using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiBussinessLayer
{
    public class clsClients
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int ClientID { set; get; }
        public string AccountNumber { set; get; }
        public decimal Balance { set; get; }
        public string PinCode  { set; get; }
        public int PersonID { set; get; }


        public clsClients()
        {
            _Mode = enMode.AddNew;
            ClientID = -1;
            AccountNumber = "";
            Balance = 0;
            PinCode = "";
            PersonID = -1;
        }
        public clsClients(int ClientID, string AccountNumber, decimal Balance, string PinCode ,int PersonID)
        {
            _Mode = enMode.Update;
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;
            this.Balance = Balance;
            this.PinCode = PinCode;
            this.PersonID = PersonID;
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
    }
}
