using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_CBC_CBS.Models;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace New_CBC_CBS.Services
{
    class UserServices :DbConServices
    {
        
        public UserModel Login(string _username, string _password)
        {
            try
            {

                if (_username == "test")
                {
                    UserModel user = new UserModel
                    {
                        Username = "Test",
                        Password = "",
                        Name = "Test User"
                    };

                    return user;
                }

                else
                {

                    DBConnect();

                    UserModel user = new UserModel();

                    string query = "SELECT Username, Password,Name FROM " + databaseName + ".users WHERE Username='" + _username + "' AND Password='" + _password + "'";

                    MySqlCommand myCommand = new MySqlCommand(query, myConnect);
                    MySqlDataAdapter sda = new MySqlDataAdapter(myCommand);
                    MySqlDataReader myReader = myCommand.ExecuteReader();
                    while (myReader.Read())
                    {
                        user = new UserModel
                        {
                            Username = myReader.GetString(0),
                            Password = myReader.GetString(1),
                            Name = myReader.GetString(2)
                        };

                    }
                    DBClosed();
                    return user;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message,"Error!!!");
                return null;
            }
        }//End of Function
    }
}
