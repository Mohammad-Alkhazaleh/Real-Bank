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
        public enum enMode { AddNew, Update }
        public enMode _Mode;
        public int ClientID { set; get; }
        public string AccountNumber { set; get; }
        public decimal Balance { set; get; }
        public string PinCode  { set; get; }
        public int PersonID { set; get; }

        public clsClientsDTO ClientDTO { get { return new clsClientsDTO(this.ClientID, this.AccountNumber, this.Balance, this.PinCode, this.PersonID); } }

        public clsClients(clsClientsDTO cDto, enMode Mode = enMode.AddNew)
        {
            this.ClientID = cDto.ClientID;
            this.AccountNumber = cDto.AccountNumber;
            this.Balance = cDto.Balance;
            this.PinCode = cDto.PinCode;
            this.PersonID = cDto.PersonID;
            this._Mode = Mode;
        }

        public static List<clsClientsDTO> GetAllClients()
        {
            return clsClientsData.GetAllClients();
        }
        public static clsClients GetClientByID(int ClientID)
        {
            clsClientsDTO ClientDTO = new clsClientsDTO();
            if ((ClientDTO = clsClientsData.Find(ClientID)) != null)
            {
                return new clsClients(ClientDTO, enMode.Update);
            }
            else
            {
                return null;
            }   
        }
        private bool _AddNewClient()
        {
            ClientID = clsClientsData.AddNewClient(ClientDTO);
            return ClientID != -1;
        }
        private bool _UpdateClient()
        {
            return clsClientsData.UpdateClient(ClientDTO);
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewClient())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                case enMode.Update:
                    {
                        return _UpdateClient() ;
                    }
            }
            return false;
        }
    }
}
