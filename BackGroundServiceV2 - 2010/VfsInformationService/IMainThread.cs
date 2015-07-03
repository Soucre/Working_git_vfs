using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VfsCustomerInformationServices
{
    public interface IMainThread
    {
        void Start();
        void Stop();
        void Pause();
        void Resume();
    }
}
