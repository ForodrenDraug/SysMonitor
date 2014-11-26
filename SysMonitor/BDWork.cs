using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.Common;
using MySql.Data.Types;
using MySql.Data.MySqlClient;

namespace SysMonitor
{
    class BDWork
    {
        bool connected = false;
        MySqlCommand command;
        MySqlConnection sql_connection;
        String BD;

        public BDWork(String BD)
        {
            this.BD = BD;
            connect();
        }
             
        private void connect(){
            try
            {
                sql_connection = new MySqlConnection(BD);
                sql_connection.Open();

                command = sql_connection.CreateCommand();
              
                Log.AddLog("connect() : connect succesfull");

                if (db_loadTableList().Exists(x => x == Wmi.pcname()))
                {
                    command.CommandText = "CREATE TABLE " + Wmi.pcname() + "(data datetime, cpuuse int, memoryuse int, cputemp double)";
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Log.AddLog("connect() {global} : " + e.Message);
                connected = false;
            }
        }

        public void sendCommand(String scommand)
        {
            if (connected)
            {
                try
                {
                    command.CommandText = scommand;
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Log.AddLog("sendCommand() : " + e.Message);
                    connected = false;

                }
            }
            else
            {
                connect();
                
            }
        }
        
        private List<string> db_loadTableList()
        {
            List<string> tablelist = new List<string>();

            try
            {
                MySqlCommand cmdName = new MySqlCommand("SHOW TABLES FROM " + "onsevl_test", sql_connection);
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
                Log.AddLog("db_loadTableList() : " + excep.Message);
                return tablelist;
            }
        }
    }
}
