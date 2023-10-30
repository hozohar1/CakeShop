using BO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace PL
{
    class ConvertImagPathToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string imageRelatuveName = (string)value;
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelatuveName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }
            catch (Exception ex)
            {
                string imageRelatuveName = @"\img\IMG_FAILURE.jpg";
                string currentDir = Environment.CurrentDirectory[..^4];
                string imageFullName = currentDir + imageRelatuveName;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imageFullName));
                return bitmapImage;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class ConvertProg : IValueConverter
    {
        static readonly Random rand = new Random();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            OrderStatus orderStatus = (OrderStatus)value;
            switch (orderStatus)
            {
                case OrderStatus.Ordered:
                    return rand.Next(0, 25);
                case OrderStatus.Shipped:
                    return rand.Next(25, 50);
                case OrderStatus.Delivered:
                    return 100;
                default:
                    return 0;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class StatusToBackgroundConverter : IValueConverter
    {
        //convert from source property type to target property type
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            BO.OrderStatus stingValue = (BO.OrderStatus)value;
            if (stingValue == BO.OrderStatus.Ordered)
            {

                return "#FFD9EC";
            }
            else if (stingValue == BO.OrderStatus.Delivered)
            {
                return "#FF3399";
            }
            else if (stingValue == BO.OrderStatus.Shipped)
            {
                return "#FF75BA";
            }

            else
                return null;
        }
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}