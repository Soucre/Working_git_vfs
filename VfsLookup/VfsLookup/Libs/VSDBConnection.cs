using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace VfsLookup.Libs
{
    public class VSDBConnection
    {
        static SqlConnection cSOnlineTrading=null;

        static public SqlConnection CSOnlineTrading
        {
            get { 
                if(cSOnlineTrading==null)
                cSOnlineTrading=new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CSOnlineTrading"].ConnectionString);
                return cSOnlineTrading; }
        }
        static SqlConnection cSVSFServices;

        static public SqlConnection CSVSFServices
        {
            get {
                if (cSVSFServices == null)
                    cSVSFServices = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CSVFSServices"].ConnectionString);
                return cSVSFServices; }
        }
        static SqlConnection cSOnlinePrice;

        static public SqlConnection CSOnlinePrice
        {
            get {
                if (cSOnlinePrice == null)
                    cSOnlinePrice = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["CSOnlinePrice"].ConnectionString);
                return cSOnlinePrice; }
        }
        static public DataTable getDataTable(String query,SqlConnection conn)
        {
            DataTable dataTable = new DataTable();
            //try
            //{
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                //string connString = @"your connection string here";
                //string query = "select * from table";

                //SqlConnection conn = new SqlConnection(connString);
                SqlCommand cmd = new SqlCommand(query, conn);
                //conn.Open();

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // this will query your database and return the result to your datatable
                da.Fill(dataTable);
                //conn.Close();
                da.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    ErrorMessage+="\nSQL error: " + query + "\nError:" + ex.Message;
                
            //}
            return dataTable;
        }
        public static string MD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X1"));
            }
            return sb.ToString();
        }
        public static string ErrorMessage = "";
    }
}