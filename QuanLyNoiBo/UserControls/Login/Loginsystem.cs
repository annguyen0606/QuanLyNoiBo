using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNoiBo.UserControls.Login
{
    public partial class Loginsystem : Form
    {
        
        public Loginsystem()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
               
        }

        //IFirebaseClient client;
       
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Dock = DockStyle.Fill;
            this.Visible = false;
            frm.Show();
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn hủy bỏ ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
        protected void ReallyCenterToScreen()
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };
        }
        private void Loginsystem_Load(object sender, EventArgs e)
        {

            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            ReallyCenterToScreen();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }
    }

    
}
