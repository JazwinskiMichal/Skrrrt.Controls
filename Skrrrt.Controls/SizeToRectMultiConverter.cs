using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Media3D;

namespace Skrrrt.Controls
{
    class SizeToRectMultiConverter : MarkupExtension, IMultiValueConverter
    {
        private Rect _defaultRect = new Rect(0, 0, 300, 100);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.All(x => x != null))
            {
                if (values.FirstOrDefault().GetType().FullName == "MS.Internal.NamedObject")
                    return _defaultRect;
                else
                {
                    double width = (double)values[0];
                    double height = (double)values[1];

                    return new Rect(0, 0, width, height);
                }
            }
            else
                return _defaultRect;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
