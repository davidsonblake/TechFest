using System;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechFest.Helpers
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;

            if (Source == null)
            {
                return null;
            }

            var exists = assembly.GetManifestResourceNames().Any(res => Source == res);

            if (!exists)
                throw new ArgumentException("Resource not found: " + Source);

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source);

            return imageSource;
        }
    }
}
