using Model;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Util;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace LocalWeatherAPI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e) { }

        private void mapTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void imgSearchTapped(object sender, TappedRoutedEventArgs e)
        {
            busyProgressRing.IsActive = true;
            string url = Url.getCurrentWeatherByCity(txtSearch.Text);
            var data = await Client.getData<WeatherModel>(url);
            busyProgressRing.IsActive = false;
            
            if (data is WeatherModel)
            {
                WeatherModel wm = data as WeatherModel;
                txtLocation.Text = wm.location.name + ", " + wm.location.region + ", " + wm.location.country;
                imgCondition.Source = wm.current.condition.image;
                txtTemperature.Text = wm.current.temp_c + "°";
                txtFeelsLike.Text = "Feels like: " + wm.current.feelslike_c + "°";
                txtWind.Text = "Wind: " + wm.current.wind_kph + "km/h";
                txtPrecipitation.Text = "Precipitation: " + wm.current.precip_mm + "mm";
                txtHumidity.Text = "Humidity: " + wm.current.humidity.ToString();
            }
            else
            {
                Error error = data as Error;
                txtError.Text = error.message;
            }
        }

        private void searchTapped(object sender, TappedRoutedEventArgs e) { }
    }
}
