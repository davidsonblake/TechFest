using FreshMvvm;
using PropertyChanged;

namespace TechFest.Models
{
    [ImplementPropertyChanged]
    public class SpeakerPair
    {
        public Speaker Speaker1 { get; set; }
        public Speaker Speaker2 { get; set; }
    }
}