using System;
using System.Windows.Input;
using System.Xml.Serialization;

namespace TechFest.Models
{
    [XmlRoot(ElementName = "field")]
    public class Event
    {
        [XmlElement(ElementName = "Tagline")]
        public string Tagline { get; set; }

        [XmlElement(ElementName = "Url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "StartDate")]
        public DateTime? StartDate { get; set; }

        [XmlElement(ElementName = "EndDate")]
        public DateTime? EndDate { get; set; }

        [XmlElement(ElementName = "Active")]
        public string Active { get; set; }

        [XmlElement(ElementName = "Hosted")]
        public string Hosted { get; set; }

        [XmlElement(ElementName = "LastModified")]
        public string LastModified { get; set; }

        public string Title { get; set; }

        public ICommand EventSelectedCommand { get; set; }
    }
}