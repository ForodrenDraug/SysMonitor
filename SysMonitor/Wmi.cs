using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace SysMonitor
{
    static class Wmi
    {

        public static String pcname()
        {
            string name = "Test";
            return name;
        }

        public static String cpuuse()
        {
            int processuse = 0;
            return processuse.ToString();
        }

        public static String memoryuse()
        {
            int memoryuse = 0;
            return memoryuse.ToString();
        }

        public static String cputemp()
        {
            int processuse = 0;

            
            /*ManagementObjectSearcher searcher =
                new ManagementObjectSearcher("root\\WMI",
                                 "SELECT * FROM MSAcpi_ThermalZoneTemperature");

            ManagementObjectCollection.ManagementObjectEnumerator enumerator =
                searcher.Get().GetEnumerator();

            while (enumerator.MoveNext())
            {
                ManagementBaseObject tempObject = enumerator.Current;
                Console.WriteLine(tempObject["CurrentTemperature"].ToString());
            }*/


            return processuse.ToString();
        }

    }
}
