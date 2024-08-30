using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tekton.Domain.Base
{
    public static class LogWriter
    {
        private static string m_exePath = string.Empty;
        public static void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); 
            if (!File.Exists(m_exePath + "\\" + "log.txt")) { 
                string s = ""; 
                s = DateTime.Now.ToString("h:mm:ss tt"); 
                FileStream fs = 
                 File.Create(m_exePath + "\\" + "log.txt"); 
                Byte[] info = new UTF8Encoding(true).GetBytes("File Created:" + s + "\r\n"); 
                fs.Write(info, 0, info.Length); 
                fs.Close(); 
            }
            try
            {
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                    AppendLog(logMessage, w);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void AppendLog(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
