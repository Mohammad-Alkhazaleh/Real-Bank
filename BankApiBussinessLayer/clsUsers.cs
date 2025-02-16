using BankApiDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApiBussinessLayer
{
    public class clsUsers
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public int Permissions { set; get; }
        public int PersonID { set; get; }
        public clsUsersDTO UserDTO { get { return new clsUsersDTO(UserID, UserName, Password, Permissions, PersonID); } }

        public clsUsers()
        {
            _Mode = enMode.AddNew;
            UserID = -1;
            UserName = "";
            Password = "";
            Permissions = 0;
            PersonID = -1;
        }
        public clsUsers(int UserID, string UserName, string Password, int Permissions, int PersonID)
        {
            _Mode = enMode.Update;
            this.UserID = UserID;
            this.UserName = UserName;
            this.Password = Password;
            this.Permissions = Permissions;
            this.PersonID = PersonID;
        }
        public static List<clsUsersDTO> GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
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
