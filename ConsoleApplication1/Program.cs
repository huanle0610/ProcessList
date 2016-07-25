using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using System.Management;
using System.Runtime.InteropServices;
using Cassia;
using System.Security.Principal;

namespace ProcessListApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            String hostname = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            ITerminalServicesManager manager = new TerminalServicesManager();
            using (ITerminalServer server = manager.GetRemoteServer(hostname))
            {
                server.Open();
                foreach (ITerminalServicesSession session in server.GetSessions())
                {
                    NTAccount account = session.UserAccount;
                    string userName = session.UserName;

                    if (account != null)
                    {
                        Console.WriteLine(String.Format("{0} {1}", session.SessionId, account));
                    }
                }
            }


            Console.WriteLine(GetProcessList(0));

            Console.ReadLine();
        }

        static void useDb()
        {
            Db db = null;
            if (db == null)
            {
                db = new Db();
            }
            db.bind("id", "1");
            DataTable d = db.query("SELECT * FROM Persons WHERE id > @id");

            Dictionary<string, string> row = new Dictionary<string, string>();

            if (d.Rows.Count > 0)
            {
                for (int i = 0; i < d.Columns.Count; i++)
                {
                    row.Add(d.Columns[i].ColumnName.ToLower(), d.Rows[0][i].ToString());
                    System.Console.WriteLine(d.Rows[0][i].ToString());
                }
            }

            String name = "";
            row.TryGetValue("Lastname", out name);

            try
            {
                string a = row["lastname"];
            }
            catch (Exception ex)
            {
                db.CloseConn();
                string exception = "Exception : " + ex.Message.ToString() + "\n\rApplication will close now. \n\r";
                Console.WriteLine(exception + "/n/r");
                Environment.Exit(1);
            }
        }

        static void getSystemAccount()
        {
            SelectQuery query = new SelectQuery("Win32_UserAccount");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            foreach (ManagementObject envVar in searcher.Get())
            {
                Console.WriteLine("Username : {0}", envVar["Name"]);
            }
        }

        static string GetProcessOwner(int processId)
        {
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return DOMAIN\user
                    return argList[1] + "\\" + argList[0];
                }
            }

            return "";
        }

        static string GetProcessList(int process_type)
        {
            string query = "Select ProcessId,Caption,SessionId,executablepath,CreationDate From Win32_Process Where SessionId  != 0";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            foreach (ManagementObject obj in processList)
            {
                try
                {
                    DateTime create_dt = ManagementDateTimeConverter.ToDateTime(obj.GetPropertyValue("CreationDate").ToString());
                    string caption = obj.GetPropertyValue("Caption").ToString();
                    string executablepath = "";
                    Object executablePath = obj.GetPropertyValue("executablepath");
                    if (executablePath != null)
                    {
                        executablepath = executablePath.ToString();
                    }

                    int SessionId = Convert.ToInt32(obj.GetPropertyValue("SessionId"));
                    int processId = Convert.ToInt32(obj.GetPropertyValue("ProcessId"));
                    string owner = GetProcessOwner(processId);
                    string name = System.Environment.MachineName;

                    Console.WriteLine(String.Format("{5} {0} {1} {2} {4} {3} {6}", SessionId, processId, caption, executablepath, owner, name, create_dt));
                }
                catch (Exception ex)
                {
                    string exception = "Exception : " + ex.Message.ToString() + "\n\rApplication will close now. \n\r";
                    Console.WriteLine(exception + "/n/r");
                    Console.ReadLine();
                    Environment.Exit(1);
                }


            }

            return "amtf";
        }

    }
}