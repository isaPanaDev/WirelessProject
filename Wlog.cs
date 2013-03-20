using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using log4net;

namespace WirelessProject
{
    public static class Wlog
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static ILog Configure()
        {
            //log4net.Config.XmlConfigurator.Configure(new FileInfo("WirelessProject.exe.log4net"));
            log4net.Config.XmlConfigurator.Configure();
            return log4net.LogManager.GetLogger("user");
        }

        public static string FileName()
        {
            log4net.Repository.ILoggerRepository RootRep;
            RootRep = log4net.LogManager.GetRepository();

            foreach (log4net.Appender.IAppender iApp in RootRep.GetAppenders())
            {
                if (iApp.Name.CompareTo("CsvFileAppender") == 0 && iApp is log4net.Appender.FileAppender)
                {
                    log4net.Appender.FileAppender fApp = (log4net.Appender.FileAppender)iApp;

                    return fApp.File;
                }
                return null;
            }
            return null;
        }
    }
}
