using System;
using System.ComponentModel;
using System.Diagnostics;
using TechFest.Effects;
using TechFest.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("TechFest")]
[assembly: ExportEffect(typeof(ShadowEffect), "ShadowEffect")]
namespace TechFest.iOS
{

	public class ShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			UpdateSize();
			UpdateColor();
			UpdateOpacity();
		}

		protected override void OnDetached()
		{
			Container.Layer.ShadowOpacity = 0;
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			Debug.WriteLine(args.PropertyName);
			if (args.PropertyName == ViewEffects.HasShadowProperty.PropertyName) {
				UpdateOpacity();
			} else if (args.PropertyName == ViewEffects.ShadowColorProperty.PropertyName) {
				UpdateColor();
			} else if (args.PropertyName == ViewEffects.ShadowSizeProperty.PropertyName) {
				UpdateSize();
			}
		}

		private void UpdateOpacity()
		{
			Container.Layer.ShadowOpacity = ViewEffects.GetHasShadow(Element) ? 1 : 0;
		}

		private void UpdateColor()
		{
			var color = ViewEffects.GetShadowColor(Element);
			Container.Layer.ShadowColor = color.ToCGColor();
		}

		private void UpdateSize()
		{
			Container.Layer.ShadowRadius = (nfloat)ViewEffects.GetShadowSize(Element);
		}
	}
}

