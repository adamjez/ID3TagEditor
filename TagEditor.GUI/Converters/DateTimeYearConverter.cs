using System;
using Windows.UI.Xaml.Data;

namespace TagEditor.GUI.Converters
{
    public class DateTimeYearConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var year = (uint?)value;
            if (year != null)
            {
                return new DateTimeOffset(new DateTime((int)year,1,1));
            }

            return new DateTimeOffset();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var date = (DateTimeOffset)value;
            if (date.Year > 0)
            {
                return (uint?) date.Year;
            }

            return null;
        }
    }

}
