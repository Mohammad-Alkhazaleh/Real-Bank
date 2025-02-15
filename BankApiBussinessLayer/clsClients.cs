using BankApiDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
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

        public clsClientsDTO ClientDTO { get { return new clsClientsDTO(this.ClientID, this.AccountNumber, this.Balance, this.PinCode, this.PersonID); } }

        public clsClients()
        {
            _Mode = enMode.AddNew;
            ClientID = -1;
            AccountNumber = "";
            Balance = 0;
            PinCode = "";
            PersonID = -1;
        }
        public clsClients(int ClientID, string AccountNumber, decimal Balance, string PinCode, int PersonID)
        {
            _Mode = enMode.Update;
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;
            this.Balance = Balance;
            this.PinCode = PinCode;
            this.PersonID = PersonID;
        }

        public static List<clsClientsDTO> GetAllClients()
        {
            return clsClientsData.GetAllClients();
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
