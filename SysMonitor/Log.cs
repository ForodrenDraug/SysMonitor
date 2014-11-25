using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SysMonitor
{
    static class Log
    {
        static EventLog EventLog2 = new System.Diagnostics.EventLog();
        public static void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("SysMonitor"))
                {
                    EventLog.CreateEventSource("SysMonitor", "SysMonitor");
                }
                EventLog2.Source = "SysMonitor";
                EventLog2.WriteEntry(log);
            }
            catch { }
        }
    }
}
