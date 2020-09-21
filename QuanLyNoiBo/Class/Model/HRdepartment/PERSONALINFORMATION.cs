using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThe.Model
{
    public class PERSONALINFORMATION
    {
        public ENTITYID ETNfcID = new ENTITYID();
        public string ETName { get; set; }
        public ENTITYPHONENUMBER ETPhones = new ENTITYPHONENUMBER();

        public ENTITYTYPE ETType { get; set; }
        public ENTITYDESCRIP ETDescrip = new ENTITYDESCRIP();
        public ENTITYADDRESS ETAddress { get; set; }
        public ENTITYSERVICES ETServices { get; set; }
        public ENTITYWORK ETWorks = new ENTITYWORK();
        public ENTITYEDU ETEducation { get; set; }
    }
}
