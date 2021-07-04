using System.Diagnostics;
using System.Globalization;

namespace Popupnik.Common.Utils.Logging
{
    public static class Logger
    {
        public static bool IsInitialized
        {
            get
            {
                return true;
            }
        }

        public static void Initialize()
        {
        }

        public static void WriteLine(string message)
        {
            WriteLineFormat(message, new object[0]);
        }

        public static void WriteLineFormat(string format, params object[] args)
        {
            Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
        }

        public static void Deinitialize()
        {
            Trace.Flush();
        }
    }
}