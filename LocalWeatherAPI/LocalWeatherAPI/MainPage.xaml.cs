using Model;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Util;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace LocalWeatherAPI
{
    public sealed partial class MainPage : Page
    {
        Geoposition position;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //prepare map
            var locator = new Geolocator();
            locator.DesiredAccuracyInMeters = 50;
            position = await locator.GetGeopositionAsync();
            await map.TrySetViewAsync(position.Coordinate.Point, 18);

            //get data
            string url = Url.getCurrentWeatherByLatitudeLongitude(position.Coordinate.Point.Position.Latitude, position.Coordinate.Point.Position.Longitude);
            var data = await Client.getData<WeatherModel>(url);

            //set data or error
            if (data is WeatherModel)
            {
                WeatherModel wm = data as WeatherModel;
                img.Source = wm.current.condition.image;
                txtCondition.Text = wm.current.condition.text;
                txtCurrentTempC.Text = wm.current.temp_c.ToString() + "°";
            }
            else
            {
                Error error = data as Error;
                txtError.Text = error.message;
            }
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapServiceToken = "ptkhy9lI9bMSTaeuoufZpkAs4MpZb8v";
        }
    }
}
