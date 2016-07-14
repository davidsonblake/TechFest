using System.Xml.Serialization;

namespace TechFest.Models
{
    [XmlRoot(ElementName = "field")]
    public class Speaker
    {
        [XmlElement(ElementName = "Link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "Company")]
        public string Company { get; set; }

        [XmlElement(ElementName = "Tagline")]
        public string Tagline { get; set; }

        [XmlElement(ElementName = "SortOrder")]
        public string SortOrder { get; set; }

        [XmlElement(ElementName = "Active")]
        public string Active { get; set; }

        [XmlElement(ElementName = "Featured")]
        public string Featured { get; set; }

        [XmlElement(ElementName = "Photo")]
        public string Photo { get; set; }

        [XmlElement(ElementName = "Bio")]
        public string Bio { get; set; }

        [XmlElement(ElementName = "Facebook")]
        public string Facebook { get; set; }

        [XmlElement(ElementName = "GooglePlus")]
        public string GooglePlus { get; set; }

        [XmlElement(ElementName = "Instagram")]
        public string Instagram { get; set; }

        [XmlElement(ElementName = "LinkedIn")]
        public string LinkedIn { get; set; }

        [XmlElement(ElementName = "Rss")]
        public string Rss { get; set; }

        [XmlElement(ElementName = "Twitter")]
        public string Twitter { get; set; }

        [XmlElement(ElementName = "YouTube")]
        public string YouTube { get; set; }

        [XmlElement(ElementName = "Flickr")]
        public string Flickr { get; set; }

        [XmlElement(ElementName = "Github")]
        public string Github { get; set; }

        [XmlElement(ElementName = "Foursquare")]
        public string Foursquare { get; set; }

        [XmlElement(ElementName = "Pinterest")]
        public string Pinterest { get; set; }

        [XmlElement(ElementName = "Dribbble")]
        public string Dribbble { get; set; }

        [XmlElement(ElementName = "MySpace")]
        public string MySpace { get; set; }

        [XmlElement(ElementName = "SlideShare")]
        public string SlideShare { get; set; }

        [XmlElement(ElementName = "SpeakerDeck")]
        public string SpeakerDeck { get; set; }

        [XmlElement(ElementName = "SpeakerRate")]
        public string SpeakerRate { get; set; }

        [XmlElement(ElementName = "SoundCloud")]
        public string SoundCloud { get; set; }

        [XmlElement(ElementName = "Tumblr")]
        public string Tumblr { get; set; }

        [XmlElement(ElementName = "StackOverflow")]
        public string StackOverflow { get; set; }

        [XmlElement(ElementName = "AboutMe")]
        public string AboutMe { get; set; }

        [XmlElement(ElementName = "BrandedMe")]
        public string BrandedMe { get; set; }

        [XmlElement(ElementName = "Mvp")]
        public string Mvp { get; set; }

        [XmlElement(ElementName = "XING")]
        public string XING { get; set; }

        public string Name { get; set; }

        public string ThumbnailUrl
        {
            get
            {
                var name = Name.Replace(" ", "_");
                var url = $"http://techfests.com/Resources/SpeakerPhotos/_t/{name}_jpg.jpg";
                return url;
            }
        }

        public string ImageUrl
        {
            get
            {
                var name = Name.Replace(" ", "_");
                var url = $"http://techfests.com/Resources/SpeakerPhotos/{name}.jpg";
                return url;
            }
        }
    }
}