using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;
using TechFest.UWP.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Label), typeof(CustomLabelRenderer))]
namespace TechFest.UWP.Renderers
{

    public class CustomLabelRenderer : LabelRenderer
    {
        private const string ElementA = "A";
        private const string ElementB = "B";
        private const string ElementBr = "BR";
        private const string ElementEm = "EM";
        private const string ElementI = "I";
        private const string ElementP = "P";
        private const string ElementStrong = "STRONG";
        private const string ElementU = "U";
        private const string ElementUl = "UL";
        private const string ElementLi = "LI";
        private const string ElementDiv = "DIV";

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if(Control == null)
                return;
            
            this.Control.TextWrapping = TextWrapping.WrapWholeWords;
            UpdateText();
        }

        private void UpdateText()
        {
            if (string.IsNullOrEmpty(Element?.Text)) return;

            string text = Element.Text;

            // Just incase we are not given text with elements.
            text = text.Replace("<br>", @"<br />");
            string modifiedText = string.Format("<div>{0}</div>", text);

            // reset the text because we will add to it.
            Control.Inlines.Clear();
            try
            {
                var element = XElement.Parse(modifiedText);
                ParseText(element, Control.Inlines);
            }
            catch (Exception)
            {
                // if anything goes wrong just show the html
                Control.Text = text;
            }
        }


        private static void ParseText(XElement element, InlineCollection inlines)
        {
            if (element == null) return;

            InlineCollection currentInlines = inlines;
            var elementName = element.Name.ToString().ToUpper();
            switch (elementName)
            {
                case ElementA:
                    var link = new Hyperlink();
                    inlines.Add(link);
                    currentInlines = link.Inlines;
                    break;
                case ElementB:
                case ElementStrong:
                    var bold = new Bold();
                    inlines.Add(bold);
                    currentInlines = bold.Inlines;
                    break;
                case ElementI:
                case ElementEm:
                    var italic = new Italic();
                    inlines.Add(italic);
                    currentInlines = italic.Inlines;
                    break;
                case ElementU:
                    var underline = new Underline();
                    inlines.Add(underline);
                    currentInlines = underline.Inlines;
                    break;
                case ElementBr:
                    inlines.Add(new LineBreak());
                    break;
                case ElementP:
                    // Add two line breaks, one for the current text and the second for the gap.
                    if (AddLineBreakIfNeeded(inlines))
                    {
                        inlines.Add(new LineBreak());
                    }

                    Windows.UI.Xaml.Documents.Span paragraphSpan = new Windows.UI.Xaml.Documents.Span();
                    inlines.Add(paragraphSpan);
                    currentInlines = paragraphSpan.Inlines;
                    break;
                case ElementLi:
                    inlines.Add(new LineBreak());
                    inlines.Add(new Run { Text = " • " });
                    break;
                case ElementUl:
                case ElementDiv:
                    AddLineBreakIfNeeded(inlines);
                    Windows.UI.Xaml.Documents.Span divSpan = new Windows.UI.Xaml.Documents.Span();
                    inlines.Add(divSpan);
                    currentInlines = divSpan.Inlines;
                    break;
            }
            foreach (var node in element.Nodes())
            {
                XText textElement = node as XText;
                if (textElement != null)
                {
                    currentInlines.Add(new Run { Text = textElement.Value });
                }
                else
                {
                    ParseText(node as XElement, currentInlines);
                }
            }
            // Add newlines for paragraph tags
            if (elementName == ElementP)
            {
                currentInlines.Add(new LineBreak());
            }
        }

        /// <summary>
        /// Check if the InlineCollection contains a LineBreak as the last item.
        /// </summary>
        /// <param name="inlines"></param>
        /// <returns></returns>
        private static bool AddLineBreakIfNeeded(InlineCollection inlines)
        {
            if (inlines.Count > 0)
            {
                var lastInline = inlines[inlines.Count - 1];
                while ((lastInline is Windows.UI.Xaml.Documents.Span))
                {
                    var span = (Windows.UI.Xaml.Documents.Span)lastInline;
                    if (span.Inlines.Count > 0)
                    {
                        lastInline = span.Inlines[span.Inlines.Count - 1];
                    }
                }
                if (!(lastInline is LineBreak))
                {
                    inlines.Add(new LineBreak());
                    return true;
                }
            }
            return false;
        }
    }
}