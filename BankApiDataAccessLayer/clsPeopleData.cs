namespace BankApiDataAccessLayer
{
    public class clsPeopleDTO
    {
        private enum enMode { AddNew, Update }
        private enMode _Mode;
        public int PersonID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }

        public clsPeopleDTO()
        {
            _Mode = enMode.AddNew;
            PersonID = -1;
            FirstName = "";
            LastName = "";
            Email = "";
            Phone = "";
        }
        public clsPeopleDTO(int PersonID, string FirstName, string LastName, string Email, string Phone)
        {
            _Mode = enMode.Update;
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
        }
        public class clsPeopleData
        {


        }
    }
}
