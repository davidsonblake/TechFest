using TechFest.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(CustomActivitiyIndicatorRenderer))]
namespace TechFest.iOS.Renderers
{
    public class CustomActivitiyIndicatorRenderer : ActivityIndicatorRenderer
    {
		protected override void OnElementChanged(ElementChangedEventArgs<ActivityIndicator> e)
		{
			base.OnElementChanged(e);

			Control.ActivityIndicatorViewStyle = UIKit.UIActivityIndicatorViewStyle.WhiteLarge;
		}

    }
}