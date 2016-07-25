using System;
using TechFest.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ListView), typeof(TransparentListViewRenderer))]
namespace TechFest.Droid
{
	public class TransparentListViewRenderer : ListViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
		{
			base.OnElementChanged(e);


			if (Control != null) {
				Control.SetSelector(Android.Resource.Color.Black);
				Control.CacheColorHint = Android.Graphics.Color.Pink;
			}
		}

	}
}

