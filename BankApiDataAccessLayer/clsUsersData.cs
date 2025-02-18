using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
        public class clsUsersData
        {
            public static List<clsUsersDTO> GetAllUsers()
            {
                List<clsUsersDTO> ListUsers = new List<clsUsersDTO>(); 
                using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
                {
                Connection.Open();
                    using (SqlCommand Command = new SqlCommand("[dbo].[GetAllUsers]",Connection))
                    {
                        using (SqlDataReader reader = Command.ExecuteReader())
                        {
                            while (reader.Read())
                                ListUsers.Add(new clsUsersDTO(

                                 reader.GetInt32(reader.GetOrdinal("UserID")),
                                reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")),
                                reader.GetInt32(reader.GetOrdinal("UserPermissions")),
                                reader.GetInt32(reader.GetOrdinal("PersonID"))
                                 ));
                        }
                        

                        }
                    }
                return ListUsers;
                }
        public static clsUsersDTO GetUserByID(int UserID)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                
                using (SqlCommand Command  = new SqlCommand("[dbo].FindUser",Connection) )
                {
                    
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);
                    Connection.Open();
                    using (SqlDataReader reader = Command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new clsUsersDTO(UserID, reader.GetString(reader.GetOrdinal("UserName")),
                                reader.GetString(reader.GetOrdinal("Password")) , reader.GetInt32(reader.GetOrdinal("UserPermissions")),
                                reader.GetInt32(reader.GetOrdinal("PersonID"))); 
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
        }
        public static int AddNewUser(clsUsersDTO UserDTO)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[AddNewUser]", Connection))
                {
                    
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserName", UserDTO.UserName);
                    Command.Parameters.AddWithValue("@Password", UserDTO.Password);
                    Command.Parameters.AddWithValue("@UserPermissions", UserDTO.Permissions);
                    Command.Parameters.AddWithValue("@PersonID", UserDTO.PersonID);
                    var OutPutIdParameter = new SqlParameter("@UserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutPutIdParameter);
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    return (int)OutPutIdParameter.Value;

                }
            }
        }
        public static  bool UpdateUser(clsUsersDTO UserDTO)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[UpdateUser]",Connection))
                {
                    Command.Parameters.AddWithValue("@UserName", UserDTO.UserName);
                    Command.Parameters.AddWithValue("@Password", UserDTO.Password);
                    Command.Parameters.AddWithValue("@UserPermissions", UserDTO.Permissions);
                    Command.Parameters.AddWithValue("@PersonID", UserDTO.PersonID);
                    Command.Parameters.AddWithValue("@UserID", UserDTO.UserID);
                    Command.CommandType = CommandType.StoredProcedure;
                    Connection.Open();
                    return Command.ExecuteNonQuery()>0;
                }
            }
        }
        public static int DeleteUser(int UserID)
        {
            using (SqlConnection Connection = new SqlConnection(clsConnectionString.ConnectionString))
            {
                using (SqlCommand Command = new SqlCommand("[dbo].[DeleteUser]", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@UserID", UserID);
                    var OutPutIdParameter = new SqlParameter("@RowsAffected", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    Command.Parameters.Add(OutPutIdParameter);
                    Connection.Open();
                    Command.ExecuteNonQuery();
                    return (int)OutPutIdParameter.Value;


                }
            }
        }
    }
}

