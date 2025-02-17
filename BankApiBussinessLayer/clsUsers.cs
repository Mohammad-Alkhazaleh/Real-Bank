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
        public enum enMode { AddNew, Update }
        public enMode _Mode;
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
        public clsUsers( clsUsersDTO UserDTO, enMode Mode = enMode.AddNew)
        {
            _Mode = Mode;
            this.UserID = UserDTO.UserID;
            this.UserName = UserDTO.UserName;
            this.Password = UserDTO.Password;
            this.Permissions = UserDTO.Permissions;
            this.PersonID = UserDTO.PersonID;
        }
        public static List<clsUsersDTO> GetAllUsers()
        {
            return clsUsersData.GetAllUsers();
        }

        private bool _AddNewUser()
        {
            UserID = clsUsersData.AddNewUser(UserDTO);
            return UserID != -1;
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewUser())
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
                        break;
                    }
            }
            return false;
        }
        public static clsUsers GetUserByID(int UserID)
        {
            clsUsersDTO UserDTO = new clsUsersDTO();
           if((UserDTO = clsUsersData.GetUserByID(UserID) )!= null)
            {
                return new clsUsers(UserDTO,enMode.Update);
            }
            else
            {
                return null;
            }
        }
    }
}
