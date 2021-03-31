using System;
using System.Collections.Generic;
using System.Text;

namespace Latchet.Domain.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToPersianFriendlyDisplay(this TimeSpan timeSpan, bool roundMinutes)
        {
            int hours = (int)timeSpan.TotalHours;
            int minutes = timeSpan.Minutes;

            if (roundMinutes)
            {
                minutes = (int)(Math.Ceiling(minutes / 5d) * 5);

                if (minutes > 55)
                {
                    hours++;
                    minutes = 0;
                }
            }

            if (hours > 0)
            {
                return minutes > 0
                    ? $"{hours} ساعت و {minutes} دقیقه"
                    : $"{hours} ساعت";
            }
            else if (minutes > 0)
            {
                return $"{minutes} دقیقه";
            }
            else
            {
                return null;
            }
        }
    }
}
