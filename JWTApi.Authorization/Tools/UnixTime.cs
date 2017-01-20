using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace JWTApi.Authorization.Tools
{
    public static class UnixTime
    {

        public static long ChangeTS(long ts)
        {
            if (ts >= 1380000000)
            {
                return 0;
            }
            else
            {
                return ts;
            }
        }

        public static DateTime GetDateTime(DateTime? aDateTime)
        {
            if (aDateTime.HasValue)
            {
                return aDateTime.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime CutOffMillisecond(DateTime dt)
        {
            return new DateTime(dt.Ticks - (dt.Ticks % TimeSpan.TicksPerSecond), dt.Kind);
        }
        /// <summary>
        /// UTC 1970-1-1 00:00:00
        /// </summary>
        public static readonly DateTime UTCUnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a DateTime to unix time. Unix time is the number of seconds 
        /// between 1970-1-1 0:0:0.0 (unix epoch) and the time (UTC).
        /// </summary>
        /// <param name="time">要转换为unix时间的时间,必须是格林威治时间</param>
        /// <returns>The number of seconds between Unix epoch and the input time</returns>
        public static Int64 ToUnixTime(DateTime time)
        {
            return (Int64)(time - UTCUnixEpoch).TotalSeconds;
        }

        /// <summary>
        /// 时间戳专用
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static Int64 ToUnixTimes(DateTime time)
        {
            return (Int64)(time - UTCUnixEpoch).TotalMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aTime"></param>
        /// <param name="aKind">时间类型</param>
        /// <returns></returns>
        public static Int64 ToUnixTime(DateTime aTime, DateTimeKind aKind)
        {
            if (aKind == DateTimeKind.Utc)
            {
                return (Int64)(aTime - UTCUnixEpoch).TotalSeconds;
            }
            else
            {
                return (Int64)(aTime.ToUniversalTime() - UTCUnixEpoch).TotalSeconds;
            }
        }

        /// <summary>
        /// Converts a long representation of a unix time into a DateTime. Unix time is 
        /// the number of seconds between 1970-1-1 0:0:0.0 (unix epoch) and the time (UTC).
        /// </summary>
        /// <param name="unixTime">The number of seconds since Unix epoch (must be >= 0)</param>
        /// <returns>A UTC DateTime object representing the unix time</returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "unix", Justification = "UNIX is a domain term")]
        public static DateTime FromUnixTime(long unixTime)
        {
            //if (unixTime < 0)
            //    throw new ArgumentOutOfRangeException("unixTime");

            return UTCUnixEpoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// 时间戳专用
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "unix", Justification = "UNIX is a domain term")]
        public static DateTime FromUnixTimes(long unixTime)
        {
            //if (unixTime < 0)
            //    throw new ArgumentOutOfRangeException("unixTime");

            return UTCUnixEpoch.AddMilliseconds(unixTime);
        }

    }
}