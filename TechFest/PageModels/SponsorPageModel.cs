using TechFest.Models;

namespace TechFest.PageModels
{
    public class SponsorPageModel : BasePageModel
    {
        public Sponsor Sponsor { get; set; }

        public SponsorPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Sponsor = initData as Sponsor;
        }
    }
}