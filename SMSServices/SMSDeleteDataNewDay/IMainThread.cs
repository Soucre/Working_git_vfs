using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMS
{
    public interface IMainThread
    {
        void Start();
        void Stop();
        void Pause();
        void Resume();
    }
}
