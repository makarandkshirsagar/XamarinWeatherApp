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

        
    }
}
