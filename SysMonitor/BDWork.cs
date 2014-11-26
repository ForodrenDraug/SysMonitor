using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.Common;
using MySql.Data.Types;
using MySql.Data.MySqlClient;
using System.Timers;

namespace SysMonitor
{
    class BDWork
    {
        int limit = 10000;
        bool connected = false;
        MySqlCommand command;
        MySqlConnection sql_connection;
        String BD;
        Timer timer;
        Queue<string> queue=new Queue<string>();

        public BDWork(String BD)
        {
            
            this.BD = BD;
            this.timer = new System.Timers.Timer(1000D);
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
            connect();
        }
        ~BDWork()
        {
            this.timer.Close();
            this.timer = null;
        }
        
        public void Enqueue(string str){
            while(queue.Count>=limit)
                queue.Dequeue();
            queue.Enqueue(str);
        }
        private void sendQueue(){
            if (queue.Count > 0)
            {
                string sql = "INSERT INTO "+Wmi.pcname()+" (`date`,`cpuuse`,`memoryuse`,`cputemp`) VALUES ";

                sql += queue.Dequeue();
                while (queue.Count > 0)
                    sql += "," + queue.Dequeue();
                sendCommand(sql);
            }
        }
        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
            if (connected)
                sendQueue();
            else
                connect();
        }
             
        private void connect(){
            try
            {
                sql_connection = new MySqlConnection(BD);
                sql_connection.Open();

                command = sql_connection.CreateCommand();
              
                Log.AddInfo("connect() : connect succesfull");

                if (!db_loadTableList().Exists(x => x == Wmi.pcname()))
                {
                    command.CommandText = "CREATE TABLE " + Wmi.pcname() + "(date datetime, cpuuse int, memoryuse int, cputemp double)";
                    Log.AddInfo("sql: " + command.CommandText);
                    command.ExecuteNonQuery();
                }
                connected = true;
            }
            catch (Exception e)
            {
                Log.AddError("connect() {global} : " + e.Message);
                connected = false;
            }
        }

        public void sendCommand(String scommand)
        {
          try
                {
                Log.AddInfo("SendCommand() sql: " + scommand);
                command.CommandText = scommand;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Log.AddError("sendCommand() : " + e.Message);
                    connected = false;

                }
        }
        
        private List<string> db_loadTableList()
        {
            List<string> tablelist = new List<string>();

            try
            {
                //   MySqlCommand cmdName = new MySqlCommand("SHOW TABLES FROM " + "onsevl_test", sql_connection);

                MySqlCommand cmdName = new MySqlCommand("SHOW TABLES FROM " + "test", sql_connection);
                MySqlDataReader reader = cmdName.ExecuteReader();

                while (reader.Read())
                {
                    tablelist.Add(reader.GetString(0));
                }

                reader.Close();

                return tablelist;
            }
            catch (System.Exception excep)
            {
                Log.AddError("db_loadTableList() : " + excep.Message);
                return tablelist;
            }
        }
    }
}
