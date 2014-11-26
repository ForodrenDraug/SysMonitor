using System;
using System.ServiceProcess;
namespace SysMonitor
{
    public partial class Sys_Monitor : ServiceBase
    {
        public Sys_Monitor()
        {
            InitializeComponent();
        }

        #region
        DateTime dt;
        BDWork bd;
        private System.Timers.Timer timer;
        #endregion

        protected override void OnStart(string[] args)
        {
            bd = new BDWork("Server=mysql.onsevl.myjino.ru; Database=onsevl_test; Uid=onsevl; Pwd=svetofor");

            this.timer = new System.Timers.Timer(1000D);
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
        }

        protected override void OnStop(){
            this.timer.Stop();
            this.timer = null;
        }

        #region
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Log.AddLog("timer_Elapsed(...) : Tick");
            dt = DateTime.Now;
            bd.Enqueue(" (" + dt.TimeOfDay.TotalMinutes.ToString().Replace(',', '.') + "," + Wmi.cpuuse().Replace(',', '.') + "," + Wmi.memoryuse().Replace(',', '.') + "," + Wmi.cputemp().Replace(',', '.') + ")");
            //bd.sendCommand("INSERT INTO app1 (`date`,`cpuuse`,`memoryuse`,`cputemp`) VALUES (" + dt.TimeOfDay.TotalMinutes.ToString().Replace(',', '.') + "," + Wmi.cpuuse().Replace(',', '.') + "," + Wmi.memoryuse().Replace(',', '.') + "," + Wmi.cputemp().Replace(',', '.') + ")");
        }                      
        #endregion             
    }                          
}                              
                               
                               