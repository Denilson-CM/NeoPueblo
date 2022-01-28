using Android.Content.Res;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeoPueblo
{
    public partial class App : Application
    {
      
        
        public static int val;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.Red);
            
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
