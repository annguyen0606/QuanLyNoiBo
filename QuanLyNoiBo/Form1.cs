using DevComponents.DotNetBar;
using QuanLyNoiBo.UserControls.HRDepartment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
