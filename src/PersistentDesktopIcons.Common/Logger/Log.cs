﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace PersistentDesktopIcons.Common.Logger
{
    public static class Log
    {
        public static void WriteLine(string message)
        {
            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
#if DEBUG
            Debug.WriteLine("{0}: {1}", timestamp, message);
#else
            var logWriter = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt");

            using (logWriter)
            {
                logWriter.WriteLine("{0}: {1}", timestamp, message);
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
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", string.Empty);
            }
        }
    }
}