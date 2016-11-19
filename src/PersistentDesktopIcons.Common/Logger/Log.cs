using System;
using System.Diagnostics;
using System.IO;

namespace PersistentDesktopIcons.Common.Logger
{
    public static class Log
    {
        public static void WriteLine(string log)
        {
#if DEBUG
            Debug.WriteLine(log);
#else
            var logWriter = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");

            using (logWriter)
            {
                logWriter.WriteLine(log);
            }
#endif
        }

        public static void WriteLine(string log, params object[] arg)
        {
#if DEBUG
            Debug.WriteLine(log, arg);
#else
            var logWriter = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");

            using (logWriter)
            {
                logWriter.WriteLine(log, arg);
            }
#endif
        }

        public static void Clear()
        {
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");
        }
    }
}