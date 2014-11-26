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
        public static void AddLog(string log,EventLogEntryType type)
        {
            try
            {
                if (!EventLog.SourceExists("SysMonitor"))
                {
                    EventLog.CreateEventSource("SysMonitor", "SysMonitor");
                }
                EventLog2.Source = "SysMonitor";
                EventLog2.WriteEntry(log,type);
            }
            catch { }
        }
        public static void AddError(string log)
        {
            AddLog(log, EventLogEntryType.Error);
        }
        public static void AddInfo(string log)
        {
            AddLog(log, EventLogEntryType.Information);
        }
        public static void AddWarning(string log)
        {
            AddLog(log, EventLogEntryType.Warning);
        }
    }
}
