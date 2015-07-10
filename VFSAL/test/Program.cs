using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

        }
        /// <summary>
        /// public static void makeWorkbook(ArrayList aktRecords, Hashtable recCount, int numWorkbooks)
        /// 
        /// make the workbook from the array that was generated.
        /// </summary>
        /// <param name="aktRecords"></param>
        /// <param name="recCount"></param>
        /// <param name="numWorkbooks"></param>
        public static void makeWorkbook(ArrayList aktRecords, Hashtable recCount)
        {
            //Declare these two variables globally so you can access them from both
            //Button1 and Button2.
            Excel.Application objApp;
            Excel._Workbook objBook;

            Excel.Workbooks objBooks;
            Excel.Sheets objSheets;
            Excel._Worksheet objSheet;

            Excel.Range range;

            try
            {
                // Instantiate Excel and start a new workbook.
                objApp = new Excel.Application();
                if (objApp == null)
                {
                    return;
                }
                
                // Uncomment the line below if you want to see what's happening in Excel
                objApp.Visible = true;

                objBooks = objApp.Workbooks;
                objBook = objApp.Workbooks.Open(""); //objBooks.Add(Missing.Value);
                objSheets = objBook.Worksheets;
                /*

                int i = 0;  // each site array.
                String siteCode;

                foreach (String[,] ary in aktRecords.ToArray())
                {


                    siteCode = ary[1, 12];

                    int count = (int)recCount[siteCode];

                    objSheet = (Excel.Worksheet)objSheets.Add(Missing.Value);
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    range = objSheet.get_Range("A1", Missing.Value);

                    range = range.get_Resize(count + 1, 12);

                    //Set the range value to the array.
                    range.set_Value(Missing.Value, ary);
                    objSheet.get_Range("A1", "K1").Font.Bold = true;
                    objSheet.get_Range("A1", "K1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    objSheet.get_Range("G1", "K1").ColumnWidth = 25;
                    objSheet.get_Range("B1", "F1").ColumnWidth = 10;
                    objSheet.get_Range("A1").ColumnWidth = 12;
                    objSheet.get_Range("I1").ColumnWidth = 10;
                    objSheet.Name = siteCode;
                }


                //Return control of Excel to the user.
                objApp.Visible = true;
                objApp.UserControl = true;
                 * */

            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                //MessageBox.Show(errorMessage, "Error");
            }

        }
    }
}
