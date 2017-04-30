using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace F4CIOsDNSUpdaterCommon
{
    public class DataAccess
    {
        private static Object iniLock = new Object();
        private static Object logLock = new Object();
        private static Object htmLock = new Object();

        public static void WriteLog(string logString)
        {
            lock (logLock)
            {
                try
                {

                    if (!File.Exists(Constants.LogFilePath))
                    {
                        using (StreamWriter sw = File.CreateText(Constants.LogFilePath))
                        {
                            sw.Close();
                        }
                    }
                    using (StreamWriter sw = File.AppendText(Constants.LogFilePath))
                    {
                        sw.WriteLine(logString);
                        sw.Close();
                    }
                }
                catch (Exception) { };
            }
        }        

        public static void SaveSettings(long interval, string uri)
        {
            lock (iniLock)
            {
                try
                {

                    List<string> linesList = new List<string>();
                    linesList.Add(interval.ToString());
                    linesList.Add(uri);
                    File.WriteAllLines(Constants.IniFilePath, linesList.ToArray());
                }
                catch (Exception exception)
                {
                    WriteLog(DateTime.Now + "Error occured while writeing to file " + Constants.IniFilePath + ". Error details:" + exception.Message);
                }
            }
        }

        public static long GetInterval()
        {
            long interval = 600000;

            lock (iniLock)
            {
                try
                {
                    if (File.Exists(Constants.IniFilePath))
                    {
                        string[] lines = File.ReadAllLines(Constants.IniFilePath);
                        interval = Convert.ToInt64(lines[0]);
                    }
                }
                catch (Exception exception)
                {
                    WriteLog(DateTime.Now + "Error occured while reading Interval from " + Constants.IniFilePath + ". Error details:" + exception.Message);
                }
            }

            return interval;
        }

        public static string GetUri()
        {
            string uri = @"http://www.google.com";

            try
            {

                if (File.Exists(Constants.IniFilePath))
                {
                    string[] lines = File.ReadAllLines(Constants.IniFilePath);
                    uri = lines[1];
                }
            }
            catch (Exception exception)
            {
                WriteLog(DateTime.Now + "Error occured while reading Uri from "+Constants.IniFilePath+". Error details:"+exception.Message);
            }

            return uri;
        }

        public static string RequestUri(string updateUri)
        {
            string responseString = null;

            try
            {
                WriteLog(DateTime.Now + " Requesting '" + updateUri + "'...");

                // used to build entire input
                StringBuilder sb = new StringBuilder();

                // used on each read operation
                byte[] buf = new byte[8192];

                // prepare the web page we will be asking for
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(updateUri);

                // execute the request
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                // we will read data via the response stream
                Stream resStream = response.GetResponseStream();

                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?

                responseString = sb.ToString();
            }
            catch (Exception exception)
            {
                WriteLog(DateTime.Now + " Requesting of '" + updateUri + "' failed. Error details:" + exception.Message);
            }

            WriteLog(DateTime.Now + " Requesting succeeded.");
            return responseString;
        }

        public static void SaveResponseHtm(string responseString)
        {
            lock (htmLock)
            {
                try
                {
                    File.WriteAllText(Constants.HtmFilePath, responseString);
                }
                catch (Exception exception)
                {
                    WriteLog(DateTime.Now + "Error occured while saving response string to " + Constants.HtmFilePath + ". Error details:" + exception.Message);
                }
            }
        }
    }
}
