using CoreGraphics;
using Foundation;
using TechFest;
using TechFest.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HtmlLabel), typeof(HtmlLabelRenderer))]

namespace TechFest.iOS
{
    public class HtmlLabelRenderer : ViewRenderer
    {
		private UITextView _tv;
		
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            var view = (Label)Element;
            if (view == null) return;

            if (e.NewElement == null)
                return;

			if (string.IsNullOrEmpty(view.Text))
				return;

			if (_tv == null) {

				UITextView uilabelleftside = new UITextView(new CGRect(0, 0, Element.Width, Element.Height));
				var text = view.Text.AsAttributedString(NSDocumentType.HTML);
				uilabelleftside.AttributedText = text;
				uilabelleftside.Font = UIFont.SystemFontOfSize((float)view.FontSize);
				uilabelleftside.Editable = false;
				uilabelleftside.TextColor = view.TextColor.ToUIColor();
				uilabelleftside.ScrollEnabled = false;
				uilabelleftside.TintColor = UIColor.LightGray;
				uilabelleftside.TextContainer.LineFragmentPadding = 0f;
				uilabelleftside.TextContainerInset = new UIEdgeInsets(2, 0, 0, 0);

				// Setting the data detector types mask to capture all types of link-able data
				uilabelleftside.DataDetectorTypes = UIDataDetectorType.All;
				uilabelleftside.BackgroundColor = UIColor.Clear;

				if (!string.IsNullOrEmpty(view.FontFamily)) {
					uilabelleftside.Font = UIFont.FromName(view.FontFamily, (System.nfloat)view.FontSize);
				}

				// overriding Xamarin Forms Label and replace with our native control
				SetNativeControl(uilabelleftside);
				_tv = uilabelleftside;
			}
        }
    }

    public static class NSStringExtensions
    {
        public static NSAttributedString AsAttributedString(this string input, NSDocumentType docType)
        {
            NSError err = null;
            var rtn = new NSAttributedString(input, new NSAttributedStringDocumentAttributes { DocumentType = docType }, ref err);
            if (err == null)
            {
                return rtn;
            }
            throw new NSErrorException(err);
        }
    }
}