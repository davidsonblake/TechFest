using System;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SpeakerPageModel : BasePageModel
    {
        public Speaker Speaker { get; set; }

        public Command<string> SocialSelected => new Command<string>(HandleSocialSelected);
        
        public SpeakerPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Speaker = initData as Speaker;
        }

        private void HandleSocialSelected(string obj)
        {
            if (obj.StartsWith(@"//"))
                obj = "http:" + obj;

            Device.OpenUri(new Uri(obj));
        }
    }
}