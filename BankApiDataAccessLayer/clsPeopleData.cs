using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq.Expressions;

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
    }
    public class clsPeopleData
    {
        public static int AddNewPerson(clsPeopleDTO Person ,int UserID)
        {
            int PersonID = -1;
            try
            {
                using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
                {
                    using (SqlCommand Command = new SqlCommand("[dbo].[AddNewPerson]", Connection))
                    {
                        Command.CommandType = CommandType.StoredProcedure;
                        Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = Person.FirstName;
                        Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = Person.LastName;
                        Command.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = Person.Email;
                        Command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10).Value = Person.Phone;
                        Command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                        var outPutIdParameter = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        Command.Parameters.Add(outPutIdParameter);

                        Connection.Open();
                        Command.ExecuteNonQuery();
                        PersonID = Convert.ToInt32(outPutIdParameter.Value);
                    }
                }

            }
            catch (SqlException EX)
            {
                Console.WriteLine(EX.Message);
                PersonID = -1;
            }
            return PersonID;
        }

        public static bool UpdatePerson(clsPeopleDTO PersonDTO , int UserID)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[UpdatePerson]",Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonDTO.PersonID;
                    Command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 100).Value = PersonDTO.FirstName;
                    Command.Parameters.Add("@LastName", SqlDbType.NVarChar, 100).Value = PersonDTO.LastName;
                    Command.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = PersonDTO.Email;
                    Command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10).Value = PersonDTO.Phone;
                    Command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    Connection.Open();
                    return Command.ExecuteNonQuery()>0;
                    
                }
            }
        }
        public static bool DeletePerson(int PersonID, int UserID)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[DeletePerson]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    Command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    Connection.Open();
                    return Command.ExecuteNonQuery() > 0;

                }
            }
        }
        public static clsPeopleDTO FindPerson(int PersonID, int UserID)
        {
            string Email = "";
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[FindPerson]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
                    Command.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read()) {
                           
                            return new clsPeopleDTO(reader.GetInt32(reader.GetOrdinal("PersonID")),
                                reader.GetString(reader.GetOrdinal("FirstName")),
                                reader.GetString(reader.GetOrdinal("LastName")),
                                ((reader["Email"] == DBNull.Value )?"": reader.GetString(reader.GetOrdinal("Email"))), reader.GetString(reader.GetOrdinal("PhoneNumber")));
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
        }
    }
}

