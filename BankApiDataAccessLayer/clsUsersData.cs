using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiDataAccessLayer
{
    public class clsUsersDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int Permissions { set; get; }
        public int PersonID { set; get; }


        public clsUsersDTO()
        {
            _Mode = enMode.AddNew;
            UserID = -1;
            UserName = "";
            Password = "";
            Permissions = 0;
            PersonID = -1;
        }
        public clsUsersDTO(int UserID, string UserName, string Password, int Permissions, int PersonID)
        {
            _Mode = enMode.Update;
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.Permissions = Permissions;
            this.PersonID = PersonID;
        }
        public class clsUsersData
        {

        }
    }
}
