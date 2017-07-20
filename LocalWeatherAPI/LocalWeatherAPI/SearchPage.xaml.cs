using Model;
using Service;
using Util;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace LocalWeatherAPI
{
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) { }

        private void mapTapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void imgSearchTapped(object sender, TappedRoutedEventArgs e)
        {
            //set loading
            busyProgressRing.IsActive = true;

            //get data
            string url = Url.getCurrentWeatherByCity(txtSearch.Text);
            var data = await Client.getData<WeatherModel>(url);

            //remove loading
            busyProgressRing.IsActive = false;
            
            //set data or error
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

        private void txtSearchGotFocus(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
        }
    }
}
