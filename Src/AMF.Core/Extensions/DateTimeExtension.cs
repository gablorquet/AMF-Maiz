using System;

namespace AMF.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static int AsAge(this DateTime date)
        {
            var today = DateTime.Today;

            var age = today.Year - date.Year;

            if (today.Date >= date.Date && today.Month >= date.Month)
                age++;

            return age;
        }
    }
}
