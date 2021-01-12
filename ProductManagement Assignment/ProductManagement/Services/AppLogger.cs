using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Services
{
    public class AppLogger
    {
        private static Logger logger;

        public static Logger GetLogger(string theLogger)
        {
            if (AppLogger.logger == null)
                AppLogger.logger = LogManager.GetLogger(theLogger);

            return AppLogger.logger;
        }
    }
}