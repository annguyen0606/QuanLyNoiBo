using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThe.Model
{
    public class PERSONALINFORMATION
    {
        public List<ENTITYID> ETNFCIDs = new List<ENTITYID>();
        public string ETName { get; set; }
        public List<ENTITYPHONENUMBER> ETPhones = new List<ENTITYPHONENUMBER>();
        public ENTITYTYPE ETType { get; set; }
        public ENTITYDESCRIP ETDescrip { get; set; }
        public ENTITYADDRESS ETAddress { get; set; }
        public List<ENTITYSERVICES> ETServices = new List<ENTITYSERVICES>();
        public List<ENTITYWORK> ETWorks = new List<ENTITYWORK>();
        public List<ENTITYEDU> ETEducation = new List<ENTITYEDU>();
    }
}
