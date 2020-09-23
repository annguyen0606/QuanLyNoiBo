using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using QuanLyNoiBo.Class.Model.HRdepartment.DuLieuChamCong;
using Newtonsoft.Json.Linq;

namespace QuanLyNoiBo.UserControls.HRDepartment
{
    public partial class UCLoadDuLieuChamCong : UserControl
    {
        public UCLoadDuLieuChamCong()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fromDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            String toDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            ShowReport(fromDate,toDate);
        }
        public void ShowReport(String fromDate ,String toDate)
        {
            var client = new RestClient("http://cloudapi.conek.vn/api/GetDataDiemDanh?function=all&company=Conek&fromday="+fromDate+"&today="+toDate);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            List<Staff> staffList = new List<Staff>();
            JObject jObject = JObject.Parse(response.Content);
            JArray jArray = JArray.Parse(jObject["table"].ToString());

            for (int i = 0; i < jArray.Count; i++)
            {
                Staff staff = new Staff();
                JObject jObject1 = JObject.Parse(jArray[i].ToString());
                foreach (KeyValuePair<string, JToken> property in jObject1)
                {
                    if (property.Key.ToString().Trim().Equals("staffid"))
                    {
                        staff.staffID = property.Value.ToString().Trim();
                    }
                    else if (property.Key.ToString().Trim().Equals("staffname"))
                    {
                        staff.staffName = property.Value.ToString();
                    }
                    else if (property.Key.ToString().Trim().Equals("companyname"))
                    {
                        staff.companyName = property.Value.ToString();
                    }
                    else if (property.Key.ToString().Trim().Equals("department"))
                    {
                        staff.deparment = property.Value.ToString();
                    }
                    else if (property.Key.ToString().Trim().Equals("daytouch"))
                    {
                        staff.dayTouch = property.Value.ToString();
                    }
                    else if (property.Key.ToString().Trim().Equals("timehours"))
                    {
                        staff.timeHours = property.Value.ToString();
                    }
                    else if (property.Key.ToString().Trim().Equals("timelate"))
                    {
                        staff.timeLate = property.Value.ToString();
                    }
                }
                staffList.Add(staff);
            }

            List<StaffReport> stafReportList = new List<StaffReport>();
            StaffReport staffReport = null;
            for (int i = 0; i < staffList.Count; i++)
            {
                //Add the last element
                if (i == (staffList.Count - 1))
                {
                    staffReport = new StaffReport();
                    staffReport.staffID = staffList[i].staffID;
                    staffReport.staffName = staffList[i].staffName;
                    staffReport.companyName = staffList[i].companyName;
                    staffReport.deparment = staffList[i].deparment;
                    staffReport.dayTouch = staffList[i].dayTouch;
                    staffReport.timeStart = staffList[i].timeHours;
                    staffReport.time = staffList[i].timeLate;
                    stafReportList.Add(staffReport);
                }
                //Add dulicate element
                else if (staffList[i].staffID == staffList[i + 1].staffID && staffList[i].dayTouch == staffList[i + 1].dayTouch)
                {
                    staffReport = new StaffReport();
                    staffReport.staffID = staffList[i].staffID;
                    staffReport.staffName = staffList[i].staffName;
                    staffReport.companyName = staffList[i].companyName;
                    staffReport.deparment = staffList[i].deparment;
                    staffReport.dayTouch = staffList[i].dayTouch;
                    staffReport.timeStart = staffList[i + 1].timeHours;
                    staffReport.timeOut = staffList[i].timeHours;
                    staffReport.time = staffList[i + 1].timeLate;
                    stafReportList.Add(staffReport);
                }
                //Add unduplicate element
                else
                {
                    staffReport = new StaffReport();
                    staffReport.staffID = staffList[i].staffID;
                    staffReport.staffName = staffList[i].staffName;
                    staffReport.companyName = staffList[i].companyName;
                    staffReport.deparment = staffList[i].deparment;
                    staffReport.dayTouch = staffList[i].dayTouch;
                    staffReport.timeStart = staffList[i].timeHours;
                    staffReport.time = staffList[i].timeLate;
                    stafReportList.Add(staffReport);
                }
            }

            List<StaffReport> staffReports = new List<StaffReport>(); //Trum cuoi 
            for (int i = 0; i < stafReportList.Count; i++)
            {
                //Add last element 
                if (i == stafReportList.Count - 1)
                {
                    staffReports.Add(stafReportList[i]);
                }
                //Remove dulicate element 
                else if (stafReportList[i].staffID == stafReportList[i + 1].staffID && stafReportList[i].dayTouch == stafReportList[i + 1].dayTouch)
                {
                    StaffReport sf = new StaffReport();
                    stafReportList[i + 1].timeOut = stafReportList[i].timeOut;
                }
                else staffReports.Add(stafReportList[i]);
            }
            //Check note
            for (int i = 0; i < staffReports.Count; i++)
            {
                if (staffReports[i].timeOut == null)
                {
                    staffReports[i].note = "No checkout ";
                    if (Int32.Parse(staffReports[i].time) > 0) staffReports[i].note = "Late and No checkout";
                }
                else if (Int32.Parse(staffReports[i].time) <= 0)
                {
                    staffReports[i].time = "0";
                }
                else if (Int32.Parse(staffReports[i].time) > 0)
                {
                    staffReports[i].note = "Late";
                }
            }
            dataGridView1.DataSource = staffReports;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            copyAlltoClipboard();
            Microsoft.Office.Interop.Excel.Application xlexcel;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlexcel = new Microsoft.Office.Interop.Excel.Application();
            xlexcel.Visible = true;
            xlWorkBook = xlexcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range CR = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[1, 1];
            CR.Select();
            xlWorkSheet.PasteSpecial(CR, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true);
        }
        private void copyAlltoClipboard()
        {
            dataGridView1.SelectAll();
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }
    }
}
