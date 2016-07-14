using TechFest.Models;

namespace TechFest.PageModels
{
    public class SessionPageModel : BasePageModel
    {
        public Session Session { get; set; }

        public SessionPageModel(IDataService dataService)
            : base(dataService)
        {
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Session = initData as Session;
        }
    }
}