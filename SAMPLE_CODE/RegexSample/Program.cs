using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RegexSample
{
    class Program
    {
        public static IEnumerable<int> Numbers(int max) {
            for (int i = 0; i < max; i++) {
                Console.WriteLine("Returning {0}", i);
                yield return i;
            }
        }
        static void Main(string[] args) // Caller
        {
            var test = GetSuperUserRights(true);
            var tdfasest = EnumUserRight.Add;
        }

        static IEnumerable<EnumUserRight> GetSuperUserRights(bool SuperUsersAllowed) {
            return SuperUsersAllowed
                   ? new[] { EnumUserRight.Add, EnumUserRight.Edit, EnumUserRight.Remove }
                   : Enumerable.Empty<EnumUserRight>();
        }
    }

}
