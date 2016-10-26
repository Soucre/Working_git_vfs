using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SwipeJob.Utility.Exceptions;

namespace SwipeJob.Utility
{
    public static class Utils
    {
        public static int UnixTimeStamp(this DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public static DateTime ToDate(this int second)
        {
            return (new DateTime(1970, 1, 1)).AddSeconds(second);
        }

        public static void CheckNullOrEmpty(List<string> argNames, params object[] args)
        {
            if (args == null)
            {
                throw new UserException("ARGUMENT_IS_REQUIRED=>" + argNames[0]);
            }

            for (int i = 0; i < args.Length; i++)
            {
                object arg = args[i];

                if (arg == null)
                {
                    throw new UserException("ARGUMENT_IS_REQUIRED=>" + argNames[i]);
                }

                if (arg is string && string.IsNullOrWhiteSpace(arg.ToString()))
                {
                    throw new UserException("ARGUMENT_IS_REQUIRED=>" + argNames[i]);
                }
            }
        }

        public static void CheckNoneZero(List<string> argNames, params decimal[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == 0)
                {
                    throw new UserException("ARGUMENT_IS_REQUIRED=>" + argNames[i]);
                }
            }
        }

        public static void CheckNoneZero(List<string> argNames, params double[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == 0)
                {
                    throw new UserException("ARGUMENT_IS_REQUIRED=>" + argNames[i]);
                }
            }
        }

        public static void CheckNoneZero(List<string> argNames, params Guid[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == Guid.Empty)
                {
                    throw new UserException("ARGUMENT_IS_REQUIRED=>" + argNames[i]);
                }
            }
        }

        public static bool IsEmail(string email)
        {
            const string emailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" +
                                        @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." +
                                        @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" +
                                        @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, emailPattern);
        }

        public static int GetAge(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static DateTime GetStartOfDay(DateTime date)
        {
            return DateTime.ParseExact(date.ToString("yyyyMMdd"), "yyyyMMdd", null);
        }

        public static DateTime GetEndOfDay(DateTime date)
        {
            return DateTime.ParseExact(date.ToString("yyyyMMdd"), "yyyyMMdd", null).AddDays(1).AddTicks(-1);
        }

        public static TimeZoneInfo OlsonTimeZoneToTimeZoneInfo(string olsonTimeZoneId)
        {
            string windowsTimeZoneId = string.Empty;
            using (TextReader file = new StreamReader(typeof(Utils).Assembly.GetManifestResourceStream("Hovit.Utility.Data.OslonToWindowsTimeZone.json")))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject json = (JObject)JToken.ReadFrom(reader);
                    windowsTimeZoneId = json.Value<string>(olsonTimeZoneId);
                }
            }

            var windowsTimeZone = default(TimeZoneInfo);
            try { windowsTimeZone = TimeZoneInfo.FindSystemTimeZoneById(windowsTimeZoneId); }
            catch (TimeZoneNotFoundException) { }
            catch (InvalidTimeZoneException) { }
            return windowsTimeZone;
        }
    }
}
