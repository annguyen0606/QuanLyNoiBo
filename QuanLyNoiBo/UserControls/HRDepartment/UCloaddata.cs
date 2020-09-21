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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThirdParty.Json.LitJson;
using System.IO;

using System.Security.Cryptography;
using QuanLyThe.Model;

namespace QuanLyNoiBo.UserControls.HRDepartment
{
    public partial class UCloaddata : UserControl
    {
        public UCloaddata()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(2370, 1221);
            dataGridViewX1.AutoResizeColumns();
           dataGridViewX1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewX1.ColumnCount = 10;
            dataGridViewX1.Columns[0].Name = "ID";
            dataGridViewX1.Columns[1].Name = "Name";
            dataGridViewX1.Columns[2].Name = "Birthday";
            dataGridViewX1.Columns[3].Name = "SDT";
            dataGridViewX1.Columns[4].Name = "CMND";
            dataGridViewX1.Columns[5].Name = "MST";
            dataGridViewX1.Columns[6].Name = "BHXH";
            dataGridViewX1.Columns[7].Name = "Department";
            dataGridViewX1.Columns[8].Name = "DayStart";
            dataGridViewX1.Columns[9].Name = "Status";

        }

        private void loaddata()
        {
            var client = new RestClient("http://cloudapi.conek.vn/api/GetData?function=all&dataSearch=Conek&status=ON");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            String json = JsonConvert.SerializeObject(client);
            List<PERSONALINFORMATION> InformationPersonnals = new List<PERSONALINFORMATION>();


            JObject jObject = JObject.Parse(response.Content);
            JArray jArray = (JArray)jObject["table"];
            for (int i = 0; i < jArray.Count; i++)
            {
                PERSONALINFORMATION pERSONALINFORMATION = new PERSONALINFORMATION();
                JObject jObject1 = JObject.Parse(jArray[i].ToString());
                foreach (KeyValuePair<string, JToken> property in jObject1)
                {
                    if (property.Key.ToString().Trim().Equals("nfcid"))
                    {
                        JObject jObject2 = JObject.Parse(property.Value.ToString());
                        foreach (KeyValuePair<string, JToken> property2 in jObject2)
                        {
                            JObject jObject3 = JObject.Parse(property2.Value.ToString());
                            foreach (KeyValuePair<string, JToken> property3 in jObject3)
                            {
                                if (property3.Key.ToString().Trim().Equals("NFCID"))
                                {
                                    pERSONALINFORMATION.ETNfcID.NFCID = property3.Value.ToString().Trim();
                                }
                                else if (property3.Key.ToString().Trim().Equals("Status"))
                                {
                                    if (property3.Value.ToString().Trim().Equals("ON"))
                                    {

                                    }
                                    else
                                    {
                                        pERSONALINFORMATION.ETNfcID.NFCID = "";
                                        break;
                                    }
                                }
                                
                            }


                        }
                    }

                    else if (property.Key.ToString().Trim().Equals("phonenumber"))
                    {
                        JObject jObjectphonenumber = JObject.Parse(property.Value.ToString());
                        foreach (KeyValuePair<string, JToken> propertyphonenumber in jObjectphonenumber)
                        {
                            JObject jObject3 = JObject.Parse(propertyphonenumber.Value.ToString());
                            foreach (KeyValuePair<string, JToken> property3 in jObject3)
                            {
                                if (property3.Key.ToString().Trim().Equals("Phonenumber"))
                                {
                                    pERSONALINFORMATION.ETPhones.PHONENUMBER = property3.Value.ToString().Trim();
                                }
                                /*else if (property3.Key.ToString().Trim().Equals("Status"))
                                {
                                    if (property3.Value.ToString().Trim().Equals("ON"))
                                    {

                                    }
                                    else
                                    {
                                        pERSONALINFORMATION.ETPhones.PHONENUMBER = "";
                                        break;
                                    }
                                }*/
                            }

                        }
                    }
                    if (property.Key.ToString().Trim().Equals("name"))
                    {
                        pERSONALINFORMATION.ETName = property.Value.ToString();
                    }
                    if (property.Key.ToString().Trim().Equals("birthday"))
                    {
                        pERSONALINFORMATION.ETDescrip.DateOfBirth = property.Value.ToString();
                    }
                    if (property.Key.ToString().Trim().Equals("identifycard"))
                    {
                        pERSONALINFORMATION.ETDescrip.IdentifyCard = property.Value.ToString();
                    }
                    if (property.Key.ToString().Trim().Equals("dateon"))
                    {
                        pERSONALINFORMATION.ETWorks.DateOn = property.Value.ToString();
                    }
                    if (property.Key.ToString().Trim().Equals("department"))
                    {
                        pERSONALINFORMATION.ETWorks.Department = property.Value.ToString();
                    }
                    if (property.Key.ToString().Trim().Equals("taxnumber"))
                    {
                        pERSONALINFORMATION.ETDescrip.TaxIdentificationNumber = property.Value.ToString();
                    }
                    if (property.Key.ToString().Trim().Equals("socialinsurance"))
                    {
                        pERSONALINFORMATION.ETDescrip.SocialInsuranceNumber = property.Value.ToString();
                    }


                }


                InformationPersonnals.Add(pERSONALINFORMATION);


            }

            for (int i = 0; i < InformationPersonnals.Count; i++)

            {
                dataGridViewX1.Rows.Add();
                for (int K = 0; K < InformationPersonnals[i].ETNfcID.NFCID.Count(); K++)
                {
                    dataGridViewX1.Rows[i].Cells[0].Value = InformationPersonnals[i].ETNfcID.NFCID.ToString(); 
                        
                }
                dataGridViewX1.Rows[i].Cells[1].Value = InformationPersonnals[i].ETName.ToString();
                dataGridViewX1.Rows[i].Cells[2].Value = InformationPersonnals[i].ETDescrip.DateOfBirth.ToString();
                for (int j = 0; j < InformationPersonnals[i].ETPhones.PHONENUMBER.Count(); j++)
                {
                    dataGridViewX1.Rows[j].Cells[3].Value = InformationPersonnals[j].ETPhones.PHONENUMBER.ToString();
                }

                dataGridViewX1.Rows[i].Cells[4].Value = InformationPersonnals[i].ETDescrip.IdentifyCard.ToString();
                dataGridViewX1.Rows[i].Cells[5].Value = InformationPersonnals[i].ETDescrip.TaxIdentificationNumber.ToString();
                dataGridViewX1.Rows[i].Cells[6].Value = InformationPersonnals[i].ETDescrip.SocialInsuranceNumber.ToString();
                dataGridViewX1.Rows[i].Cells[7].Value = InformationPersonnals[i].ETWorks.Department.ToString();
                dataGridViewX1.Rows[i].Cells[8].Value = InformationPersonnals[i].ETWorks.DateOn.ToString();
              //  dataGridViewX1.Rows[i].Cells[9].Value = InformationPersonnals[i].ETWorks.Position.ToString();
            }

        }



        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void UCloaddata_Load(object sender, EventArgs e)
        {
            loaddata();

        }
    }
}
