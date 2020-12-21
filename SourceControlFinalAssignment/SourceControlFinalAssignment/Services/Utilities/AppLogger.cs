using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SourceControlFinalAssignment.Services.Utilities
{
    public class AppLogger
    {
        private static Logger logger;
        //private static Logger dbLogger;

        public static Logger GetLogger(string theLogger)
        {
            if (AppLogger.logger == null)
                AppLogger.logger = LogManager.GetLogger(theLogger);

            return AppLogger.logger;
        }

        //public static Logger GetDBLogger()
        //{
        //    if (AppLogger.dbLogger == null)
        //        AppLogger.dbLogger = LogManager.GetLogger("databaseLoggerRules");

        //    return AppLogger.dbLogger;
        //}
    }
}