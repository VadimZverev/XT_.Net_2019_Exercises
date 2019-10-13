using log4net;
using log4net.Config;
using System;

namespace EPAM.Social_Network.Loggers
{
    public static class Logger
    {
        private static readonly ILog log
            = LogManager.GetLogger("LOGGER");

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public static void SendError(Exception ex, string customMessage)
        {
            var msg = ex.Message.Replace(Environment.NewLine, "");

            InitLogger();

            if (string.IsNullOrWhiteSpace(customMessage))
            {
                Log.Error(msg);
            }
            else
            {
                Log.Error($"{customMessage} {msg}");
            }
        }
        
        public static void SendFatalError(Exception ex, string customMessage)
        {
            var msg = ex.Message.Replace(Environment.NewLine, "");

            InitLogger();

            if (string.IsNullOrWhiteSpace(customMessage))
            {
                Log.Fatal(msg);
            }
            else
            {
                Log.Fatal($"{customMessage} {msg}");
            }
        }
    }
}
