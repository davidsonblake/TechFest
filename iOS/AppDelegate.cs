using FFImageLoading.Transformations;
using Foundation;
using HockeyApp.iOS;
using UIKit;
using XamSvg.XamForms.iOS;

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
            FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
            var ignore = new CircleTransformation();

            global::Xamarin.Forms.Forms.Init();
			SvgImageRenderer.InitializeForms();
			LoadApplication(new App());

            UITabBar.Appearance.BackgroundColor = UIColor.FromRGB(50, 78, 96);
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(50, 78, 96); //bar background
			UINavigationBar.Appearance.TintColor = UIColor.FromRGB(37,53,64); //Tint color of button items
			UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes() {
				Font = UIFont.FromName("AvenirNex-Bold", 18),
				ForegroundColor = UIColor.FromRGB(29, 45, 55)
			};

			UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent,false);

            return base.FinishedLaunching(app, options);
        }
    }
}