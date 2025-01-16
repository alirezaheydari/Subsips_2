namespace Repository.Helper;

public static class DayOfWeekHelper
{
    public static string GetPersianDisplayName(this DayOfWeek dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Saturday:
                return "شنبه";
            case DayOfWeek.Sunday:
                return "یک شنبه";
            case DayOfWeek.Monday:
                return "دوشنبه";
            case DayOfWeek.Tuesday:
                return "سه شنبه";
            case DayOfWeek.Wednesday:
                return "چهارشنبه";
            case DayOfWeek.Thursday:
                return "پنج شنبه";
            case DayOfWeek.Friday:
                return "جمعه";
        }


        return string.Empty;
    }
}
