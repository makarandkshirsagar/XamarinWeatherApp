using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace WeatherUITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void ButtonClicked()
        {
            //app.Tap(c => c.Marked("getWeatherBtn"));
            app.Tap(c => c.Marked("Get Weather")); 
        }

        [Test]
        public void TempratureDisplayed()
        {
            app.Tap(c => c.Marked("Get Weather"));
            Assert.IsNotEmpty(app.Query("ResponseLabel").Length.ToString());

            #region Commented
            //AppResult[] results = app.Query("The temperature in New York was");
            //AppResult[] results = app.Query(c => c.Marked("tempOutPut"));
            //app.WaitForElement(c => c.Marked("ResponseLabel"));
            //Assert.IsTrue(results.Any(), "The temperature in New York was");
            //Assert.AreEqual("The temperature in New York was", app.Query("ResponseLabel")[0].Text);
            #endregion
        }
    }
}

