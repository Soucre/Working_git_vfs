using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeJob.Data.Base
{
    public class Disposable: IDisposable
    {
        #region Constructor

        /// <summary>
        /// Construct
        /// </summary>
        protected Disposable()
        {
        }

        #endregion

        #region IDisposable Related Functions

        /// <summary>
        /// Dispose function
        /// </summary>
        public virtual void Dispose()
        {
            try {
                Dispose(true);
            }
            catch { }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Function to override in order to dispose objects
        /// </summary>
        /// <param name="Managed">If true, managed and unmanaged objects should be disposed. Otherwise unmanaged objects only.</param>
        protected virtual void Dispose(bool bManaged)
        {
        }

        #endregion
    }
}
