﻿using System;
using System.IO;

namespace HttpRequests.Models
{
    public class Logger
    {
        public static void Log(string datetime, string rq_type, string cl_ip, string rq_URL)
        {
            using (StreamWriter sw = new StreamWriter("RequestLog.txt", true, System.Text.Encoding.Default))
            {
                sw.WriteLine("{0} ----- {1} ----- {2} ----- {3}", datetime, rq_type, cl_ip, rq_URL);
            }
        }
    }
}
