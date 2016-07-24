using System;
using System.Windows.Input;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace TechFest.Models
{
    [ImplementPropertyChanged]
    public class SpeakerPair
    {
        public Speaker Speaker1 { get; set; }
        public Speaker Speaker2 { get; set; }
		public ICommand Speaker1Selected { get; set;}
		public ICommand Speaker2Selected { get; set;}
		private Action<Speaker> _speakerSelected;

		public SpeakerPair(Action<Speaker> speakerSelectedAction)
		{
			_speakerSelected = speakerSelectedAction;
			Speaker1Selected = new Command(() => _speakerSelected.Invoke(Speaker1));
			Speaker2Selected = new Command(() => _speakerSelected.Invoke(Speaker2));
		}
    }
}