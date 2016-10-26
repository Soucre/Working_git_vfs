using System;
using log4net;

namespace SwipeJob.Utility
{
    public class LoggingHelper
    {
        private static readonly ILog _log = LogManager.GetLogger(string.Empty);

        public static void Error(string message)
        {
            _log.Error(message);
        }

        public static void Info(string message)
        {
            _log.Info(message);
        }

        public static void Log(Exception ex)
        {
            _log.Error(ex.Message, ex);
        }
    }
}
