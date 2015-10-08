using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;
using System.Configuration;
using Bussiness;
using System.Data.SqlClient;
using System.Data;

namespace SMS
{
    public static class Ultility
    {
        static string FileOutPut = ConfigurationManager.AppSettings["FileOutPut"].ToString();

        public static void LogFile(string sExceptionName, string directory)
        {

            StreamWriter log;

            if (!File.Exists(directory))
            {
                log = new StreamWriter(directory);
            }

            else
            {
                log = File.AppendText(directory);
            }

            // Write to the file:

            log.WriteLine("Data Time:" + DateTime.Now + "-------" + sExceptionName);
            
            // Close the stream:

            log.Close();

        }

        public static void deleteData()
        {
            //sendSMS.SendSPAM("0909070481", "test gui tin nhan cash 1");

            IRepository<VFS_CheckSMSSent> repoVFS_CheckSMSSent = new VFS_CheckSMSSentRepository();
            VFS_CheckSMSSent VFS_CheckSMSSent = new VFS_CheckSMSSent();

            // xóa tin nhan da gui cho ngay moi
            repoVFS_CheckSMSSent.Delete(VFS_CheckSMSSent); // delete all data in VFS_CheckSMSSent

        }
        static public string CheckDatabaseConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["CheckDatabaseConnection"].ToString();

            }
        }
        public static bool CheckConnectionSQL()
        {
            try
            {
                using (SqlConnection myConn = new SqlConnection(CheckDatabaseConnection))
                {
                    SqlCommand myCmd = new SqlCommand("SELECT COUNT(*) FROM [master].[dbo].[spt_values]", myConn);
                    if (myConn.State != ConnectionState.Open)
                        myConn.Open();
                    myCmd.ExecuteNonQuery();
                    return (myConn.State == ConnectionState.Open);

                }
            }
            catch
            {
                return false;
            }

        }
    }
}
