using System;
using System.Diagnostics;
using System.IO;

namespace PersistentDesktopIcons.Common.Logger
{
    public static class Log
    {
        public static void WriteLine(string message)
        {
#if DEBUG
            Debug.WriteLine(message);
#else
            var logWriter = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");

            using (logWriter)
            {
                logWriter.WriteLine(message);
            }
#endif
        }

        public static void WriteLine(string format, params object[] args)
        {
#if DEBUG
            Debug.WriteLine(format, args);
#else
            var logWriter = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");

            using (logWriter)
            {
                logWriter.WriteLine(format, args);
            }
#endif
        }

        public static void ClearIfLargerThan(long maxSizeInKiloBytes)
        {
            var logPath = AppDomain.CurrentDomain.BaseDirectory + "Log.txt";

            if (File.Exists(logPath))
            {
                var logSize = new FileInfo(logPath).Length;

                if (logSize > maxSizeInKiloBytes * 1000)
                {
                    Clear();
                }
            }
        }

        public static void Clear()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Log.txt"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");
            }
        }
    }
}