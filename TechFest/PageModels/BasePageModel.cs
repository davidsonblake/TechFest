using FreshMvvm;
using PropertyChanged;
using System.Reflection;

namespace TechFest.PageModels
{
    [ImplementPropertyChanged]
    public class BasePageModel : FreshBasePageModel
    {
        private bool _hasAppeared;

        public IDataService DataService { get; private set; }

        public bool IsBusy { get; set; }

        public bool HasAppeared => _hasAppeared;

        public string Title { get; set; }

        public Assembly Assembly => typeof(App).GetTypeInfo().Assembly;

        public BasePageModel(IDataService dataService)
        {
            DataService = dataService;

            _hasAppeared = false;
        }

        protected override void ViewIsAppearing(object sender, System.EventArgs e)
        {
            _hasAppeared = true;

            base.ViewIsAppearing(sender, e);
        }
    }
}