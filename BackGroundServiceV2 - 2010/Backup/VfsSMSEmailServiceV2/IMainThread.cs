using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VfsSMSEmailServiceV2
{
    public interface IMainThread
    {
        void Start();
        void Stop();
        void Pause();
        void Resume();
    }
}
