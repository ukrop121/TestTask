using log4net;
using log4net.Config;

namespace MyChudoFrame
{
    public static class Logger
    {
        private static ILog _log = LogManager.GetLogger("LOGGER");

        public static ILog Log
        {
            get;
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public static void Info(string str)
        {
            _log.Info(str);
        }
    }
}
