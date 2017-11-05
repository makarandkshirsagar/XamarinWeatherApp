using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Business;
using WeatherApp.Model;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //Set the default binding to a default object for now
            this.BindingContext = new WeatherDetails();
            
        }

        private async void BtnWeather_Click(object sender, EventArgs e)
        {
            WeatherDetails weather;
            
            try
            {
                //Passing hardcoded zip code to function
                //This can be made generic in future to get the input from UI
                weather = await Core.GetWeatherAsync("10001");

                if (weather != null)
                {
                    weather.CustomTemperature = "The temperature in New York was "+weather.Temperature +"("+weather.Celsius+" c) at "+weather.Sunrise;
                    this.BindingContext = weather;
                }
            }
            catch (Exception ex) 
            {

                throw;
            }         
        }
    }
}
