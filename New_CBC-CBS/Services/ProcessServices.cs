using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using New_CBC_CBS.Models;

namespace New_CBC_CBS.Services
{
    class ProcessServices
    {
       private string bank = Form1.banks;
        DbConServices con = new DbConServices();
        static List<BranchModel> branch = new List<BranchModel>();
        StreamWriter file;
        
        //string folderName = "";
        public void DoBlockProcess(List<OrderModel> _checks, CBS _mainForm)
        {
            string outputFolder = Application.StartupPath + "\\" + Form1.banks + "\\Output";
            var listofcheck = _checks.Select(r => r.ChkType).ToList();
            foreach (string Scheck in listofcheck)
            {

                if (Scheck == "A")
                {

                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = DataOutputServices.ConvertToBlockText(checks, "PERSONAL", _mainForm.batchfile, _mainForm.deliveryDate, LogIn.userName);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {

                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\BlockC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = DataOutputServices.ConvertToBlockText(checks, "COMMERCIAL", _mainForm.batchfile, _mainForm.deliveryDate, LogIn.userName);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {

                if (Scheck == "CW")
                {
                    //_outpuFolder = "Check Write";
                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = DataOutputServices.ConvertToBlockText(checks, "Check Write", _mainForm.batchfile, _mainForm.deliveryDate, LogIn.userName);

                        file.WriteLine(output);
                    }

                }

            }
            foreach (string Scheck in listofcheck)
            {

                if (Scheck == "CV")
                {
                //    _outpuFolder = "Check with Voucher";
                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\BlockP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checks.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {

                        string output = DataOutputServices.ConvertToBlockText(checks, "Check With Voucher", _mainForm.batchfile, _mainForm.deliveryDate, LogIn.userName);

                        file.WriteLine(output);
                    }

                }
            }

        }
        public void PackingText(List<OrderModel> _checksModel, CBS _mainForm)
        {

            StreamWriter file;
            DbConServices db = new DbConServices();
            //  db.GetAllData(_checksModel, _mainForm._batchfile);
            var listofcheck = _checksModel.Select(e => e.ChkType).ToList();
            string outputFolder = Application.StartupPath + "\\" + Form1.banks + "\\Output";
            foreach (string Scheck in listofcheck)
            {
                
                if (Scheck == "A")
                {

                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\PackingP.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPackingList(checks, "PERSONAL", _mainForm);

                        file.WriteLine(output);
                    }

                }

            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "B")
                {

                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\PackingC.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPackingList(checks, "COMMERCIAL", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "CW")
                {
                    //_outputFolder = "Check Write";
                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\PackingA.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPackingList(checks, "Check Write", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
            foreach (string Scheck in listofcheck)
            {
                if (Scheck == "CV")
                {
                    //_outputFolder = "Check with Voucher";
                    string packkingListPath = outputFolder + "\\" + CBS.outputFolder + "\\PackingB.txt";
                    if (File.Exists(packkingListPath))
                        File.Delete(packkingListPath);
                    var checks = _checksModel.Where(a => a.ChkType == Scheck).Distinct().ToList();
                    file = File.CreateText(packkingListPath);
                    file.Close();

                    using (file = new StreamWriter(File.Open(packkingListPath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPackingList(checks, "Check with Voucher", _mainForm);

                        file.WriteLine(output);
                    }

                }
            }
        }
        public void SaveToPackingDBF(List<OrderModel> _checks, string _batchNumber, CBS _mainForm)
        {
            string dbConnection;
            string tempCheckType = "";
            int blockNo = 0, blockCounter = 0;
            DbConServices db = new DbConServices();
            //   db.GetAllData(_checks, _mainForm._batchfile);
          //  OleDbConnection Connect;
            var listofchecks = _checks.Select(e => e.ChkType).Distinct().ToList();
            //for (int i = 0; i < listofchecks.Count; i++)
            //{
            //    if(_checks[i].BRSTN == null)
            //    {
            //        i++;
            //    }
            //    else
            //    {
            //for (int i = 0; i < _checks.Count; i++)
            //{
            //    Connect = new OleDbConnection(dbConnection);
            //    if (_checks[i].ChkType == "CV")
            //    {
            //        _outputFolder = "Check with Voucher";
            //        dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath +"\\" +Form1.banks+ "\\Output\\" + _outputFolder + "\\Packing.dbf" + "; Mode=ReadWrite;";

            //        //Check if packing file exists
            //        //if (!File.Exists(_filepath))
            //        //{
                    
            //        OleDbCommand oCommand;
            //        Connect.Open();
            //        oCommand = new OleDbCommand("DELETE FROM PACKING", Connect);
            //        oCommand.ExecuteNonQuery();
            //        if (tempCheckType != _checks[i].ChkType)
            //            blockNo = 1;

            //        tempCheckType = _checks[i].ChkType;

            //        if (blockCounter < 4)
            //            blockCounter++;
            //        else
            //        {
            //            blockCounter = 1;
            //            blockNo++;
            //        }

            //        string sql = "INSERT INTO PACKING (BATCHNO,BLOCK, RT_NO,BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1,ACCT_NAME2," +
            //         "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, ADDRESS1, ADDRESS2,ADDRESS3,ADDRESS4, ADDRESS5, ADDRESS6 ) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + _checks[i].BRSTN + "','" + _checks[i].BranchName +
            //         "','" + _checks[i].AccountNo + "','" + _checks[i].AccountNo + "','" + _checks[i].ChkType + "','" + _checks[i].AccountName.Replace("'", "''") + "','" + _checks[i].AccountName2.Replace("'", "''") + "',1,'" +
            //        _checks[i].StartingSerial + "','" + _checks[i].EndingSerial + "','','" + _checks[i].BranchName + "','" + _checks[i].Address2 + "','" + _checks[i].Address3 + "','" + _checks[i].Address4 + "','" + _checks[i].Address5 + "','" + _checks[i].Address6 + "')";

            //        oCommand = new OleDbCommand(sql, Connect);

            //        oCommand.ExecuteNonQuery();



            //        Connect.Close();
            //    }
                
            //}
            foreach (string checktype in listofchecks)
            {
                   
                if (checktype == "A" || checktype == "B")
                {
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\Output\\" + CBS.outputFolder + "\\Packing.dbf" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    //foreach (var check in _checks)
                    //{
                    for (int i = 0; i < _checks.Count; i++)
                    {

                    
                        if (_checks[i].ChkName == "")
                        { }
                        else
                        {
                            if (tempCheckType != _checks[i].ChkType)
                                blockNo = 1;

                            tempCheckType = _checks[i].ChkType;

                            if (blockCounter < 4)
                                blockCounter++;
                            else
                            {
                                blockCounter = 1;
                                blockNo++;
                            }

                            string sql = "INSERT INTO PACKING (BATCHNO,BLOCK, RT_NO,BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1,ACCT_NAME2," +
                             "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, ADDRESS1, ADDRESS2,ADDRESS3,ADDRESS4, ADDRESS5, ADDRESS6 ) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + _checks[i].BRSTN + "','" + _checks[i].BranchName.Trim(' ') +
                             "','" + _checks[i].AccountNo + "','" + _checks[i].AccountNo + "','" + _checks[i].ChkType + "','" + _checks[i].AccountName.Replace("'", "''") + "','" + _checks[i].AccountName2.Replace("'", "''") + "',1,'" +
                            _checks[i].StartingSerial + "','" + _checks[i].EndingSerial + "','','" + _checks[i].BranchName.Trim(' ') + "','" + _checks[i].Address2.Trim(' ') + "','" + _checks[i].Address3.Trim(' ') + "','" + _checks[i].Address4.Trim(' ') + "','" + _checks[i].Address5.Trim() + "','" + _checks[i].Address6.Trim() + "')";

                            oCommand = new OleDbCommand(sql, oConnect);

                            oCommand.ExecuteNonQuery();
                        }
                    }
                    oConnect.Close();
                    // }
                    // }
                }
            }
            foreach (string checktype in listofchecks)
            {

                
                if (checktype == "CV")
                {
                    //var checksorder = _checks.FirstOrDefault(e => e.ChkType == checktype);
                  //  _outputFolder = "Check with Voucher";
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\" + Form1.banks + "\\Output\\" + CBS.outputFolder + "\\Packing.DBF" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    foreach (var check in _checks)
                    {
                        if (check.ChkName == "")
                        {
                            //i++;
                            oConnect.Close();
                        }
                        else
                        {
                            if (tempCheckType != check.ChkType)
                                blockNo = 1;

                            tempCheckType = check.ChkType;

                            if (blockCounter < 4)
                                blockCounter++;
                            else
                            {
                                blockCounter = 1;
                                blockNo++;
                            }

                            string sql = "INSERT INTO PACKING (BATCHNO,BLOCK, RT_NO,BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1,ACCT_NAME2," +
                             "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, ADDRESS1, ADDRESS2,ADDRESS3,ADDRESS4, ADDRESS5, ADDRESS6 ) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                             "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                            check.StartingSerial + "','" + check.EndingSerial + "','','" + check.BranchName + "','" + check.Address2 + "','" + check.Address3 + "','" + check.Address4 + "','" + check.Address5 + "','" + check.Address6 + "')";

                            oCommand = new OleDbCommand(sql, oConnect);

                            oCommand.ExecuteNonQuery();
                        }
                    }
                    oConnect.Close();
                    // }
                    // }
                }
            }
            foreach (string checktype in listofchecks)
            {


                if (checktype == "CW")
                {
                   // var checksorder = _checks.FirstOrDefault(e => e.ChkType == checktype);
                   // _outputFolder = "CheckWrite";
                    dbConnection = "Provider=VfpOleDB.1; Data Source=" + Application.StartupPath + "\\" + Form1.banks + "\\Output\\" + CBS.outputFolder + "\\Packing.DBF" + "; Mode=ReadWrite;";

                    //Check if packing file exists
                    //if (!File.Exists(_filepath))
                    //{
                    OleDbConnection oConnect = new OleDbConnection(dbConnection);
                    OleDbCommand oCommand;
                    oConnect.Open();
                    oCommand = new OleDbCommand("DELETE FROM PACKING", oConnect);
                    oCommand.ExecuteNonQuery();
                    foreach (var check in _checks)
                    {
                        if (check.ChkName == "")
                        {
                            //i++;
                            oConnect.Close();
                        }
                        else
                        {
                            if (tempCheckType != check.ChkType)
                                blockNo = 1;

                            tempCheckType = check.ChkType;

                            if (blockCounter < 4)
                                blockCounter++;
                            else
                            {
                                blockCounter = 1;
                                blockNo++;
                            }

                            string sql = "INSERT INTO PACKING (BATCHNO,BLOCK, RT_NO,BRANCH, ACCT_NO, ACCT_NO_P, CHKTYPE, ACCT_NAME1,ACCT_NAME2," +
                             "NO_BKS, CK_NO_B, CK_NO_E, DELIVERTO, ADDRESS1, ADDRESS2,ADDRESS3,ADDRESS4, ADDRESS5, ADDRESS6 ) VALUES('" + _batchNumber + "'," + blockNo.ToString() + ",'" + check.BRSTN + "','" + check.BranchName +
                             "','" + check.AccountNo + "','" + check.AccountNo + "','" + check.ChkType + "','" + check.AccountName.Replace("'", "''") + "','" + check.AccountName2.Replace("'", "''") + "',1,'" +
                            check.StartingSerial + "','" + check.EndingSerial + "','','" + check.BranchName + "','" + check.Address2 + "','" + check.Address3 + "','" + check.Address4 + "','" + check.Address5 + "','" + check.Address6 + "')";

                            oCommand = new OleDbCommand(sql, oConnect);

                            oCommand.ExecuteNonQuery();
                        }
                    }
                    oConnect.Close();
                    // }
                    // }
                }
            }
        }
        public void PrinterFile(List<OrderModel> _checkModel, CBS _mainForm)
        {
            string outputFolder = Application.StartupPath + "\\" + Form1.banks + "\\Output";
            // DbConServices db = new DbConServices();
            // db.GetAllData(_checkModel, _mainForm._batchfile);
            StreamWriter file;

            var listofchecks = _checkModel.Select(e => e.ChkType).Distinct().ToList();

            foreach (string checktype in listofchecks)
            {
                if (checktype == "CV")
                {
                    string printerFilePathA = outputFolder +"\\" + CBS.outputFolder + "\\CV" + _mainForm.batchfile.Substring(0, 4)+  "CA.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePathA))
                        File.Delete(printerFilePathA);

                    file = File.CreateText(printerFilePathA);
                    file.Close();

                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePathA, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPrinterFile(check);

                        file.WriteLine(output);
                    }
                    //}
                    //  ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    // ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }

            }
            foreach (string checktype in listofchecks)
            {
                if (checktype == "B")
                {
                    string printerFilePath = outputFolder + "\\" + CBS.outputFolder + "\\CBS" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPrinterFile(check);

                        file.WriteLine(output);
                    }
                    //}
                    // ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    //ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }
            }
            foreach (string checktype in listofchecks)
            {
                if (checktype == "CW")
                {
                   // _outputFolder = "Check Write";
                    string printerFilePath = outputFolder + "\\" + CBS.outputFolder + "\\CBS" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPrinterFile(check);

                        file.WriteLine(output);
                    }
                    //}
                    // ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    //ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }
            }
            foreach (string checktype in listofchecks)
            {
                if (checktype == "A")
                {
                    //_outputFolder = "Check with Voucher";
                    string printerFilePath = outputFolder +"\\" + CBS.outputFolder + "\\CBS" /*+ _mainForm.batchfile.Substring(0, 4)*/ + "C.txt";
                    var check = _checkModel.Where(e => e.ChkType == checktype).ToList();
                    if (File.Exists(printerFilePath))
                        File.Delete(printerFilePath);

                    file = File.CreateText(printerFilePath);
                    file.Close();
                    //for (int a = 0; a < check.Count; a++)
                    //{


                    using (file = new StreamWriter(File.Open(printerFilePath, FileMode.Append)))
                    {
                        string output = DataOutputServices.ConvertToPrinterFile(check);

                        file.WriteLine(output);
                    }
                    //}
                    // ZipFileServices.CopyPrinterFile(checktype, _mainForm);
                    //ZipFileServices.CopyPackingDBF(checktype, _mainForm);
                }
            }
        }
    }
}
