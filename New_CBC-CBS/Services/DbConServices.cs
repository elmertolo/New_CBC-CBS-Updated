﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using New_CBC_CBS.Models;

namespace New_CBC_CBS.Services
{
    class DbConServices
    {
        public MySqlConnection myConnect;
       // private int serial = 1;
        public string databaseName = "";
        public string localhost = "";
        public void DBConnect()
        {
            try
            {
                string DBConnection = "";

                //                if (frmLogIn._userName == "test")
                //              {
                DBConnection = "datasource=localhost;port=3306;username=root;password=corpcaptive; convert zero datetime=True;";

                databaseName = "captive_database";
                //MessageBox.Show(databaseName);
                //            }
                //else
                //{
                //    //  DBConnection = "";
                //    DBConnection = "datasource=192.168.0.254;port=3306;username=root;password=CorpCaptive; convert zero datetime=True;";
                //    // MessageBox.Show("HELLO");
                //    databaseName = "captive_database";
                //    // MessageBox.Show(databaseName);

                //}


                myConnect = new MySqlConnection(DBConnection);

                myConnect.Open();

            }
            catch (Exception Error)
            {

                MessageBox.Show(Error.Message, "System Error");
            }
        }// end of function

        public void DBClosed()
        {
            myConnect.Close();
        }
        // end of function
        public List<BranchModel> GetAllBranches(List<BranchModel> _branches)
        {
            DBConnect();
            string sql = "Select * from " + databaseName + "."+Form1.banks+"_branches";
            //List<BranchModel> Branches = new List<BranchModel>();

            MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            while (myReader.Read())
            {
                BranchModel branch = new BranchModel();

                branch.BRSTN = myReader.GetString(0);


                branch.Address1 = !myReader.IsDBNull(1) ? myReader.GetString(1) : "";

                branch.Address2 = !myReader.IsDBNull(2) ? myReader.GetString(2) : "";

                branch.Address3 = !myReader.IsDBNull(3) ? myReader.GetString(3) : "";

                branch.Address4 = !myReader.IsDBNull(4) ? myReader.GetString(4) : "";

                branch.Address5 = !myReader.IsDBNull(5) ? myReader.GetString(5) : "";
                branch.Address6 = !myReader.IsDBNull(6) ? myReader.GetString(6) : "";
                //branch.Company = !myReader.IsDBNull(7) ? myReader.GetString(7) : "";
                //branch.BranchCode = !myReader.IsDBNull(8) ? myReader.GetString(8) : "";
                //branch.BaeStock = !myReader.IsDBNull(9) ? myReader.GetString(9) : "";
                branch.Reg_LastNo = !myReader.IsDBNull(7) ? myReader.GetInt64(7) : 0;
                branch.DBCA_LastNo = !myReader.IsDBNull(8) ? myReader.GetInt64(8) : 0;
                branch.ModifiedDate = !myReader.IsDBNull(9) ? myReader.GetDateTime(9) : DateTime.Today;
                //  branch.Binan_LastNo = !myReader.IsDBNull(11) ? myReader.GetInt64(11) : 0;


                _branches.Add(branch);
            }//END OF WHILE
            DBClosed();

            return _branches;

        }
        public OrderModel SavedDatatoDatabase(OrderModel _check,string _batch)
        {
            if (_check.AccountNo == null)
            {

            }
            else
            {
                string sql = "INSERT INTO captive_database."+Form1.banks+"_history(Date,Time,DeliveryDate,ChkType,ChequeName,BRSTN,AccountNo,Name1,Name2,Address1,Address2,Address3,Address4,Address5,Address6,Batch,StartingSerial, EndingSerial)VALUES(" +

                            "'" + DateTime.Now.ToString("yyyy-MM-dd") + "'," +
                            "'" + DateTime.Now.ToString("HH:mm:ss") + "'," +
                            "'" + _check.deliveryDate.ToString("yyyy-MM-dd") + "'," +
                            "'" + _check.ChkType + "'," +
                            "'" + _check.ChkName + "'," +
                            "'" + _check.BRSTN + "'," +
                            "'" + _check.AccountNo + "'," +
                            "'" + _check.AccountName.Replace("'", "''") + "'," +
                            "'" + _check.AccountName2.Replace("'", "''") + "'," +
                            "'" + _check.BranchName.Replace("'", "''") + "'," +
                           // "'" + _check.BranchCode + "'," +
                            "'" + _check.Address2.Replace("'", "''") + "'," +
                            "'" + _check.Address3.Replace("'", "''") + "'," +
                            "'" + _check.Address4.Replace("'", "''") + "'," +
                            "'" + _check.Address5.Replace("'", "''") + "'," +
                            "'" + _check.Address6.Replace("'", "''") + "'," +
                            "'" + _batch + "'," +
                            "'" + _check.StartingSerial + "'," +
                            "'" + _check.EndingSerial + "')";



                DBConnect();
                MySqlCommand myCommand = new MySqlCommand(sql, myConnect);

                myCommand.ExecuteNonQuery();
                DBClosed();

            }
            return _check;
        }// end of function

        public List<MySqlLocatorModel> GetMySQLLocations()
        {
            MySqlConnection connect = new MySqlConnection("datasource=192.168.0.254 ;port=3306;username=root;password=CorpCaptive");

            connect.Open();

            MySqlCommand myCommand = new MySqlCommand("SELECT * FROM captive_database.mysqldump_location", connect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            List<MySqlLocatorModel> sqlLocator = new List<MySqlLocatorModel>();

            while (myReader.Read())
            {
                MySqlLocatorModel myLocator = new MySqlLocatorModel
                {
                    PrimaryKey = myReader.GetInt32(0),
                    Location = myReader.GetString(1)
                };

                sqlLocator.Add(myLocator);
            }

            connect.Close();

            return sqlLocator;
        }//end of Function
        public void DumpMySQL()
        {
            string dbname = Form1.banks +"_branches";
            string outputFolder =Application.StartupPath +"\\"+Form1.banks+ @"\Output\"+ CBS.outputFolder;
            Process proc = new Process();

            proc.StartInfo.FileName = "cmd.exe";

            proc.StartInfo.UseShellExecute = false;

            proc.StartInfo.WorkingDirectory = GetMySqlPath().ToUpper().Replace("MYSQLDUMP.EXE", "");

            proc.StartInfo.RedirectStandardInput = true;

            proc.StartInfo.RedirectStandardOutput = true;

            proc.Start();

            StreamWriter myStreamWriter = proc.StandardInput;

            string temp = "mysqldump.exe --user=root --password=corpcaptive --host=localhost captive_database " + dbname + " > " +
                outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            dbname = Form1.banks + "_history";

            temp = "mysqldump.exe --user=root --password=password=corpcaptive --host=localhost captive_database " + dbname + " > " +
                 outputFolder + "\\" + DateTime.Today.ToShortDateString().Replace("/", ".") + "-" + dbname + ".SQL";

            myStreamWriter.WriteLine(temp);

            myStreamWriter.Close();

            proc.WaitForExit();

            proc.Close();
        }//end of Function
        public string GetMySqlPath()
        {
            var mySQLocator = GetMySQLLocations();

            foreach (var loc in mySQLocator)
            {
                if (File.Exists(loc.Location))
                    return loc.Location;
            }

            return "";
        } //end of Function
        public List<OrderModel> GetBatchFile(List<OrderModel> check) //Checking if the batch is already exists
        {
            DBConnect();

            MySqlCommand myCommand = new MySqlCommand("SELECT Distinct Batch FROM " + databaseName + "." + Form1.banks + "_history ", myConnect);

            MySqlDataReader myReader = myCommand.ExecuteReader();

            // List<CheckModel> check = new List<CheckModel>();

            while (myReader.Read())
            {
                OrderModel order = new OrderModel
                {

                    Batchfile = myReader.GetString(0)
                };

                check.Add(order);
            }

            DBClosed();

            return check;
        }
    }
}
