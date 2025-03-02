using BankApiDataAccessLayer;
using System.Globalization;

namespace BankApiBussinessLayer
{
    public class clsPeople
    {
        public enum enMode { AddNew, Update }
        public enMode _Mode;
        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public clsPeopleDTO PersonDTO { get{ return new clsPeopleDTO(PersonID,FirstName,LastName,Email,Phone); } }
        
        public clsPeople()
        {
            _Mode = enMode.AddNew;
            PersonID = -1;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
        }
        public clsPeople(clsPeopleDTO peopleDTO, enMode Mode =enMode.AddNew)
        {
            _Mode = Mode;
            this.PersonID = peopleDTO.PersonID;
            this.FirstName = peopleDTO.FirstName;
            this.LastName = peopleDTO.LastName;
            this.Email = peopleDTO.Email;
            this.Phone = peopleDTO.Phone;
        }
        private bool _AddNewPerson()
        {
            PersonID = clsPeopleData.AddNewPerson(PersonDTO, BankApiDataAccessLayer.clsGeneralLibrary.UserID);
            return PersonID != -1 ;
        }
        private bool _UpdatePerson()
        {
            return clsPeopleData.UpdatePerson(PersonDTO , clsGeneralLibrary.UserID);
        }
        public static bool DeletePerson(int PersonID , int UserID)
        {
            return clsPeopleData.DeletePerson(PersonID , UserID);
        }
        public static clsPeople FindPerson(int PersonID , int UserID)
        {
            clsPeopleDTO PersonDTO = new clsPeopleDTO();
            if((PersonDTO = clsPeopleData.FindPerson(PersonID, UserID))!=null)
            {
                return new clsPeople(PersonDTO,enMode.Update);
            }
            else
            {
                return null;
            }
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
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
                        return _UpdatePerson();
                    }
            }
            return false;
        }
    }
}
