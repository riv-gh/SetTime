using System;
using System.Runtime.InteropServices;
using System.Net;

namespace SetTime
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            WebClient wc = new WebClient();
            wc.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            string time_str = wc.DownloadString(new Uri("http://clock.time.in.ua/"));
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(long.Parse(time_str));
            SystemTime updatedTime = new SystemTime();
            updatedTime.Year = (ushort)dt.Year;
            updatedTime.Month = (ushort)dt.Month;
            updatedTime.Day = (ushort)dt.Day;
            updatedTime.Hour = (ushort)dt.Hour;
            updatedTime.Minute = (ushort)dt.Minute;
            updatedTime.Second = (ushort)dt.Second;

            Win32SetSystemTime(ref updatedTime);
        }

        public struct SystemTime
        {
            public ushort Year;
            public ushort Month;
            public ushort DayOfWeek;
            public ushort Day;
            public ushort Hour;
            public ushort Minute;
            public ushort Second;
            public ushort Millisecond;
        };

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime sysTime);
    }
}
