using System;
using TechFest.iOS;
using TechFest.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamSvg;
using XamSvg.XamForms;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace TechFest.iOS
{
	public class CustomTabbedPageRenderer : TabbedRenderer
	{
		UITabBarController tabbedController;
		bool isInitialized = false;

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null) {
				tabbedController = (UITabBarController)ViewController;
			}
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (!isInitialized) {

				this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(SvgFactory.FromBundle("res:Images.Menu",20f), UIBarButtonItemStyle.Plain, null), false);

				tabbedController.ViewControllers[0].TabBarItem.Image = SvgFactory.FromBundle("res:Images.speaker", 20f);
				tabbedController.ViewControllers[1].TabBarItem.Image = SvgFactory.FromBundle("res:Images.code", 20f);
				tabbedController.ViewControllers[2].TabBarItem.Image = SvgFactory.FromBundle("res:Images.moneybag", 20f);

				isInitialized = true;
			}
		}
	}
}

