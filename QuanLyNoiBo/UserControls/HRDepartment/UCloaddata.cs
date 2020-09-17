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
            dataGridViewX1.Columns[7].Name = "Title";
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
               
                PERSONALINFORMATION InformationPersonnal = new PERSONALINFORMATION();
                JObject root = JObject.Parse(jArray[i].ToString());
                foreach (KeyValuePair<string, JToken> property in root)
                {
                    if (property.Key.ToString().Trim() == "entitynfcid")
                    {
                        JArray array = JArray.Parse(property.Value.ToString());
                        for (int j = 0; j < array.Count; j++)
                        {

                            JObject jObject1 = JObject.Parse(array[j].ToString());
                            ENTITYID eNTITYID = new ENTITYID();
                            foreach (KeyValuePair<string, JToken> property1 in jObject1)
                            {

                                if (property1.Key.ToString() == "NFCID")
                                {
                                    eNTITYID.NFCID = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString() == "DateOn")
                                {
                                    eNTITYID.DateOn = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString() == "DateOff")
                                {
                                    eNTITYID.DateOff = property1.Value.ToString();
                                }

                            }
                            InformationPersonnal.ETNFCIDs.Add(eNTITYID);
                        }
                    }
                    else if (property.Key.ToString().Trim() == "entityname")
                    {
                        InformationPersonnal.ETName = property.Value.ToString();
                    }
                    else if (property.Key.ToString().Trim() == "entityphonenumber")
                    {
                        JArray array = JArray.Parse(property.Value.ToString());
                        for (int j = 0; j < array.Count; j++)
                        {
                            JObject jObject1 = JObject.Parse(array[j].ToString());
                            ENTITYPHONENUMBER nTITYPHONENUMBER = new ENTITYPHONENUMBER();
                            foreach (KeyValuePair<string, JToken> property1 in jObject1)
                            {
                                if (property1.Key.ToString() == "PHONENUMBER")
                                {
                                    nTITYPHONENUMBER.PHONENUMBER = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString() == "DateOn")
                                {
                                    nTITYPHONENUMBER.DateOn = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString() == "DateOff")
                                {
                                    nTITYPHONENUMBER.DateOff = property1.Value.ToString();
                                }
                            }
                            InformationPersonnal.ETPhones.Add(nTITYPHONENUMBER);
                        }
                    }
                    else if (property.Key.ToString().Trim() == "entitytype")
                    {
                       ENTITYTYPE eNTITYTYPE = new ENTITYTYPE();
                        JObject jObject1 = JObject.Parse(property.Value.ToString());
                        foreach (KeyValuePair<string, JToken> property1 in jObject1)
                        {
                            if (property1.Key.ToString() == "TypeName")
                            {
                                eNTITYTYPE.TypeName = property1.Value.ToString();
                            }
                            else if (property1.Key.ToString() == "TypeID")
                            {
                                eNTITYTYPE.TypeID = property1.Value.ToString();
                            }
                        }
                        InformationPersonnal.ETType = eNTITYTYPE;
                    }
                    else if (property.Key.ToString().Trim() == "entitydescrip")
                    {
                        JObject jObject1 = JObject.Parse(property.Value.ToString());
                       ENTITYDESCRIP eNTITYDESCRIP = new ENTITYDESCRIP();
                        foreach (KeyValuePair<string, JToken> property1 in jObject1)
                        {
                            if (property1.Key.ToString().Trim() == "DateOfBirth")
                            {
                                eNTITYDESCRIP.DateOfBirth = property1.Value.ToString();
                            }
                            else if (property1.Key.ToString().Trim() == "IdentifyCard")
                            {
                                eNTITYDESCRIP.IdentifyCard = property1.Value.ToString();
                            }
                            else if (property1.Key.ToString().Trim() == "SocialInsuranceNumber")
                            {
                                eNTITYDESCRIP.SocialInsuranceNumber = property1.Value.ToString();
                            }
                            else if (property1.Key.ToString().Trim() == "TaxIdentificationNumber")
                            {
                                eNTITYDESCRIP.TaxIdentificationNumber = property1.Value.ToString();
                            }
                            else if (property1.Key.ToString().Trim() == "Notes")
                            {
                                eNTITYDESCRIP.Notes = property1.Value.ToString();
                            }
                        }
                        InformationPersonnal.ETDescrip = eNTITYDESCRIP;
                    }
                    else if (property.Key.ToString().Trim() == "entitywork")
                    {
                        JArray jArray1 = JArray.Parse(property.Value.ToString());
                        for (int j = 0; j < jArray1.Count; j++)
                        {
                            JObject jObject1 = JObject.Parse(jArray1[j].ToString());
                            ENTITYWORK eNTITYWORK = new ENTITYWORK();

                            foreach (KeyValuePair<string, JToken> property1 in jObject1)
                            {
                                if (property1.Key.ToString().Trim() == "Company")
                                {
                                    eNTITYWORK.Company = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString().Trim() == "DateOn")
                                {
                                    eNTITYWORK.DateOn = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString().Trim() == "Position")
                                {
                                    eNTITYWORK.Position = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString().Trim() == "DateOff")
                                {
                                    eNTITYWORK.DateOff = property1.Value.ToString();
                                }
                                else if (property1.Key.ToString().Trim() == "Office")
                                {
                                    eNTITYWORK.Office = property1.Value.ToString();
                                }
                            }
                            InformationPersonnal.ETWorks.Add(eNTITYWORK);
                        }
                    }
                }
                

                InformationPersonnals.Add(InformationPersonnal);
            }
           for(int i = 0; i< InformationPersonnals.Count; i++)

            {
                dataGridViewX1.Rows.Add();
                for (int K = 0; K < InformationPersonnals[i].ETNFCIDs.Count; K++)
                {
                    if (InformationPersonnals[i].ETNFCIDs[K].NFCID.ToString().Contains("ON"))
                    {
                        dataGridViewX1.Rows[i].Cells[0].Value = InformationPersonnals[i].ETNFCIDs[K].NFCID.ToString().Split(',')[0];
                    }
                }
                dataGridViewX1.Rows[i].Cells[1].Value = InformationPersonnals[i].ETName;
                for (int j = 0; j < InformationPersonnals[i].ETPhones.Count; j++)
                {
                    if (InformationPersonnals[i].ETPhones[j].PHONENUMBER.ToString().Contains("ON"))
                    {
                        dataGridViewX1.Rows[i].Cells[3].Value = InformationPersonnals[i].ETPhones[j].PHONENUMBER.ToString().Split(',')[0];
                    }
                    
                }
                dataGridViewX1.Rows[i].Cells[4].Value = InformationPersonnals[i].ETDescrip.IdentifyCard;
                dataGridViewX1.Rows[i].Cells[2].Value = InformationPersonnals[i].ETDescrip.DateOfBirth;
                for (int d = 0; d < InformationPersonnals[i].ETWorks.Count; d++)
                {
                  if (InformationPersonnals[i].ETWorks[d].Company.ToString().Contains("ON"))
                    {
                        dataGridViewX1.Rows[i].Cells[9].Value = InformationPersonnals[i].ETWorks[d].Company.ToString().Split(',')[1];
                        dataGridViewX1.Rows[i].Cells[8].Value = InformationPersonnals[i].ETWorks[d].DateOn;
                        dataGridViewX1.Rows[i].Cells[7].Value = InformationPersonnals[i].ETWorks[d].Position;
                    }
       
                  
                }
                dataGridViewX1.Rows[i].Cells[5].Value = InformationPersonnals[i].ETDescrip.TaxIdentificationNumber;


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
