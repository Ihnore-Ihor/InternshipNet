using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace InternshipNet.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string status = value as string;
            if (string.IsNullOrEmpty(status))
            {
                return Brushes.Transparent; 
            }

            switch (status.ToLower())
            {
                case "accepted":
                    return new SolidColorBrush(Colors.LightGreen);
                case "pending":
                    return new SolidColorBrush(Colors.LightYellow);
                case "rejected":
                    return new SolidColorBrush(Colors.LightCoral);
                default:
                    return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
