using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace F4CIOsDNSUpdaterCommon
{
    public class Threads
    {
        public static Thread StartWorkingThread()
        {
            Thread newThread = new Thread(new ThreadStart(ThreadLoop));
            newThread.Start();
            return newThread;
        }

        public static void ThreadLoop()
        {
            while (true)
            {
                long intervalInMiliseconds = DataAccess.GetInterval();

                Thread.Sleep(Convert.ToInt32(intervalInMiliseconds));

                Threads.DoJob(null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">pass null to use value from .ini file</param>
        /// <returns></returns>
        public static string DoJob(string uri)
        {
            string response = string.Empty;

            if (uri == null)
            {
                uri = DataAccess.GetUri();
            }

            try
            {
                response = DataAccess.RequestUri(uri);

                if (!string.IsNullOrEmpty(response))
                {
                    if (response.Length < 255) DataAccess.WriteLog(DateTime.Now + " Response recieved:" + response);

                    DataAccess.SaveResponseHtm(response);
                }
            }
            catch (Exception exception)
            {
                DataAccess.WriteLog(DateTime.Now + " Error occured while requesting response and writing LastResponse.htm . Error details:" + exception.Message);
            }
 

            return response;
        }
    }
}
