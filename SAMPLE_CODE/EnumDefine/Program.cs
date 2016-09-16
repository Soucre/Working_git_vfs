using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumDefine
{
    class Program
    {
        static void Main(string[] args) {
            //
            // Compute two with the exponent of 30.
            //
            //foreach (int value in ComputePower(2, 30)) {
            //    Console.Write(value);
            //    Console.Write(" ");
            //}
            string swccase = "VN";
            switch (HelperEnumn.Parse<Enumtest>(swccase)) {
                case Enumtest.VN:
                    break;
                default:
                    break;
            }
            Console.WriteLine();
        }

        //public static IEnumerable<int> ComputePower(int number, int exponent) {
        //    int exponentNum = 0;
        //    int numberResult = 1;
        //    //
        //    // Continue loop until the exponent count is reached.
        //    //
        //    while (exponentNum < exponent) {
        //        //
        //        // Multiply the result.
        //        //
        //        numberResult *= number;
        //        exponentNum++;
        //        //
        //        // Return the result with yield.
        //        //
        //        yield return numberResult;
        //    }
        //}

        public static IEnumerable<int> ComputePower(int number, int exponent) {
            int exponentNum = 0;
            int numberResult = 1;
            var listResult = new List<int>();
            //
            // Continue loop until the exponent count is reached.
            //
            for (int i = 0; i < exponent; i++) {
                numberResult *= number;
                exponentNum++;
                listResult.Add(numberResult);
            }

            return listResult as IEnumerable<int>;
            //while (exponentNum < exponent) {
            //    //
            //    // Multiply the result.
            //    //
            //    numberResult *= number;
            //    exponentNum++;
            //    //
            //    // Return the result with yield.
            //    //
            //    yield return numberResult;
            //}
        }
    }
}
