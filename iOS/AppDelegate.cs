using Foundation;
using HockeyApp.iOS;
using UIKit;

namespace TechFest.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var manager = BITHockeyManager.SharedHockeyManager;
            manager.Configure("5f4db5c730f145a9bc432f40e9f67798");
            manager.StartManager();

            global::Xamarin.Forms.Forms.Init();

            LoadApplication(new App());

            UITabBar.Appearance.BackgroundColor = UIColor.FromRGB(21, 33, 41);
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(21, 33, 41); //bar background
            UINavigationBar.Appearance.TintColor = UIColor.White; //Tint color of button items

            return base.FinishedLaunching(app, options);
        }
    }
}