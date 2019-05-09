using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strPath, strJson;
            List<FundValue> lsFundValue;

            strPath = string.Format("{0}\\..\\..\\..\\WindowsFormsApplication1\\Data\\MyData.json", System.AppDomain.CurrentDomain.BaseDirectory);
            strJson = SingletonClass.singletonClass.GetData(strPath);

            lsFundValue = Newtonsoft.Json.JsonConvert.DeserializeObject<List<FundValue>>(strJson);

            //RequestData("https://www.fundrich.com.tw/fund/116011.html?id=116011#%E5%9F%BA%E9%87%91%E7%B8%BD%E8%A6%BD");

        }

        private void RequestData(FundValue fundValue)
        {
            //https://www.fundrich.com.tw/fund/116011.html?id=116011#%E5%9F%BA%E9%87%91%E7%B8%BD%E8%A6%BD
            //https://www.fundrich.com.tw/fund/062003.html?id=062003#%E5%9F%BA%E9%87%91%E7%B8%BD%E8%A6%BD
            //https://www.fundrich.com.tw/fund/CIT004.html?id=CIT004#%E5%9F%BA%E9%87%91%E7%B8%BD%E8%A6%BD

            /*
                庫存	*	淨值	*	匯率
                69.189	*	35.91	*	30.8825     =   76730
            
             => 利潤：     76730   /   75000      =   102.3%

             */

            try
            {
                string strPath, strData;

                strPath = string.Format("{0}\\..\\..\\..\\WindowsFormsApplication1\\Data\\MyData.json", System.AppDomain.CurrentDomain.BaseDirectory);
                strData = SingletonClass.singletonClass.RequestUrl(fundValue.strUrl, System.Text.Encoding.UTF8);


                NSoup.Nodes.Document htmlDoc = NSoup.NSoupClient.Parse(strData);
                NSoup.Select.Elements ele = htmlDoc.GetElementsByTag("span");

                for (int i = 0; i < ele.Count; i++)
                {
                    if ((ele[i].Dataset.ContainsKey("reactid")) && (ele[i].Dataset["reactid"] == "491"))
                    {
                        fundValue.netValue = SingletonClass.singletonClass.ConvertStringToDouble(ele[i].Text().Trim());
                    }

                    if ((ele[i].Dataset.ContainsKey("reactid")) && (ele[i].Dataset["reactid"] == "492"))
                    {
                        fundValue.cur = ele[i].Text().Trim();
                    }
                }


            }
            catch(Exception ex)
            {
                SingletonClass.singletonClass.WriteLog(ex.ToString());
            }

        }


    }
}
