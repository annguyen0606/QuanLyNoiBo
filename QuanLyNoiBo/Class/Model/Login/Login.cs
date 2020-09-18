using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThe.Model
{
    public class login

    {
        public string iduser{ get; set; }
        public string pass { get; set; }
        public string status { get; set; }
        public login(String user, String pass)
        {
            this.iduser = user;
            this.pass = pass;
        }
       
    }
}
