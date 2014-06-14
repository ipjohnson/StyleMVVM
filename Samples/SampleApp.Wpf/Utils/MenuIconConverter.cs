using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Samples.Wpf.Utils
{

    [ValueConversion(typeof(Uri), typeof(Image))]
    public class MenuIconConverter : IValueConverter
    {
        /// <summary>
        /// Convert string to Image
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var uri = value as Uri;

            if (uri == null)
            {
                return Binding.DoNothing;
            }

            var img = new Image
                      {
                          Width = 16,
                          Height = 16
                      };

            var bmp = new BitmapImage(uri);
            img.Source = bmp;
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("Cannot convert from an image to an Uri");
        }
    }
}
