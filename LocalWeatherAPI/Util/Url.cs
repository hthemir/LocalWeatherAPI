using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Url
    {
        public static string baseURL
        {
            get
            {
                return "http://api.apixu.com/v1";
            }
            set { }
        }
        public static string currentWeather
        {
            get
            {
                return "/current.json";
            }
            set { }
        }
        public static string key
        {
            get
            {
                return "?key=2356675b3110466ab9c121508171807";
            }
            set { }
        }

        public static string getCurrentWeatherByLatitudeLongitude(double latitude, double longitude)
        {
            return baseURL + currentWeather + key + "&q=" + latitude + "," + longitude;
        }

        public static string getCurrentWeatherByCity(string cityName)
        {
            return baseURL + currentWeather + key + "&q=" + cityName;
        }
    }
}
