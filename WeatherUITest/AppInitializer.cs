using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace WeatherUITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android.ApkFile("E:/XamarinProjects/WeatherApp/WeatherApp/WeatherApp.Android/bin/Debug/com.companyname.WeatherApp.apk")//@"E:\XamarinProjects\WeatherApp\WeatherApp\WeatherApp.Android\bin\Debug\com.companyname.WeatherApp.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

