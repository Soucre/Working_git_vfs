using NCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ProxyList.Test
{
    public class Base
    {
        public static void Execute<Action>(Action action)
        {
            AsyncTask.Run(() => action);
        }

    }
}
