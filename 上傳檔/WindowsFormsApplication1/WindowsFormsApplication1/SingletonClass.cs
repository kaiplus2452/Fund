using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class SingletonClass
    {
        public static SingletonClass singletonClass = new SingletonClass();

        
        public bool WriteLog(string strLog)
        {
            bool bolSuc = false;

            try
            {
                string strPath = string.Format("{0}\\..\\..\\..\\WindowsFormsApplication1\\Log\\{1}-Log.txt", System.AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("MM-dd-yyyy"));

                lock (UsefulValues.LOCK_LOG_FILE)
                {
                    using (StreamWriter sw = new StreamWriter(strPath, true))
                    {
                        sw.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), strLog));
                        bolSuc = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return bolSuc;
        }


        public bool WriteMsg(string strMsg, string strPath)
        {
            bool bolSuc = false;

            try
            {
                lock (UsefulValues.LOCK_DATA_FILE)
                {
                    using (StreamWriter sw = new StreamWriter(strPath, true))
                    {
                        sw.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), strMsg));
                        bolSuc = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return bolSuc;
        }


        public string RequestUrl(string strUrl, Encoding encoding)
        {
            string strResponse = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(strUrl))
                {
                    return strResponse;
                }

                HttpWebRequest request = HttpWebRequest.Create(strUrl) as HttpWebRequest;
                request.Method = WebRequestMethods.Http.Get;
                request.Timeout = 10 * 1000;
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return strResponse;
        }


        public string RequestPostUrl(string strUrl, string strParameter, Encoding encoding)
        {
            string strResponse = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(strUrl))
                {
                    return strResponse;
                }

                byte[] bs = encoding.GetBytes(strParameter);

                HttpWebRequest request = HttpWebRequest.Create(strUrl) as HttpWebRequest;
                request.Method = WebRequestMethods.Http.Post;
                request.Timeout = 10 * 1000;
                request.ContentLength = bs.Length;

                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return strResponse;
        }


        public string GetData(string strPath)
        {
            string strRtn = string.Empty;

            try
            {
                lock (UsefulValues.LOCK_DATA_FILE)
                {
                    using (StreamReader sr = new StreamReader(strPath))
                    {
                        strRtn = sr.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return strRtn;
        }


        public double ConvertStringToDouble(string strNum)
        {
            double doubleNum = -0.001F;

            try
            {
                double.TryParse(strNum, out doubleNum);

                if (doubleNum != -0.001F)
                {
                    return doubleNum;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.ToString());
            }

            return doubleNum;
        }


    }
}
