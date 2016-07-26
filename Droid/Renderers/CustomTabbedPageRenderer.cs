using System;
using Android.App;
using Android.Graphics;
using Android.Widget;
using TechFest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace TechFest.Droid
{
	public class CustomTabbedPageRenderer : TabbedPageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{
			base.OnElementChanged(e);

			AddImageToBackground();
		}

		private void AddImageToBackground()
		{
			var view = new ImageView(this.Context);
			view.SetImageBitmap(BitmapFactory.DecodeStream(ResourceLoader.GetEmbeddedResourceStream(typeof(App).Assembly,"TechFest.Images.SessionsBackground.png")));

			AddView(view);
		}
	}
}

