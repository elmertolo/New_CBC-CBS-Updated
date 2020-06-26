﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using New_CBC_CBS.Models;

namespace New_CBC_CBS.Services
{
    class DataOutputServices
    {
        private static string GenerateSpace(int _noOfSpaces)
        {
            string output = "";

            for (int x = 0; x < _noOfSpaces; x++)
            {
                output += " ";
            }

            return output;

        }//END OF FUNCTION

        private static string Seperator()
        {
            return "";
        }

        public static string ConvertToBlockText(List<OrderModel> _check, string _ChkType, string _batchNumber, DateTime _deliveryDate, string _preparedBy)

        {

            int page = 1, lineCount = 14, blockCounter = 1, blockContent = 1;
            string date = DateTime.Now.ToString("MMM. dd, yyyy");
            bool noFooter = true;
            string countText = "";
            string output = "";

            //Sort Check List
            var sort = (from c in _check
                        orderby c.BRSTN
                        ascending
                        select c).ToList();

         
            output += "\n" + GenerateSpace(8) + "Page No. " + page.ToString() + "\n" +
            GenerateSpace(8) + date +
            "\n" +
            GenerateSpace(15) + "China Bank Savings - SUMMARY OF BLOCK - " + _ChkType.ToUpper() + "\n";
            if (_ChkType == "Check with Voucher")
            {
                output += GenerateSpace(30) + "CONTINUES" + "\n\n"+
                GenerateSpace(5) + "MICR Alignment is 15-54 (Use Basestock w/ 2 Signature Line Only)\n\n";
            }
            else
            {
               
            }
              output +=
            GenerateSpace(15) + " A L L   M A N U A L   E N D O D E D ! ! !" + "\n\n" +
            GenerateSpace(18) +  "Pls. Disregard Series on Hard Copy.\n" +
            GenerateSpace(8) +" Series is maintained by CPC. Series is for every Account \n\n" +
            GenerateSpace(8) + "BLOCK RT_NO" + GenerateSpace(5) + "M ACCT_NO" + GenerateSpace(9) + "START_NO." + GenerateSpace(2) + "END_NO.\n\n";
           // Int64 checkTypeCount = 0;
            foreach (var check in sort)
            {


                if (_ChkType == "PERSONAL")
                {
                  //  checkTypeCount = check.Quantity;
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {

                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }


                if (blockContent == 1)
                {
                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";
                    lineCount += 2;
                }

                if (blockContent == 5)
                {
                    blockContent = 2;

                    blockCounter++;

                    output += "\n" + GenerateSpace(7) + "** BLOCK " + blockCounter.ToString() + "\n";

                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";
                }
                else
                {
                    output += GenerateSpace(12) + blockCounter.ToString() + " " + check.BRSTN + GenerateSpace(3) + check.AccountNo +
                    GenerateSpace(4) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + "\n";

                    lineCount += 1;

                    blockContent++;
                }
            }
            //if (lineCount >=61 )
            //{
            if (noFooter) //ADD FOOTER
            {
                output += "\n " + _batchNumber + GenerateSpace(46) + "DLVR: " + _deliveryDate.ToString("MM-dd(ddd)") + "\n\n" +
                    "B = " + _check.Count + GenerateSpace(20) + _check[0].FileName +"\n\n" +
                    
                    GenerateSpace(4) + "Prepared By" + GenerateSpace(3) + ": " + _preparedBy + "\t\t\t\t RECHECKED BY:\n" +
                    GenerateSpace(4) + "Updated By" + GenerateSpace(4) + ": " + _preparedBy + "\n" +
                    GenerateSpace(4) + "Time Start" + GenerateSpace(4) + ": " + DateTime.Now.ToShortTimeString() + "\n" +
                    GenerateSpace(4) + "Time Finished :\n" +
                    GenerateSpace(4) + "File rcvd" + GenerateSpace(5) + ":\n";

                noFooter = false;
            }

            // output += Seperator();

            lineCount = 1;
            //}

            return output;

        }

        public static string ConvertToPackingList(List<OrderModel> _checks, string _checkType, CBS _mainForm)
        {
            var listofbrstn = _checks.Select(e => e.BRSTN).Distinct().ToList();
            int page = 1;
            string date = DateTime.Now.ToShortDateString();
            string output = "";
            int i = 0;

            foreach (string brstn in listofbrstn)
            {

                output += "\n   Page No. " + page.ToString() + "\n " +
                                  date + "\n" +
                                  GenerateSpace(29) + "CAPTIVE PRINTING CORPORATION\n" +
                                  GenerateSpace(28) + Form1.banks.ToUpper()+" - " + _checkType + " Checks Summary\n\n" +
                                  GenerateSpace(2) + "ACCT_NO" + GenerateSpace(6)+ "QTY" + GenerateSpace(6)+"ACCOUNT NAME" + GenerateSpace(21) + "START #" + GenerateSpace(4) + "END #\n\n\n";

                var listofchecks = _checks.Where(e => e.BRSTN == brstn).ToList();
                output += "   ** ORDERS OF BRSTN " + _checks[i].BRSTN + " " + _checks[i].BranchName + "\n\n" +
                              "   * BATCH #: " + _mainForm.batchfile + "\n\n";



                foreach (var check in listofchecks)
                {

                    if (_checkType == "PERSONAL")
                    {
                        while (check.StartingSerial.Length < 7)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 7)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }
                    else
                    {
                        while (check.StartingSerial.Length < 10)
                            check.StartingSerial = "0" + check.StartingSerial;

                        while (check.EndingSerial.Length < 10)
                            check.EndingSerial = "0" + check.EndingSerial;
                    }//END OF ADDING ZERO IN SERIES NUMBER

                    output += GenerateSpace(2) + check.AccountNo + GenerateSpace(4) +"  1 " + check.ChkType ;

                    if (check.AccountName.Length < 50)
                        output += check.AccountName + GenerateSpace(50 - check.AccountName.Length);
                    else if (check.AccountName.Length > 50)
                        output += check.AccountName2.Substring(0, 50);

                    output +=  GenerateSpace(2) + check.StartingSerial + GenerateSpace(4) + check.EndingSerial + " \n";
                    if (check.AccountName2 != "")
                        output += GenerateSpace(18) + check.AccountName2 + "\n";
                }

                output += "\n";
                output += "  * * * Sub Total * * * " + listofchecks.Count + "\n";

                page++;
                i++;

            }
            output += "  * * * Grand Total * * * " + _checks.Count + "\n";
            return output;

        }// end of function


        public static string ConvertToPrinterFile(List<OrderModel> _checkModels)
        {

            //var listofcheck = _checkModel.Select(e => e.BRSTN).OrderBy(e => e).ToList();

            string output = "";
            var sort = (from c in _checkModels
                        orderby c.BRSTN, c.AccountNo
                        ascending
                        select c).ToList();


            foreach (var check in sort)
            {
                Int64 Series = 0;
                if (check.ChkType == "B")
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 100;
                }
                else
                {
                    Series = Int64.Parse(check.StartingSerial) - 1 + 50;
                }
                Int64 endSeries = Series - 1;

                string outputStartSeries = check.StartingSerial.ToString();

                string outputEndSeries = endSeries.ToString();

                //   string brstnFormat = "";

                string txtSeries = Series.ToString();

                if (check.ChkType == "A")
                {
                    while (check.StartingSerial.Length < 7)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 7)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                else
                {
                    while (check.StartingSerial.Length < 10)
                        check.StartingSerial = "0" + check.StartingSerial;

                    while (check.EndingSerial.Length < 10)
                        check.EndingSerial = "0" + check.EndingSerial;
                }
                output += "3\n" + //1 (FIXED)
                         check.BRSTN + "\n" + //2  (BRSTN)
                         check.AccountNo + "\n" + //3 (ACCT NUMBER)
                         check.EndingSerial + "\n" + //4 (Start Series + PCS per Book)
                         check.ChkType + "\n" + //5 (FIXED)
                         "\n" + //6 (BLANK)
                         check.BRSTN.Substring(0, 5) + "\n" +//7 BRSTN FORMATTED
                         " " + check.BRSTN.Substring(5, 4) + "\n" + //8 BRSTN FORMATTED
                         check.AccountNo.Substring(0, 5) + "-" + check.AccountNo.Substring(5, 5) + "-" + check.AccountNo.Substring(10, 1) + "\n" + //9 (ACCT NUMBER)
                         check.AccountName + "\n" + //10 (NAME 1)
                         "SN\n" + //11 (FIXED)
                         "\n" + //12 (BLANK) 
                         check.AccountName2 + "\n" + //13 (NAME 2)
                         "\n" + //14 (FIXED)
                         "\n" + //15 (BLANK)
                         "\n" + //16 (BLANK)
                         check.BranchName + "\n" + //17 (ADDRESS 1)
                         check.Address2 + "\n" + //18 (ADDRESS 2)
                         check.Address3 + "\n" + //19 (ADDRESS 3)
                         check.Address4 + "\n" + //20 (ADDRESS 4)
                         check.Address5 + "\n" + //21 (ADDRESS 5)
                         check.Address6 + "\n" + //41 (ADDRESS 5)
                         "\n" +//22 (BLANK)
                         "CHINA BANK SAVINGS\n" +//23 (FIXED)
                         "\n" + //24 (BLANK)//
                         "\n" + //25 (BLANK)
                         "\n" + //26 (BLANK)
                         "\n" + //27 (BLANK)
                         "\n" + //28 (BLANK)
                         "\n" + //29 (BLANK)
                         "\n" + //30 (BLANK)
                check.StartingSerial + "\n" + //31 (STARTING SERIES)
                check.EndingSerial + "\n";  //32 (ENDING SERIES)
            }

            return output;
        }
    }
}
