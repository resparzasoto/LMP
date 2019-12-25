using System;
using System.Globalization;
using Xamarin.Forms;

namespace LMP
{
    public class TeamColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            var team = (string)value;
            Color colorTeam;

            switch (team)
            {
                case "Águilas de Mexicali":
                    colorTeam = Color.Red;
                    break;
                case "Algodoneros de Guasave":
                    colorTeam = Color.Blue;
                    break;
                case "Cañeros de Los Mochis":
                    colorTeam = Color.DarkGreen;
                    break;
                case "Charros de Jalisco":
                    colorTeam = Color.DeepSkyBlue;
                    break;
                case "Mayos de Navojoa":
                    colorTeam = Color.DarkGoldenrod;
                    break;
                case "Naranjeros de Hermosillo":
                    colorTeam = Color.Orange;
                    break;
                case "Sultanes de Monterrey":
                    colorTeam = Color.DodgerBlue;
                    break;
                case "Tomateros de Culiacán":
                    colorTeam = Color.IndianRed;
                    break;
                case "Venados de Mazatlán":
                    colorTeam = Color.PaleVioletRed;
                    break;
                case "Yaquis de Ciudad Obregón":
                    colorTeam = Color.Black;
                    break;
                default:
                    colorTeam = Color.Transparent;
                    break;
            }

            return colorTeam;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
