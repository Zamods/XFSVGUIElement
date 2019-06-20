using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XFSVGUIElement
{
    public class StringToSVGStyleConverter : IValueConverter
    {
        #region IValueConverter implementation

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var svgPaintStyleString = value;

            string paintStyle;

            switch (svgPaintStyleString)
            { 
                case PaintStyle.Fill:
                    paintStyle = "Fill";
                    break;

                case PaintStyle.FillandStroke:
                    paintStyle = "FillandStroke";
                    break;

                case PaintStyle.Stroke:
                    paintStyle = "Stroke";
                    break;

                default:
                    paintStyle = "Fill";
                    break;

            }

            return paintStyle;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var svgPaintStyleString = (string)value;

            return svgPaintStyleString;
        }

        #endregion
    }
}
