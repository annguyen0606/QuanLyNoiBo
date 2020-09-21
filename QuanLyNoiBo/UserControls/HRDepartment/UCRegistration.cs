using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DevComponents.DotNetBar.Controls;
using RestSharp;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using System.Diagnostics.Eventing.Reader;

namespace QuanLyNoiBo.UserControls.HRDepartment
{
    public partial class UCRegistration : UserControl
    {
        public String linkImage = "";
        public UCRegistration()
        {
            InitializeComponent();
        }

        private void contextMenuBar1_ItemClick(object sender, EventArgs e)
        {

        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            var client1 = new RestClient("http://cloudapi.conek.vn/api/CheckDataExists?function=id&dataCheck=" + txmanv.Text);
            client1.Timeout = -1;
            var request1 = new RestRequest(Method.GET);
            IRestResponse response1 = client1.Execute(request1);
            JObject jObject1 = JObject.Parse(response1.Content);

            int result1 = int.Parse(jObject1.GetValue("err_code").ToString());
            if (result1 == 0)
            {

            }
           
            else if (result1 == 10)
            {
                MessageBox.Show("Tài khoản không thể tạo ", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Clear_Text();
                return;
            }
            else
            {
                MessageBox.Show("Lỗi Internet", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            byte[] imgAvatar = null;
            string strAvatar = "";

            if (linkImage != "")
            {
                imgAvatar = Controler.hinhanh.ReadBytePic(linkImage);
                strAvatar = Convert.ToBase64String(imgAvatar, 0, imgAvatar.Length);
            }
            else
            {
                strAvatar = "null";
            }
            if (txmanv.Text.Length <= 0)
            {
                MessageBox.Show("Mã nhân viên thêm ", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var modelID = new List<QuanLyThe.Model.ENTITYID>
            {
                new QuanLyThe.Model.ENTITYID
                {
                    NFCID = txmanv.Text+",ON",
                    DateOn = "", DateOff = ""
                }
            };

            var modelPhone = new List<QuanLyThe.Model.ENTITYPHONENUMBER>
            {
                new QuanLyThe.Model.ENTITYPHONENUMBER
                {
                    PHONENUMBER = txSDT.Text+",ON",
                    DateOn = "", DateOff = ""
                }
            };
            String dateOfBirth = "";
            if (dateTimebirthdate.Value.ToString("dd-MM-yyyy") == "01-01-0001")
            {

            }
            else
            {
                dateOfBirth = dateTimebirthdate.Value.ToString("dd-MM-yyyy");
            }
            var modelDescrip = new QuanLyThe.Model.ENTITYDESCRIP
            {
                DateOfBirth = dateOfBirth,
                Gender = cbgender.Text,
                IdentifyCard = tbcmnd.Text,
                PassportNumber = tbpassport.Text,
                SocialInsuranceNumber = tbBHXH.Text,
                TaxIdentificationNumber = tbmst.Text
            };

            var modelAddress = new QuanLyThe.Model.ENTITYADDRESS
            {
                NativePlace = txbnguyenquan.Text,
                CurrentHomeAddress = txbchoohientai.Text
            };

            var modelService = new List<QuanLyThe.Model.ENTITYSERVICES>
            {
                new QuanLyThe.Model.ENTITYSERVICES
                {
                    ServiceName = cbdichvu.Text+",ON",
                    DateOn = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"),
                    DateOff = ""
                }
            };
            String dateStart = "";
            if (dateTimestartwork.Value.ToString("dd-MM-yyyy") == "01-01-0001")
            {

            }
            else
            {
                dateStart = dateTimestartwork.Value.ToString("dd-MM-yyyy");
            }
            var modelWork = new List<QuanLyThe.Model.ENTITYWORK>
            {
                new QuanLyThe.Model.ENTITYWORK
                {
                    Company = comboBoxcongty.Text + ",ON",
                    DateOn = dateStart,
                    DateOff = "",
                    Department = txbvitri.Text,
                    Office = txbvanphong.Text
                }
            };

            var modelEdu = new List<QuanLyThe.Model.ENTITYEDU>
            {
                new QuanLyThe.Model.ENTITYEDU
                {
                    Kindergartern = txbKinder.Text,
                    KindFrom = datefromkinder.Value.ToString("yy-MM-yyyy"),
                    KindTo = datetokinder.Value.ToString("yy-MM-yyyy"),
                    Primary = txbprima.Text,
                    PrimaryFrom = datefromprima.Value.ToString("yy-MM-yyyy"),
                    PrimaryTo = datetoprima.Value.ToString("yy-MM-yyyy"),
                    Secondary = txbsec.Text,
                    SecondaryFrom = datefromsec.Value.ToString("yy-MM-yyyy"),
                    SecondaryTo = datetosec.Value.ToString("yy-MM-yyyy"),
                    High = txbhigh.Text,
                    HighFrom = datefromhigh.Value.ToString("yy-MM-yyyy"),
                    HighTo = dateTohigh.Value.ToString("yy-MM-yyyy"),
                    College = txbCollege.Text,
                    CollegeFrom = datefromCollege.Value.ToString("yy-MM-yyyy"),
                    CollegeTo = datetoCollege.Value.ToString("yy-MM-yyyy")
                }
            };
            var modelType = new QuanLyThe.Model.ENTITYTYPE
            {
                TypeName = "person",
                TypeID = "1"
            };
            var client = new RestClient("http://cloudapi.conek.vn/api/InsertEntities");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("EntityNFCID", JsonConvert.SerializeObject(modelID));
            request.AddParameter("EntityName", tbtennv.Text);
            request.AddParameter("EntityPhoneNumber", JsonConvert.SerializeObject(modelPhone));
            request.AddParameter("EntityType", JsonConvert.SerializeObject(modelType));
            request.AddParameter("EntityAvatar", strAvatar);
            request.AddParameter("EntityDescrip", JsonConvert.SerializeObject(modelDescrip));
            request.AddParameter("EntityAddress", JsonConvert.SerializeObject(modelAddress));
            request.AddParameter("EntityRelation", null);
            request.AddParameter("EntityServices", JsonConvert.SerializeObject(modelService));
            request.AddParameter("EntityNotes", null);
            request.AddParameter("EntityWork", JsonConvert.SerializeObject(modelWork));
            request.AddParameter("EntityEducation", JsonConvert.SerializeObject(modelEdu));
            IRestResponse response = client.Execute(request);



            JObject jObject = JObject.Parse(response.Content);

            int result = int.Parse(jObject.GetValue("err_code").ToString());
            if (result == 0)
            {
                MessageBox.Show("Bạn đã đăng kí thành công", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear_Text();
            }
            else if (result == 10)
            {
                MessageBox.Show("Không thể tạo tài khoản", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Clear_Text();
            }
            else
            {
                MessageBox.Show("Lỗi  Internet", "ThÃ´ng bÃ¡o", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void Clear_Text()
        {
            txmanv.Text = "";
            tbtennv.Text = "";
            tbcmnd.Text = "";
            dateTimebirthdate.Value = new DateTime(0001, 01, 01);
            txbnguyenquan.Text = "";
            tbpassport.Text = "";
            tbBHXH.Text = "";
            txbchoohientai.Text = "";
            tbmst.Text = "";
            txbEmail.Text = "";
            dateTimestartwork.Value = new DateTime(0001, 01, 01);
            txbvitri.Text = "";
            txSDT.Text = "";
            txbKinder.Text = "";
            txbsec.Text = "";
            txbhigh.Text = "";
            datefromkinder.Value = new DateTime(0001, 01, 01);
            datetokinder.Value = new DateTime(0001, 01, 01);
            datefromhigh.Value = new DateTime(0001, 01, 01);
            dateTohigh.Value = new DateTime(0001, 01, 01);
            datefromsec.Value = new DateTime(0001, 01, 01);
            datefromsec.Value = new DateTime(0001, 01, 01);
        }


        public TimeSpan Timeout { get; internal set; }




        private void UCRegistration_Load(object sender, EventArgs e)
        {
            cbgender.Items.Add("Nam");
            cbgender.Items.Add("Nữ ");

            dateTimestartwork.Value = DateTime.Today;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            linkImage = Controler.hinhanh.LayDuongDanHinhAnh();
            pictureBox1.ImageLocation = linkImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        private void txbEmail_Leave(object sender, EventArgs e)
        {

            Regex mRegxExpression;

            if (txbEmail.Text.Trim() != string.Empty)

            {

                mRegxExpression = new Regex("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");

                if (!mRegxExpression.IsMatch(txbEmail.Text.Trim()))

                {

                    MessageBox.Show("E-mail address format is not correct.", "MojoCRM", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txbEmail.Focus();

                }
            }
        }

        private void txmanv_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbcmnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbpassport_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tbmst_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}