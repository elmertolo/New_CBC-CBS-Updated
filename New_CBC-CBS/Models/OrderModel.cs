using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New_CBC_CBS.Models
{
    class OrderModel
    {
        private string returnBlankIfNull(string _input)
        {
            if (_input == null)
                return "";
            else
                return _input;
        }

        public string BRSTN { get; set; }
        public string AccountNo { get; set; }
        private string _accountName2;
        private string _accountName;
        public string AccountName
        {
            get
            {
                return (returnBlankIfNull(_accountName));
            }
            set { _accountName = value; }
        }
        public string AccountName2
        {
            get
            {
                return (returnBlankIfNull(_accountName2));
            }
            set { _accountName2 = value; }
        }
        public Int64 Quantity { get; set; }
        public string ChkType { get; set; }
        public string ChkName { get; set; }
        public string PcsPerbook { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string StartingSerial { get; set; }
        public string EndingSerial { get; set; }
        public string BranchName { get; set; }
        public string Address2 { get; set; }
        public string Batchfile { get; set; }
        private string _address3;
        private string _address4;
        private string _address5;
        public string Address3
        {
            get
            {
                return (returnBlankIfNull(_address3));
            }
            set { _address3 = value; }
        }
    public string Address4
        {
            get
            {
                return (returnBlankIfNull(_address4));
            }
            set { _address4 = value; }
        }
        public string Address5
        {
            get
            {
                return (returnBlankIfNull(_address5));
            }
            set { _address5 = value; }
        }
public string Address6 { get; set; }
        public string BranchCode { get; set; }
        //public string BaeStock { get; set; }
        //public string Company { get; set; }
        public DateTime deliveryDate { get; set; }
    }
}
