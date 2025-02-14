using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiBussinessLayer
{
    public class clsLogin
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int LoginID { set; get; }
        public int UserID{ set; get; }
        public DateTime LoginDate { set; get; }
       


        public clsLogin()
        {
            _Mode = enMode.AddNew;
            LoginID = -1;
            UserID = -1;
            LoginDate = DateTime.Now;
        }
        public clsLogin(int LoginID, int UserID, DateTime LoginDate)
        {
            _Mode = enMode.Update;
            this.LoginID = LoginID;
            this.UserID = UserID;
            this.LoginDate = LoginDate;
            
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
