using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Business;
using Xamarin.Forms;
using System.ComponentModel;
namespace WeatherApp.Model
{
    public class WeatherDetails : INotifyPropertyChanged
    {
        public string Title { get; set; }
        public string Temperature { get; set; }
        public string Celsius { get; set; }
        public string Wind { get; set; }
        public string Humidity { get; set; }
        public string Visibility { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string CustomTemperature { get; set; }
        public System.Windows.Input.ICommand GetWeatherCommand { get; private set; }

        public WeatherDetails()
        {
            
            //Because labels bind to these values, set them to an empty string to
            //ensure that the label appears on all platforms by default.
            this.Title = " ";
            this.Temperature = " ";
            this.Wind = " ";
            this.Humidity = " ";
            this.Visibility = " ";
            this.Sunrise = " ";
            this.Sunset = " ";
            this.CustomTemperature = " ";
            this.Celsius = " ";

            GetWeatherCommand = new Command(BtnWeather_Click);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void BtnWeather_Click()
        {
            WeatherDetails weather;

            try
            {
                //Passing hardcoded zip code to function
                //This can be made generic in future to get the input from UI
                weather = await Core.GetWeatherAsync("10001");

                if (weather != null)
                {
                    this.CustomTemperature = "The temperature in New York was " + weather.Temperature + "(" + weather.Celsius + " c) at " + weather.Sunrise;
                    //this.BindingContext = weather;
                }

                OnPropertyChanged("CustomTemperature");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
