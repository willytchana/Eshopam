using System;

namespace Eshopam.Models
{
    public static class Extensions
    {

        public static string ToRelativeFormat(this DateTime dateTime)
        {
            var now = DateTime.Now;
            var timeDifference = now - dateTime;
            var daySuffix = GetDaySuffix(dateTime);

            if (now.Year == dateTime.Year)
            {
                // exclude year

                if (now.Month == dateTime.Month)
                {
                    // exclude month

                    if (now.Day == dateTime.Day)
                    {
                        // exclude day

                        if (timeDifference.Hours < 24)
                        {
                            // display as hours

                            if (timeDifference.Hours < 1)
                            {
                                // display as minutes

                                if (timeDifference.Minutes < 1)
                                {
                                    // display as seconds
                                    if (timeDifference.Seconds <= 1)
                                        return "Just now";

                                    return timeDifference.Seconds + " secs ago";
                                }
                                if (timeDifference.Minutes == 1)
                                {
                                    return timeDifference.Minutes + " min ago";
                                }

                                // display as minutes
                                return timeDifference.Minutes + " mins ago";
                            }

                            // display as hours
                            if (timeDifference.Hours == 1)
                                return "1 hour ago";

                            return timeDifference.Hours + " hrs ago";
                        }
                    }

                    // display with year and month excluded
                    return dateTime.ToString($"dddd, d'{daySuffix}' 'at' h:mm tt");
                }

                // display with year excluded
                return dateTime.ToString($"dddd, MMMM d'{daySuffix}' 'at' h:mm tt");
            }

            // display with name of day excluded
            return dateTime.ToString($"MMMM d'{daySuffix}', yyyy 'at' h:mm tt");
        }


        #region HELPERS


        private static string GetDaySuffix(DateTime date)
        {
            return date.Day.ToOrdinal();
        }

        #endregion

        public static string ToOrdinal(this Int32 value)
        {
            var ordinal = "";

            switch (value)
            {
                case 1:
                case 21:
                case 31:
                    ordinal = "st";
                    break;
                case 2:
                case 22:
                    ordinal = "nd";
                    break;
                case 3:
                case 23:
                    ordinal = "rd";
                    break;
                default:
                    ordinal = "th";
                    break;
            }

            return ordinal;
        }
    }

}

