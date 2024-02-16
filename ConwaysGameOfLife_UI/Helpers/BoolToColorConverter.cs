using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLife_UI.Helpers
{
    internal class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Komische Konstruktion namens "Pattern Matching mit Type und Property" aus C#7, habs noch nicht 100%ig verstanden aber funktioniert fürs erste.
            if (value is bool boolValue && boolValue)
                return Brushes.Black;
            else 
                return Brushes.White;
        }

        //Fragwürdig ob nützlich oder nicht, Interface benötigt es aber
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ConvertBack is not supported for BoolToColorConverter, due to lack of sense in converting a color to a boolean.");
        }
    }
}
