using Weird.Data;
using Xamarin.Forms;

namespace Weird
{
    public class App : Application
    {
        public App(string dbPath)
        {
            var database = new WeirdDatabase(dbPath);

            MainPage = new NavigationPage(new QuestionPage(database));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}