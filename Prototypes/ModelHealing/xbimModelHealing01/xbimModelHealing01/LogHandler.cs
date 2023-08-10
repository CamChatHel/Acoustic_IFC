using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace xbimModelHealing01
{
    public class LogHandler
    {
        StreamWriter sw = new StreamWriter("LogModelHealing.txt");
        
        public void WriteInLog (string text)
        {
            sw.WriteLine (text);
        }

        public void EndLog()
        {
            sw.WriteLine("End of Model Healing");
            sw.Close();
        }

        
    }
}
