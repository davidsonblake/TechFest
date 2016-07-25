using BigTed;
using TechFest.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ActivityIndicator), typeof(CustomActivitiyIndicatorRenderer))]
namespace TechFest.iOS
{
    public class CustomActivitiyIndicatorRenderer : ActivityIndicatorRenderer
    {
        public CustomActivitiyIndicatorRenderer()
        {
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRunning")
            {
                if (this.Element.IsRunning)
                {
					BTProgressHUD.Show(maskType: ProgressHUD.MaskType.Gradient);
                }
                else
                {
                    BTProgressHUD.Dismiss();
                }
            }
            else
            {
                base.OnElementPropertyChanged(sender, e);
            }
        }
    }
}