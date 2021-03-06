﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using static WeatherApp.Model.ResponseResult;
using Newtonsoft.Json;

namespace WeatherApp.Business
{
    class Core
    {

        public static async Task<WeatherDetails> GetWeatherAsync(string zipCode)
        {
            //Sign up for a free API key at http://openweathermap.org/appid            
            string queryString = Helper.api + zipCode + "&appid=" + Helper.key;

            //RESPONSE
            //{"coord":{"lon":-74,"lat":40.75},"weather":[{"id":800,"main":"Clear","description":"clear sky","icon":"01n"}],"base":"stations","main":{"temp":283.3,"pressure":1025,"humidity":53,"temp_min":281.15,"temp_max":285.15},"visibility":16093,"wind":{"speed":4.1,"deg":340,"gust":7.7},"clouds":{"all":1},"dt":1509771360,"sys":{"type":1,"id":2121,"message":0.0106,"country":"US","sunrise":1509795025,"sunset":1509832089},"id":0,"name":"New York","cod":200}
            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);
            
            #region Commented
            //IEnumerable<RootObject> results = (IEnumerable<RootObject>)Task.Run(async () => { return await DataService.GetResult(queryString); });
            //Synchronous call
            //var results = DataService.GetResult(queryString);
            //var results = await DataService.getDataFromService(queryString);
            #endregion
            if (results[Helper.weather] != null)
            {
                WeatherDetails weather = new WeatherDetails();
                
                weather.Title = (string)results[Helper.name];
                weather.Temperature = (string)results[Helper.main][Helper.temp] + " f";
                double cel = 0;
                //T(°C) = (T(°F) - 32) / 1.8
                try
                {
                    string farStr = (results[Helper.main][Helper.temp]).ToString();
                    double far = Convert.ToDouble(farStr);
                    cel = (far - 32)/(1.8);                    
                }
                catch (Exception ex)
                {
                    ExceptionFileWriter.ToLogUnhandledException(ex);
                    //throw ex;
                }                
                weather.Celsius = String.Format("{0:0.00}", cel);
                weather.Wind = (string)results[Helper.wind][Helper.speed] + " "+Helper.mph;
                weather.Humidity = (string)results[Helper.main][Helper.humidity] + " %";
                weather.Visibility = (string)results[Helper.weather][0][Helper.main];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results[Helper.sys][Helper.sunrise]);
                DateTime sunset = time.AddSeconds((double)results[Helper.sys][Helper.sunset]);
                //weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunrise = sunrise.TimeOfDay.ToString()+ " "+Helper.UTC;
                weather.Sunset = sunset.ToString() + " "+Helper.UTC;                
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}
