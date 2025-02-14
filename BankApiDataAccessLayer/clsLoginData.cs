using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiDataAccessLayer
{
    public class clsLoginDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int LoginID { set; get; }
        public int UserID { set; get; }
        public DateTime LoginDate { set; get; }



        public clsLoginDTO()
        {
            _Mode = enMode.AddNew;
            LoginID = -1;
            UserID = -1;
            LoginDate = DateTime.Now;
        }
        public clsLoginDTO(int LoginID, int UserID, DateTime LoginDate)
        {
            _Mode = enMode.Update;
            this.LoginID = LoginID;
            this.UserID = UserID;
            this.LoginDate = LoginDate;

        }
        public class clsLoginData
        {

        }
    }
}