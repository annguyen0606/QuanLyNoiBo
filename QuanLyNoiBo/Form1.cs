using DevComponents.DotNetBar;
using QuanLyNoiBo.UserControls.HRDepartment;
using QuanLyNoiBo.UserControls.Login;
using QuanLyThe.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QuanLyNoiBo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        private bool KiemTraTenTabConTrol(string name)
        {
            for (int i = 0; i < superTabControl1.Tabs.Count; i++)
            {
                if (superTabControl1.Tabs[i].Text == name)
                {
                    superTabControl1.SelectedTabIndex = i;
                    return true;
                }
            }
            return false;
        }
        private void btn_RegistrationNew_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Registration") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Registration");
                UCRegistration frm = new UCRegistration();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void ribbonBar2_ItemClick(object sender, EventArgs e)
        {
         
        }

        private void btn_LoadInfor_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&UCLoaddata") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&UCLoaddata");
                UCloaddata frm = new UCloaddata();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 0;
            }
        }


        private void applicationButton1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonTabItem1_Click(object sender, EventArgs e)
        {

        }

        

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void btn_UpdateInfor_Click(object sender, EventArgs e)
        {
          
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&loginsystem") == false)
            {
           
                Loginsystem frm = new Loginsystem();
                frm.Dock = DockStyle.Fill;
                this.Visible = false;
                frm.Show();
              
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&changerpassword") == false)
            {

                changerpassword frm = new changerpassword();
                frm.Dock = DockStyle.Fill;
                this.Visible = false;
                frm.Show();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnLoadDuLieuChamCong_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&UCLoadDuLieuChamCong") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&UCLoadDuLieuChamCong");
                UCLoadDuLieuChamCong frm = new UCLoadDuLieuChamCong();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }
    }
}
