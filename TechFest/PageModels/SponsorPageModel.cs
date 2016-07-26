using System;
using TechFest.Models;
using Xamarin.Forms;

namespace TechFest.PageModels
{
    public class SponsorPageModel : BasePageModel
    {
        public Sponsor Sponsor { get; set; }

        public Command<string> SocialSelected => new Command<string>(HandleSocialSelected);

        public SponsorPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Sponsor = initData as Sponsor;
        }

        private void HandleSocialSelected(string obj)
        {
            if (obj.StartsWith(@"//"))
                obj = "http:" + obj;

            Device.OpenUri(new Uri(obj));
        }
    }
}