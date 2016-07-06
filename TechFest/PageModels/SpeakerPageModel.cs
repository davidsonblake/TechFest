using TechFest.Models;

namespace TechFest.PageModels
{
    public class SpeakerPageModel : BasePageModel
    {
        public Speaker Speaker { get; set; }

        public SpeakerPageModel(IDataService dataService) 
            : base(dataService)
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            Speaker = initData as Speaker;
        }
    }
}