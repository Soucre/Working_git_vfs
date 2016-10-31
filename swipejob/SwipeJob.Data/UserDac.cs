using SwipeJob.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeJob.Data
{
    public class UserDac: Disposable
    {
        #region [Properties]
        /// <summary>
        /// Query select
        /// </summary>
        public string QueryString { get; set; }
        
        /// <summary>
        /// SP Name 
        /// </summary>
        public string SPName { get; set; }

        #endregion

        #region Constructor

        #endregion  



    }
}
