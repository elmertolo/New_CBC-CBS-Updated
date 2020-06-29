using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using New_CBC_CBS.Models;
using New_CBC_CBS.Services;

namespace New_CBC_CBS
{
    public partial class CBS : Form
    {
        public string bank = "";
        List<OrderModel> orderList = new List<OrderModel>();
        DbConServices con = new DbConServices();
        List<BranchModel> branches = new List<BranchModel>();
        ProcessServices process = new ProcessServices();
        ZipFileServices zip = new ZipFileServices();
        List<OrderModel> bcheck = new List<OrderModel>();
        public string batchfile = "";
        DateTime dateTime;
        public DateTime deliveryDate;
        public Int64 quan = 0;
        public Int64 quantotal = 0;
        public static string outputFolder = "";
        // Form1 f1 = new Form1();
        string errorMessage = "";
        public CBS()
        {
            InitializeComponent();
            dateTime = dateTimePicker1.MinDate = DateTime.Now; //Disable selection of backdated dates to prevent errors  
        }
        
        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.GetBatchFile(bcheck);
            string tempBatch = "";
            batchfile = txtBatch.Text;
            if (bcheck == null)
                Generate();
            else
            {
                for (int a = 0; a < bcheck.Count; a++)
                {
                    if (bcheck[a].Batchfile == batchfile)
                    {
                        MessageBox.Show("Batchfile Already Exist!!!");
                        // goto isExist;
                        tempBatch = bcheck[a].Batchfile;
                        break;


                    }



                }
                //  batchfile = txtBatch.Text;
                if (tempBatch == batchfile)
                {
                    MessageBox.Show("Change the batch number!!");
                    //goto isExist;

                }
                else

                    Generate();

            }

            //for (int a = 0; a < bcheck.Count; a++)
            //{
            //if (bcheck[a].Batchfile == batchfile)
            //{
            //    MessageBox.Show("Batchfile Already Exist!!!");

            //}

            //  }

            //process.PackingText(orderList, this);
            //process.DoBlockProcess(orderList, this);
            //process.PrinterFile(orderList, this);
            //process.SaveToPackingDBF(orderList, batchfile, this);
            //for (int i = 0; i < orderList.Count; i++)
            //{
            //    if (orderList[i].ChkName == "")
            //    {
            //        i++;
            //    }
            //    else
            //        con.SavedDatatoDatabase(orderList[i],batchfile);

            //}

            //con.DumpMySQL(); //Create database backup
            //zip.ZipFileS(LogIn.userName, this); // creating zip file for processed data\
            //zip.CopyZipFile(LogIn.userName, this);
            //MessageBox.Show("Done!");
            //Environment.Exit(0);


        }
        private void Generate()
        {

            process.DoBlockProcess(orderList, this);
            process.PackingText(orderList, this);
            process.PrinterFile(orderList, this);
            process.SaveToPackingDBF(orderList, batchfile, this);
            for (int i = 0; i < orderList.Count; i++)
            {
                if (orderList[i].ChkName == "")
                {
                    i++;
                }
                else
                    con.SavedDatatoDatabase(orderList[i], batchfile);

            }

            con.DumpMySQL(); //Create database backup
            zip.ZipFileS(LogIn.userName, this); // creating zip file for processed data\
            zip.CopyZipFile(LogIn.userName, this);
            ZipFileServices.CopyPacking(LogIn.userName, this);
            ZipFileServices.DeleteExcel();
            MessageBox.Show("Done!");
            Environment.Exit(0);
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            con.GetAllBranches(branches);

            batchfile = txtBatch.Text;
            deliveryDate = dateTimePicker1.Value;
         

               
            if (txtBatch.Text == "")
             {
                    MessageBox.Show("Please Enter Batch Number!!!");
             }
           else
                {

                if (deliveryDate == dateTime)
                {
                    MessageBox.Show("Please set Delivery Date!");
                }
                else
                {
                    //con.GetBatchFile(bcheck, batchfile);
                    //if (bcheck[0].Batchfile != batchfile)
                    //{
                        deliveryDate = dateTimePicker1.Value;
                        if (Directory.GetFiles(Application.StartupPath + "\\" + Form1.banks + "\\Head\\").Length == 0) // if the path folder is empty
                            MessageBox.Show("No files found in directory path", "***System Error***");
                        else
                        {
                            string[] list = Directory.GetFiles(Application.StartupPath + "\\" + Form1.banks + "\\Head\\");

                            string Extension = "";

                            foreach (string FileName in list)
                            {
                                //Get the Extension Name
                                int LoopCount = FileName.ToString().Length - 2;
                                while (LoopCount > 0)
                                {

                                    if (FileName.ToString().Substring(LoopCount, 1) == "." && Extension == "")
                                    {
                                        Extension = FileName.ToString().Substring(LoopCount + 1, FileName.ToString().Length - LoopCount - 1).ToUpper();
                                    }

                                    LoopCount = LoopCount - 1;
                                }
                                // MessageBox.Show(Extension);

                                if (Extension == "XLS" || Extension == "XLSX")
                                {

                                    // string _fileName = "";
                                    for (int i = 0; i < list.Length; i++)
                                    {

                                        // _fileName = Path.GetFileName(list[i]);
                                        Excel.Application xlApp = new Excel.Application();
                                        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(list[0]);
                                   // MessageBox.Show(FileName);
                                        int SheetsCount = xlWorkbook.Sheets.Count;
                                        for (int b = 0; b < SheetsCount; b++)
                                        {
                                            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[b + 1];
                                            Excel.Range xlRange = xlWorksheet.UsedRange;

                                            int rowCount = xlRange.Rows.Count;
                                            int colCount = xlRange.Columns.Count;
                                            string SheetName = xlWorksheet.Name.ToUpper();


                                            //  int row = 5;
                                            string BRSTN = xlRange.Cells[6, 7].Text;
                                            string startiSerial = xlRange.Cells[9, 3].Text;
                                            string q = "";
                                            Int64 qty = 0;
                                            q = xlRange.Cells[9, 7].Text;
                                            //string endseries = "";
                                            Int64 es = 0;
                                            for (int c = 0; c < rowCount - 160; c++)
                                            {

                                                qty = Int64.Parse(q) / 100;
                                                /*check.Quantity = Int64.Parse(qty) / 100;*/

                                                for (int s = 0; s < qty; s++)
                                                {
                                                    OrderModel check = new OrderModel();
                                                    check.BRSTN = BRSTN;
                                                    check.ChkName = xlRange.Cells[c + 9, 2].Text;
                                                    check.AccountNo = xlRange.Cells[c + 9, 4].Text;
                                                    // check.EndingSerial = startiSerial;
                                                    check.StartingSerial = startiSerial;
                                                    check.AccountName = xlRange.Cells[c + 9, 5].Text;


                                                    check.Quantity = 1;
                                                    var listofbranch = branches.FirstOrDefault(sr => sr.BRSTN == check.BRSTN);
                                                    check.BranchName = listofbranch.Address1.TrimEnd();
                                                    check.Address2 = listofbranch.Address2.TrimEnd();
                                                    check.Address3 = listofbranch.Address3.TrimEnd();
                                                    check.Address4 = listofbranch.Address4.TrimEnd();
                                                    check.Address5 = listofbranch.Address5.TrimEnd();
                                                    check.Address6 = listofbranch.Address6.TrimEnd();
                                                    check.Batchfile = batchfile;

                                                    if (check.ChkName.Contains("Voucher"))
                                                    {
                                                        check.ChkType = "CV";
                                                        check.PcsPerbook = "100";
                                                        outputFolder = "CheckwithVoucher";
                                                        quan = qty;
                                                        check.FileName = "CV" + batchfile.Substring(0, 4) + "CA.txt";
                                                    }

                                                    else if (check.ChkName.Contains("Write") || check.ChkName.Contains("write"))
                                                    {
                                                        check.ChkType = "CW";
                                                        check.PcsPerbook = "100";
                                                        quan = qty;
                                                        outputFolder = "CheckWrite";
                                                        check.FileName = "CWR" + batchfile.Substring(0, 4) + "CA.txt";
                                                    }
                                                    es = Int64.Parse(check.StartingSerial) + 100;
                                                    check.EndingSerial = es.ToString(); ;
                                                    startiSerial = es.ToString();
                                                    orderList.Add(check);
                                                    // check.StartingSerial = check.EndingSerial;


                                                }
                                                // row++;

                                            }

                                        xlWorkbook.Close(0);
                                        xlApp.Quit();
                                    }

                                    }

                                }

                            }
                            quantotal += quan;

                        }

                        lblCV.Text = quan.ToString();

                        if (errorMessage == "")
                        {
                            //toolStripProgressBar.Visible = false;

                            //BindingSource checkBind = new BindingSource();
                            //checkBind.DataSource = orderList;
                            //dataGridView1.DataSource = checkBind;
                            MessageBox.Show("No Errors Found", "System Message");

                            generateToolStripMenuItem.Enabled = true;

                            checkToolStripMenuItem.Enabled = false;
                            encodeToolStripMenuItem.Enabled = false;
                        }
                        else
                            MessageBox.Show(errorMessage, "System Error");
                   // }
                    //else { MessageBox.Show("Batch Already Exist!!");}
                }
            }
        }
    }
}
