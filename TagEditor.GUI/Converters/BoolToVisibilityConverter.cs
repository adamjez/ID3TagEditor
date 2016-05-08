using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TagEditor.GUI.Converters
{
    /// <summary>
    /// Converts bool value to Visibility
    /// If given parameter is "I" like Inverse given value is inverted
    /// </summary>
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (bool)value;
            var param = (string)parameter;

            // Inverse
            if (param != null && param == "I")
                val = !val;

            return val ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
