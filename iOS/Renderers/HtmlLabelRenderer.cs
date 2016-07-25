using Foundation;
using TechFest.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Label), typeof(HtmlLabelRenderer))]

namespace TechFest.iOS
{
    public class HtmlLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var text = Control.Text.AsAttributedString(NSDocumentType.HTML);
            Control.AttributedText = new UITextView { AttributedText = text }.AttributedText;
            Control.TextColor = e.NewElement.TextColor.ToUIColor();
            if (!string.IsNullOrEmpty(e.NewElement.FontFamily))
            {
                Control.Font = UIFont.FromName(e.NewElement.FontFamily, (System.nfloat)e.NewElement.FontSize);
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