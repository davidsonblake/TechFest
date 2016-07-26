using System;
using System.Linq;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using TechFest.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace TechFest.UWP.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            if(e.NewElement == null)
                return;
            
            Control.Background = new SolidColorBrush(Windows.UI.Colors.Black);

            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/SessionsBackground.png"));
            Control.Background = brush;

            var headerTemplate = App.Current.Resources.SingleOrDefault(x => x.Key.ToString() == "PanoramaItemHeader").Value as Windows.UI.Xaml.DataTemplate;
            if (headerTemplate != null)
            {
               Control.HeaderTemplate = headerTemplate;
            }
            
            base.OnElementChanged(e);
        }
    }
}